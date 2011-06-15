using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Helpers;

namespace PhishMarket
{
    public partial class TestPage : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void yearSelector_YearSelected(object sender, PhishPond.Concrete.EventArgs.SelectYearCommandEventArgs e)
        {
            var year = e.Year;

            var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", year.ToString());
            Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
        }

    }
}
