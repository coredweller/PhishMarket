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
    public partial class TicketStubs : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
            
        }

        public void yearSelector_YearSelected(object sender, PhishPond.Concrete.EventArgs.SelectYearCommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bind()
        {
            if (string.IsNullOrEmpty(Request.QueryString["y"])) return;

            var ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());

            var ticketStubs = ticketStubService.GetTicketStubsByYear(int.Parse(Request.QueryString["y"]));

            rptTicketStubs.DataSource = ticketStubs.ToList();
            rptTicketStubs.DataBind();
        }
    }
}
