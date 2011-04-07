using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using TheCore.Interfaces;
using System.Collections.Generic;

namespace PhishMarket.MyPhishMarket
{
    public partial class MyPosters : PhishMarketBasePage
    {
        Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Add Posters for Phish Shows Here!";

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

            var tours = tourService.GetAllToursDescending().ToList();

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

        public void btnAddPicture_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Response.Redirect(LinkBuilder.AddPhotoLink(new Guid(ddlShows.SelectedValue), PhotoType.Poster, Request.Url.ToString()));
        }

        public void btnAddOther_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

            if (shows != null && shows.Count > 0)
            {
                ddlShows.Items.AddRange((from s in shows
                                         select new ListItem(s.GetShowName(), s.ShowId.ToString())).ToArray());
            }
        }

        public void btnShowFromTour_Click(object sender, EventArgs e)
        {
            if (ddlTours.SelectedValue == "none")
                return;

            var tourId = new Guid(ddlTours.SelectedValue);

            PosterService posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());
            MyShowPosterService mySPService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());

            var myShowPosters = mySPService.GetMyShowPosterByTourAndUser(tourId, userId);

            IList<IPoster> posters = new List<IPoster>();

            myShowPosters.ToList().ForEach(x =>
            {
                posters.Add(posterService.GetPoster(x.PosterId));
            });

            rptPoster.DataSource = posters;
            rptPoster.DataBind();
        }

        public void btnShowFromShow_Click(object sender, EventArgs e)
        {
            ShowFromShow(null);
        }

        private void ShowFromShow(Guid? posterId)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Guid showId = new Guid(ddlShows.SelectedValue);

            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            MyShowPosterService myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());
            PosterService posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);

            var myShowPosters = myShowPosterService.GetMyShowPosterByMyShow(myShow.MyShowId);

            IList<IPoster> posters = new List<IPoster>();

            myShowPosters.ToList().ForEach(x =>
            {
                posters.Add(posterService.GetPoster(x.PosterId));
            });

            if (posterId != null)
            {
                posters = posters.Where(x => x.PosterId != posterId).ToList();
            }

            if (posters == null || posters.Count <= 0)
            {
                phNoImages.Visible = true;
            }
            
            rptPoster.DataSource = posters;
            rptPoster.DataBind();
        }

        public void rptPoster_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var posterId = new Guid(e.CommandArgument.ToString());

            switch (e.CommandName.ToLower())
            {
                case "remove":
                    Remove(posterId); ;
                    break;
            }
        }

        private void Remove(Guid posterId)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            ResetPanels();

            Guid showId = new Guid(ddlShows.SelectedValue);

            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            MyShowPosterService myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);
            var myShowPoster = myShowPosterService.GetMyShowPosterByMyShowAndPosterId(myShow.MyShowId, posterId);

            bool success = false;

            if (myShowPoster != null)
            {
                myShowPosterService.DeleteCommit(myShowPoster);
                success = true;
            }

            if (success)
            {
                phRemoveSuccess.Visible = true;
                ShowFromShow(posterId);
            }
            else
                phRemoveError.Visible = true;
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
