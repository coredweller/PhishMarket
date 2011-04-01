using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.Controls
{
    public partial class RecentShowReviews : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var myShows = myShowService.GetAllMyShows().Where(x => x.Notes != null).OrderByDescending(y => y.NotesUpdatedDate).Take(5);

            var reviews = (from myShow in myShows
                           from show in showService.GetOfficialShows()
                           where show.ShowId == myShow.ShowId
                           select new { Show = show, MyShow = myShow });

            rptRecentShowReviews.DataSource = reviews;
            rptRecentShowReviews.DataBind();
        }
    }
}