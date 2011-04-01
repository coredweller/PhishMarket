using System;
using TheCore.Services;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using TheCore.Repository;

namespace PhishMarket.Admin
{
    public partial class CreateTour : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            TourService service = new TourService(Ioc.GetInstance<ITourRepository>());

            bool success = false;
            DateTime? startDate, endDate;

            if (Validated(out startDate, out endDate))
            {
                Tour tour = new Tour()
                {
                    TourId = Guid.NewGuid(),
                    TourName = txtTourName.Text.Trim(),
                    StartDate = startDate,
                    EndDate = endDate,
                    Official = chkOfficial.Checked
                };

                service.SaveCommit(tour, out success);
            }

            if (success)
            {
                phSuccess.Visible = true;
                phError.Visible = false;
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
        }

        private bool Validated(out DateTime? startDate, out DateTime? endDate)
        {
            bool valid = false;
            startDate = null;
            endDate = null;

            try
            {

                if (string.IsNullOrEmpty(txtTourName.Text.Trim()))
                    valid = false;

                if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                {
                    DateTime tempDate;

                    bool validDate = DateTime.TryParse(txtStartDate.Text.Trim(), out tempDate);

                    if (!validDate)
                        startDate = null;
                    else
                        startDate = tempDate;
                }

                if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                {
                    DateTime tempDate;

                    bool validEndDate = DateTime.TryParse(txtEndDate.Text.Trim(), out tempDate);

                    if (!validEndDate)
                        endDate = null;
                    else
                        endDate = tempDate;
                }

                valid = true;
            }
            catch (Exception ex)
            {
                valid = false;
            }

            return valid;
        }
    }
}
