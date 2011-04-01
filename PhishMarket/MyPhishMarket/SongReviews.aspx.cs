using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.MyPhishMarket
{
    public partial class SongReviews : PhishMarketBasePage
    {
        public string ShowName { get; set; }
        public string SongName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            if (string.IsNullOrEmpty(Request.QueryString["setSongId"]))
                Response.Redirect(LinkBuilder.DashboardLink());

            var setSongId = new Guid(Request.QueryString["setSongId"]);

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var setService = new SetService(Ioc.GetInstance<ISetRepository>());
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

            var setSong = setSongService.GetSetSong(setSongId);
            var set = setService.GetSet(setSong.SetId.Value);
            var show = showService.GetShow(set.ShowId.Value);

            ShowName = show.GetShowName();
            SongName = setSong.SongName;
            lnkReviewShow.NavigateUrl = LinkBuilder.AnalysisLink(show.ShowId);
            lnkNoReviews.NavigateUrl = LinkBuilder.AnalysisLink(show.ShowId);

            BindReviews(setSongId);
        }

        private void BindReviews(Guid setSongId)
        {
            var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());

            var analyses = analysisService.GetAnalysisBySetSong(setSongId).OrderByDescending(x => x.UpdatedDate).ToList();

            if (analyses == null || analyses.Count <= 0)
            {
                phNoReviews.Visible = true;
                return;
            }

            rptReviews.DataSource = analyses;
            rptReviews.DataBind();
        }
    }
}
