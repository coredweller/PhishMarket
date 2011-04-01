using System;

namespace PhishMarket.TourPages
{
    public partial class PredictShow : PhishMarketBasePage
    {
        public string WholeShowLink { get; set; }
        public string SetBasedShowLink { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
                Response.Redirect(LinkBuilder.PredictTourLink());

            Guid g = new Guid(Request.QueryString["id"]);

            WholeShowLink = LinkBuilder.PredictShowWholeShowLink(g);

            SetBasedShowLink = LinkBuilder.PredictShowSetBasedShowLink(g);


            
        }
    }
}
