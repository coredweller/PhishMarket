using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.Admin
{
    public partial class Shows : System.Web.UI.Page
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
            ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var shows = showService.GetAllShows();

            gridShows.DataSource = shows;
            gridShows.DataBind();
        }
    }
}
