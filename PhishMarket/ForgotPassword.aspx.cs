using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhishMarket
{
    public partial class ForgotPassword : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Forgot Your Phish Market Password?");
        }
    }
}
