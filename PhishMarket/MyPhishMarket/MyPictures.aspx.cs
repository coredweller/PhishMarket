using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Security;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using TheCore.Interfaces;
using System.Collections.Generic;

namespace PhishMarket.MyPhishMarket
{
    public partial class MyPictures : PhishMarketBasePage
    {
        Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
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

        public void rptArt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var artId = new Guid(e.CommandArgument.ToString());

            switch (e.CommandName.ToLower())
            {
                case "remove":
                    Remove(artId); ;
                    break;
            }
        }

        private void Remove(Guid artId)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Guid showId = new Guid(ddlShows.SelectedValue);

            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            MyShowArtService myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);
            var myShowArt = myShowArtService.GetMyShowArtByMyShowAndArtId(myShow.MyShowId, artId);

            bool success = false;

            if (myShowArt != null)
            {
                myShowArtService.DeleteCommit(myShowArt);
                success = true;
            }

            if (success)
            {
                phRemoveSuccess.Visible = true;
                ShowFromShow(artId);
            }
            else
                phRemoveError.Visible = true;
        }

        public void btnAddPicture_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Response.Redirect(LinkBuilder.AddPhotoLink(new Guid(ddlShows.SelectedValue), PhotoType.Art, Request.Url.ToString()));
        }

        public void btnAddOther_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Response.Redirect(LinkBuilder.FindForShowLink(new Guid(ddlShows.SelectedValue)));
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

            ArtService posterService = new ArtService(Ioc.GetInstance<IArtRepository>());
            MyShowArtService mySPService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            var myShowArts = mySPService.GetMyShowArtByTourAndUser(tourId, userId);

            IList<IArt> posters = new List<IArt>();

            myShowArts.ToList().ForEach(x =>
            {
                posters.Add(posterService.GetArt(x.ArtId));
            });

            rptArt.DataSource = posters;
            rptArt.DataBind();
        }

        public void btnShowFromShow_Click(object sender, EventArgs e)
        {
            ShowFromShow(null);
        }

        private void ShowFromShow(Guid? artId)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            phNoImages.Visible = false;

            Guid showId = new Guid(ddlShows.SelectedValue);

            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            MyShowArtService myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());
            ArtService artService = new ArtService(Ioc.GetInstance<IArtRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);

            var myShowArts = myShowArtService.GetMyShowArtByMyShow(myShow.MyShowId);

            IList<IArt> art = new List<IArt>();

            myShowArts.ToList().ForEach(x =>
            {
                art.Add(artService.GetArt(x.ArtId));
            });

            if (artId != null)
            {
                art = art.Where(x => x.ArtId != artId).ToList();
            }

            if (art == null || art.Count <= 0)
            {
                phNoImages.Visible = true;
            }

            rptArt.DataSource = art;
            rptArt.DataBind();
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
        }
    }
}
