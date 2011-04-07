using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Helpers;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;

namespace PhishMarket.MyPhishMarket
{
    public partial class FullShowReview : System.Web.UI.Page
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
            if (string.IsNullOrEmpty(Request.QueryString["myShowId"]))
                Response.Redirect(new LinkBuilder().DashboardLink());

            var myShowId = new Guid(Request.QueryString["myShowId"]);

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShow = myShowService.GetMyShow(myShowId);

            if (myShow != null)
            {
                Page.Title = "Review of " + ((MyShow)myShow).Show.GetShowName();

                lblReview.Text = myShow.Notes;
            }
        }
    }
}
