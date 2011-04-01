using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Guess;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;

namespace PhishMarket.Admin
{
    public partial class CreateTopic : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void ResetPanels()
        {
            phError.Visible = false;
            phSuccess.Visible = false;
            phTypeError.Visible = false;
            phChooseTypeError.Visible = false;
        }

        private void Bind()
        {
            TourService tourService = new TourService(Ioc.GetInstance<ITourRepository>());
            ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            ddlTourType.Items.AddRange((from x in tourService.GetAllToursDescending() select new ListItem(x.TourName, x.TourId.ToString())).ToArray());
            ddlShowType.Items.AddRange((from x in showService.GetAllShows() select new ListItem(x.GetShowName(), x.ShowId.ToString())).ToArray());

            ListItem item = new ListItem("Please select either a tour or a song", "-1");

            ddlTourType.Items.Insert(0, item);
            ddlShowType.Items.Insert(0, item);

            item.Selected = true;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();

            TopicService service = new TopicService(Ioc.GetInstance<ITopicRepository>());

            bool success = false;
            DateTime startDate;
            DateTime endDate;
            Guid? showType = null;
            Guid? tourType = null;
            TopicType t = TopicType.None;

            if (Validated(out startDate, out endDate))
            {
                if (ddlShowType.SelectedValue != "-1" && ddlTourType.SelectedValue == "-1")
                {
                    Guid g = new Guid(ddlShowType.SelectedValue);
                    showType = g;
                    t = TopicType.Show;
                }
                else if (ddlShowType.SelectedValue == "-1" && ddlTourType.SelectedValue != "-1")
                {
                    Guid g = new Guid(ddlTourType.SelectedValue);
                    tourType = g;
                    t = TopicType.Tour;
                }

                Topic topic = new Topic()
                {
                    TopicName = txtTopicName.Text.Trim(),
                    TopicId = Guid.NewGuid(),
                    StartDate = startDate,
                    EndDate = endDate,
                    Notes = txtNotes.Text.Trim(),
                    ShowId = showType,
                    TourId = tourType,
                    Type = (short)t
                };

                service.SaveCommit(topic, out success);
            }
        }

        private bool Validated(out DateTime startDate, out DateTime endDate)
        {
            bool valid = true;
            startDate = DateTime.Now;
            endDate = DateTime.Now;

            try
            {

                if (string.IsNullOrEmpty(txtTopicName.Text.Trim()))
                    valid = false;

                if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                {
                    DateTime tempDate;

                    bool validDate = DateTime.TryParse(txtStartDate.Text.Trim(), out tempDate);

                    if (!validDate)
                        startDate = DateTime.MinValue;
                    else
                        startDate = tempDate;
                }
                else
                {
                    valid = false;
                }

                if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                {
                    DateTime tempDate;

                    bool validEndDate = DateTime.TryParse(txtEndDate.Text.Trim(), out tempDate);

                    if (!validEndDate)
                        endDate = DateTime.MinValue;
                    else
                        endDate = tempDate;
                }
                else
                {
                    valid = false;
                }

                valid = valid && true;
            }
            catch (Exception ex)
            {
                valid = false;
            }

            valid = DetermineTopicType(valid);

            return valid;
        }

        private bool DetermineTopicType(bool valid)
        {
            

            if (valid)
            {
                var showValue = ddlShowType.SelectedValue;
                var tourValue = ddlTourType.SelectedValue;

                if (showValue != "-1" && tourValue != "-1")
                {
                    phTypeError.Visible = true;
                    valid = false;

                }
                else if (showValue == "-1" && tourValue == "-1")
                {
                    phChooseTypeError.Visible = true;
                    valid = false;
                }
                else
                {

                    phSuccess.Visible = true;
                    phError.Visible = false;
                }
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }

            

            return valid;
        }
    }
}
