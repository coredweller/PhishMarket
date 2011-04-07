using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using TheCore.Interfaces;

namespace PhishMarket.MyPhishMarket
{
    public partial class MyTicketStubs : PhishMarketBasePage
    {
        Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Add Ticket Stubs for Phish Shows Here!";

            userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            ResetPanels();

            var tourService = new TourService(Ioc.GetInstance<ITourRepository>());

            var tours = tourService.GetAllToursDescending();

            ddlTours.Items.AddRange((from t in tours
                                     select new ListItem(t.TourName, t.TourId.ToString())).ToArray());

            var noneItem = new ListItem("None", "none");

            ddlTours.Items.Insert(0, noneItem);

            noneItem.Selected = true;

            if (!string.IsNullOrEmpty(Request.QueryString["showId"]))
            {
                var showId = new Guid(Request.QueryString["showId"]);

                var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

                var show = showService.GetShow(showId);

                if (show != null)
                {
                    ddlTours.SelectedValue = show.TourId.ToString();
                    SetShows(show.TourId.Value);
                    ddlShows.SelectedValue = show.ShowId.ToString();
                }
            }
        }

        public void rptTicketStubs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var ticketStubId = new Guid(e.CommandArgument.ToString());

            switch (e.CommandName.ToLower())
            {
                case "remove":
                    Remove(ticketStubId); ;
                    break;
            }
        }

        private void Remove(Guid ticketStubId)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            ResetPanels();

            Guid showId = new Guid(ddlShows.SelectedValue);

            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            MyShowTicketStubService myShowTicketStubService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);
            var myShowTicketStub = myShowTicketStubService.GetMyShowTicketStubByMyShowAndTicketStubId(myShow.MyShowId, ticketStubId);

            bool success = false;

            if (myShowTicketStub != null)
            {
                myShowTicketStubService.DeleteCommit(myShowTicketStub);
                success = true;
            }

            if (success)
            {
                phRemoveSuccess.Visible = true;
                ShowFromShow(ticketStubId);
            }
            else
                phRemoveError.Visible = true;
        }

        public void ddlTours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetPanels();

            var selectedValue = ((DropDownList)sender).SelectedValue;

            switch (selectedValue)
            {
                case "":
                case "none":
                    return;
                default:
                    SetShows(new Guid(selectedValue));
                    break;
            }
        }

        public void btnAddPicture_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Response.Redirect(LinkBuilder.AddPhotoLink(new Guid(ddlShows.SelectedValue), PhotoType.TicketStub, Request.Url.ToString()));
        }

        public void btnAddOther_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Response.Redirect(LinkBuilder.FindForShowLink(new Guid(ddlShows.SelectedValue)));
        }

        public void btnShowFromShow_Click(object sender, EventArgs e)
        {
            ShowFromShow(null);
        }

        private void ShowFromShow(Guid? ticketStubId)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Guid showId = new Guid(ddlShows.SelectedValue);

            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            MyShowTicketStubService myShowTicketStubService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());
            TicketStubService ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);

            var myShowTicketStubs = myShowTicketStubService.GetMyShowTicketStubByMyShow(myShow.MyShowId);

            IList<ITicketStub> ticketStubs = new List<ITicketStub>();

            myShowTicketStubs.ToList().ForEach(x =>
            {
                ticketStubs.Add(ticketStubService.GetTicketStub(x.TicketStubId));
            });

            if (ticketStubId != null)
            {
                ticketStubs = ticketStubs.Where(x => x.TicketStubId != ticketStubId).ToList();
            }

            if (ticketStubs == null || ticketStubs.Count <= 0)
            {
                phNoImages.Visible = true;
            }

            rptTicketStubs.DataSource = ticketStubs;
            rptTicketStubs.DataBind();
        }

        private void SetShows(Guid tourId)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var shows = myShowService.GetShowsFromMyShowsForUser(userId, tourId);

            ddlShows.Items.Clear();

            ddlShows.Items.AddRange((from s in shows
                                     select new ListItem(s.GetShowName(), s.ShowId.ToString())).ToArray());
        }

        public void btnShowFromTour_Click(object sender, EventArgs e)
        {
            if (ddlTours.SelectedValue == "none")
                return;

            var tourId = new Guid(ddlTours.SelectedValue);

            TicketStubService posterService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());
            MyShowTicketStubService mySPService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());

            var myShowTicketStubs = mySPService.GetMyShowTicketStubByTourAndUser(tourId, userId);

            IList<ITicketStub> posters = new List<ITicketStub>();

            myShowTicketStubs.ToList().ForEach(x =>
            {
                posters.Add(posterService.GetTicketStub(x.TicketStubId));
            });

            rptTicketStubs.DataSource = posters;
            rptTicketStubs.DataBind();
        }

        private void ShowError(PlaceHolder ph, string message)
        {
            phError.Visible = true;
            lblError.Text = message;
        }

        private void ResetPanels()
        {
            phError.Visible = false;
            phSuccess.Visible = false;
            phNoTicketStubsError.Visible = false;
            phMain.Visible = true;
            phRemoveError.Visible = false;
            phRemoveSuccess.Visible = false;
        }
    }
}
