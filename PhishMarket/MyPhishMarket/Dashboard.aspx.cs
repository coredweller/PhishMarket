using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using PhishPond.Concrete;
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
            SetPageTitle("My Phish Market Dashboard");

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
            
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var shows = myShowService.GetShowsFromMyShowsForUser(userId);
            rptShows.DataSource = shows;
            rptShows.DataBind();
        }

        public void yearSelector_YearSelected(object sender, PhishPond.Concrete.EventArgs.SelectYearCommandEventArgs e)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var shows = myShowService.GetShowsFromMyShowsForUser(userId, e.Year);

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }

        private void BindMyShowItems()
        {
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());
            //var myShowTicketStubService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());
            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            var myShowPosterThumbnail = myShowPosterService.GetAnyMyShowPosterForUser(userId);
            //var myShowTicketStubThumbnail = myShowTicketStubService.GetAnyMyShowTicketStubForUser(userId);
            var myShowArtThumbnail = myShowArtService.GetAnyMyShowArtForUser(userId);

            if (myShowPosterThumbnail != null)
            {
                phMyPoster.Visible = true;
                imgPoster.ImageUrl = LinkBuilder.GetImageLink(myShowPosterThumbnail.Thumbnail.PhotoId);
            }

            //if (myShowTicketStubThumbnail != null)
            //{
            //    phTicketStub.Visible = true;
            //    imgTicketStub.ImageUrl = LinkBuilder.GetImageLink(myShowTicketStubThumbnail.Thumbnail.PhotoId);
            //}

            if (myShowArtThumbnail != null)
            {
                phArt.Visible = true;
                imgArt.ImageUrl = LinkBuilder.GetImageLink(myShowArtThumbnail.Thumbnail.PhotoId);
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
                    if (favoriteLiveShow != null)
                    {
                        lblFavoriteLiveShow.Text = string.Format("{0} - {1}, {2}", favoriteLiveShow.ShowDate.Value.ToString("MM/dd/yyyy"), favoriteLiveShow.VenueName, favoriteLiveShow.State);
                    }
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
                var shows = myShowService.GetShowsFromMyShowsForUser(userId);
                rptShows.DataSource = shows;
                rptShows.DataBind();
            }
        }

        public string GetShowName(string venue, string date)
        {
            return date + "-" + venue;
        }
    }
}
