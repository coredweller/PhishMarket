using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;
using System.Web.Security;

namespace PhishMarket.MyPhishMarket
{
    public partial class ShowReviews : PhishMarketBasePage
    {
        public string ShowName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void rptSongs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bind()
        {
            if (string.IsNullOrEmpty(Request.QueryString["showId"]))
                Response.Redirect(LinkBuilder.DashboardLink());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var showId = new Guid(Request.QueryString["showId"]);

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
            var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());

            BindReviews(showId, ref myShowService);

            var show = (Show)showService.GetShow(showId);

            SetPageTitle("Review of " + show.GetShowName());

            ShowName = show.GetShowName();

            var ss = (from set in show.Sets.OrderBy(x => x.SetNumber)
                      from song in setSongService.GetSetSongsBySet(set.SetId).OrderBy(z => z.Order).DefaultIfEmpty()
                      from analysis in analysisService.GetAnalysisBySetSongAndUser(song.SetSongId, userId).DefaultIfEmpty()
                      select new { Set = set, Song = song, Analysis = analysis }).ToList();

            //SlideShowExtender1.ContextKey = showId.ToString();

            rptSongs.DataSource = ss;
            rptSongs.DataBind();
        }

        private void BindReviews(Guid showId, ref MyShowService myShowService)
        {
            var myShows = myShowService.GetMyShowsForShow(showId).Where(x => x.Notes != null).OrderByDescending(y => y.NotesUpdatedDate).ToList();

            if (myShows == null || myShows.Count <= 0)
            {
                phNoReviews.Visible = true;
                return;
            }

            rptReviews.DataSource = myShows;
            rptReviews.DataBind();
        }

        public string GetSetNumber(short setNumber, bool encore)
        {
            if (encore)
                return "Encore";

            return "Set " + setNumber.ToString();
        }
    }
}
