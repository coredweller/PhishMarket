using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.TourPages
{
    public partial class PredictTour : PhishMarketBasePage
    {
        public string TourName;

        protected void Page_Load(object sender, EventArgs e)
        {
            ///ALL tour stuff was removed.  If you want to use this page
            /// Then you must use the year selector instead.
        }
    }
}
