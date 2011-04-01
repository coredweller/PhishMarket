using System;

namespace PhishMarket.MyPhishMarket
{
    public partial class ChangeProfile : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void imgLinkStep3_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(LinkBuilder.ProfileStep3Link());
        }

        public void imgLinkStep2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(LinkBuilder.ProfileStep2Link());
        }

        public void imgLinkStep1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(LinkBuilder.ProfileStep1Link());
        }
    }
}
