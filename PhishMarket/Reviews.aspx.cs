using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket
{
    public partial class Reviews : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Analyze all Phish Shows Here!");

            if (!IsPostBack)
            {

            }
        }

        public void yearSelector_YearSelected(object sender, PhishPond.Concrete.EventArgs.SelectYearCommandEventArgs e)
        {
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var shows = showService.GetShowsByYear(e.Year);

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }
    }
}
