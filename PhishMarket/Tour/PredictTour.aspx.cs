using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.TourPages
{
    public partial class PredictTour : PhishMarketBasePage
    {
        TourService service = new TourService(Ioc.GetInstance<ITourRepository>());
        public string TourName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var tours = service.GetAllToursDescending();

            foreach(var tour in tours)
            {
                string tourName = tour.TourName;

                if (tour.Shows.Count > 0) { tourName += "*"; }

                ddlTours.Items.Add(new ListItem(tourName, tour.TourId.ToString()));
            }
        }

        public void btnSelectTour_Click(object sender, EventArgs e)
        {
            Guid g = new Guid(ddlTours.SelectedItem.Value);

            var tour = service.GetTour(g);

            if (tour.TourName != null)
            { TourName = tour.TourName; }

            rptShows.DataSource = tour.Shows;
            rptShows.DataBind();
        }
    }
}
