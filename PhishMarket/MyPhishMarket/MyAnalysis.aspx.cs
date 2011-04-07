using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.MyPhishMarket
{
    public partial class MyAnalysis : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Analyze all Phish Shows Here!");

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            TourService service = new TourService(Ioc.GetInstance<ITourRepository>());

            var tours = service.GetAllToursDescending();

            foreach (var tour in tours)
            {
                ddlTours.Items.Add(new ListItem(tour.TourName, tour.TourId.ToString()));
            }
        }

        public void btnSelectTour_Click(object sender, EventArgs e)
        {
            TourService service = new TourService(Ioc.GetInstance<ITourRepository>());
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var g = new Guid(ddlTours.SelectedItem.Value);

            var tour = service.GetTour(g);

            var showIds = (from show in showService.GetAllShows()
                           select show.ShowId).ToList();

            var shows = (from show in tour.Shows
                         where showIds.Contains(show.ShowId)
                         select show).OrderBy(x => x.ShowDate).ToList();

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }
    }
}
