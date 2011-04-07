using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using System.Web.Security;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;

namespace PhishMarket.MyPhishMarket
{
    public partial class ViewProfile : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void ResetPanels()
        {
            phNoProfileError.Visible = false;
        }

        private void Bind()
        {
            ResetPanels();

            if (string.IsNullOrEmpty(Request.QueryString["userId"]) && string.IsNullOrEmpty(Request.QueryString["u"]))
                return;

            Guid userId = Request.QueryString["userId"] != null ? new Guid(Request.QueryString["userId"]) : GetUserIdFromYafId(Request.QueryString["u"]);

            var userService = new PhishMarketUserService(Ioc.GetInstance<IPhishMarketUserRepository>());

            var user = userService.GetUserById(userId);

            if (user != null)
            {
                SetPageTitle(user.UserName + "'s Profile");
            }

            BindProfile(userId);
            BindPosters(userId);
            BindTicketStubs(userId);
            BindArt(userId);
            BindMyShows(userId);
            BindShowsReviewed(userId);
        }

        private void BindShowsReviewed(Guid userId)
        {
            ///LEFT OFF HERE   Not sure when this was added but I found it on 4/6/11 so im going to leave it and see if i ever remember
        }

        private void BindMyShows(Guid userId)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var shows = myShowService.GetMyShowsFromMyShowsForUser(userId);

            rptMyShows.DataSource = shows;
            rptMyShows.DataBind();
        }

        private Guid GetUserIdFromYafId(string yafUserId)
        {
            var yafService = new YafService(Ioc.GetInstance<IYafRepository>());

            var userResult = yafService.GetUserIdFromYafId(int.Parse(yafUserId));

            return new Guid(userResult.ProviderUserKey);
        }

        private void BindArt(Guid userId)
        {
            ArtService artService = new ArtService(Ioc.GetInstance<IArtRepository>());

            var art = artService.GetArtByUser(userId).ToList();

            rptArt.DataSource = art;
            rptArt.DataBind();
        }

        private void BindTicketStubs(Guid userId)
        {
            TicketStubService ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());

            var ticketStubs = ticketStubService.GetTicketStubsByUserId(userId).ToList();

            rptTicketStubs.DataSource = ticketStubs;
            rptTicketStubs.DataBind();
        }

        private void BindPosters(Guid userId)
        {
            PosterService posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());

            var posters = posterService.GetByUser(userId);

            rptPoster.DataSource = posters;
            rptPoster.DataBind();
        }

        private Guid? BindProfile(Guid userId)
        {
            TourService service = new TourService(Ioc.GetInstance<ITourRepository>());
            ProfileService profileService = new ProfileService(Ioc.GetInstance<IProfileRepository>());

            var profile = profileService.GetProfileByUserId(userId);

            if (profile == null)
            {
                phNoProfileError.Visible = true;
                return null;
            }

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

            return userId;
        }
    }
}
