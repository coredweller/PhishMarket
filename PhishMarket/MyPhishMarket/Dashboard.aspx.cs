﻿using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using PhishPond.Concrete;
using TheCore.Interfaces;
using System.Collections.Generic;
using System.Text;
using PhishPond.Repository.LinqToSql;

namespace PhishMarket.MyPhishMarket
{
    public partial class Dashboard : PhishMarketBasePage
    {
        LogWriter writer = new LogWriter();
        TourService service = new TourService(Ioc.GetInstance<ITourRepository>());
        ProfileService profileService = new ProfileService(Ioc.GetInstance<IProfileRepository>());
        public string TourName;
        Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //THIS IS A NEW SHIT
            userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            lnkChangeProfile.NavigateUrl = LinkBuilder.ChangeProfileLink();

            if (!IsPostBack)
            {
                Bind();
            }
        }
        
        private void Bind()
        {
            BindProfile();
            BindMyShowItems();

            var tours = service.GetAllToursDescending();

            foreach (var tour in tours)
            {
                string tourName = tour.TourName;

                ddlTours.Items.Add(new ListItem(tourName, tour.TourId.ToString()));
            }

            var item = new ListItem("All", "All");
            item.Selected = true;
            ddlTours.Items.Add(item);
        }

        private void BindMyShowItems()
        {
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());
            var myShowTicketStubService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());
            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            var myShowPosterAndPoster = myShowPosterService.GetAnyMyShowPosterForUser(userId);
            var myShowTicketStubAndTicketStub = myShowTicketStubService.GetAnyMyShowTicketStubForUser(userId);
            var myShowArtAndArt = myShowArtService.GetAnyMyShowArtForUser(userId);

            if (myShowPosterAndPoster.Value != null)
            {
                phMyPoster.Visible = true;
                imgPoster.ImageUrl = LinkBuilder.GetImageLink(myShowPosterAndPoster.Value.PhotoId.Value);
            }

            if (myShowTicketStubAndTicketStub.Value != null)
            {
                phTicketStub.Visible = true;
                imgTicketStub.ImageUrl = LinkBuilder.GetImageLink(myShowTicketStubAndTicketStub.Value.PhotoId.Value);
            }

            if (myShowArtAndArt.Value != null)
            {
                phArt.Visible = true;
                imgArt.ImageUrl = LinkBuilder.GetImageLink(myShowArtAndArt.Value.PhotoId.Value);
            }
        }

        public void BindProfile()
        {
            var profile = profileService.GetProfileByUserId(userId);

            if (profile != null)
            {
                //Show Profile

                SongService songService = new SongService(Ioc.GetInstance<ISongRepository>());
                ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());

                if (profile.FavoriteStudioSong != null)
                {
                    var favoriteStudioSong = songService.GetSong(profile.FavoriteStudioSong.Value);
                    lblFavoriteStudioSong.Text = string.Format("{0} - {1}", favoriteStudioSong.SongName, favoriteStudioSong.Album);
                }

                if (profile.FavoriteLiveShow != null)
                {
                    var favoriteLiveShow = showService.GetShow(profile.FavoriteLiveShow.Value);
                    lblFavoriteLiveShow.Text = string.Format("{0} - {1}, {2}", favoriteLiveShow.ShowDate.Value.ToString("MM/dd/yyyy"), favoriteLiveShow.VenueName, favoriteLiveShow.State);
                }

                if (profile.FavoriteTour != null)
                {
                    var favoriteTour = service.GetTour(profile.FavoriteTour.Value);
                    lblFavoriteTour.Text = string.Format("{0} {1}-{2}", favoriteTour.TourName, favoriteTour.StartDate.Value.ToString("MM/dd/yyyy"), favoriteTour.EndDate.Value.ToString("MM/dd/yyyy"));
                }

                lblName.Text = profile.Name;
                lblEmail.Text = profile.Email;
                lblFavoriteAlbum.Text = profile.FavoriteAlbum;

            }
            else
            {
                //Create profile
                var newProfile = new Profile
                {
                    CreatedDate = DateTime.Now,
                    ProfileId = Guid.NewGuid(),
                    UserId = userId
                };

                bool success;

                profileService.SaveCommit(newProfile, out success);
            }
        }

        public void btnSelectTour_Click(object sender, EventArgs e)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            IList<IShow> shows = new List<IShow>();

            if (ddlTours.SelectedItem.Value == "All")
            {
                TourName = "All";
                shows = myShowService.GetShowsFromMyShowsForUser(userId);
            }
            else
            {
                var g = new Guid(ddlTours.SelectedItem.Value);

                var tour = service.GetTour(g);

                if (tour.TourName != null) { TourName = tour.TourName; }

                shows = myShowService.GetShowsFromMyShowsForUser(userId, tour.TourId);
            }

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }

        public void rptShows_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    DeleteShow(new Guid(e.CommandArgument.ToString()));
                    break;
            }
        }

        private void DeleteShow(Guid showId)
        {
            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);

            myShowService.DeleteCommit(myShow);

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var show = showService.GetShow(showId);

            if (show != null)
            {
                btnSelectTour_Click(null, null);
            }
        }

        public string GetShowName(string venue, string date)
        {
            return venue + "-" + date;
        }

        public string OutputBottomLine(string showName)
        {
            var s = new StringBuilder();

            int length = showName.Length < 34 ? 34 : showName.Length;

            for (int i = 0; i < length; i++)
            {
                //that is an underscore not a blank space
                s.Append("_");
            }

            return s.ToString();
        }
    }
}
