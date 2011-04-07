using System;

namespace PhishMarket
{
    public partial class AboutUs : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("About Me");

            if (!IsPostBack)
            {

            }
        }
    }
}
