using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Security;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using TheCore.Interfaces;

namespace PhishMarket.MyPhishMarket
{
    public partial class MyPictures : PhishMarketBasePage
    {
        Guid userId;
        public Guid ShowId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Add Pictures for Phish Shows Here!");

            userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            hdnUserId.Value = userId.ToString();

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            ResetPanels();

            var tourService = new TourService(Ioc.GetInstance<ITourRepository>());

            var tours = tourService.GetAllToursDescending();

            ddlTours.Items.AddRange((from t in tours
                                     select new ListItem(t.TourName, t.TourId.ToString())).ToArray());

            var noneItem = new ListItem("None", "none");

            ddlTours.Items.Insert(0, noneItem);

            noneItem.Selected = true;

            if (!string.IsNullOrEmpty(Request.QueryString["showId"]))
            {
                var showId = new Guid(Request.QueryString["showId"]);

                ShowId = showId;

                var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

                var show = showService.GetShow(showId);

                if (show != null)
                {
                    ddlTours.SelectedValue = show.TourId.ToString();
                    SetShows(show.TourId.Value);

                    if (!ddlShows.Items.Contains(new ListItem(show.GetShowName(), show.ShowId.ToString())))
                    {
                        phAddShow.Visible = true;
                    }

                    ddlShows.SelectedValue = show.ShowId.ToString();
                }
            }
        }

        public void ddlTours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetPanels();

            var selectedValue = ((DropDownList)sender).SelectedValue;

            switch (selectedValue)
            {
                case "":
                case "none":
                    return;
                default:
                    SetShows(new Guid(selectedValue));
                    break;
            }
            
        }

        public void btnAddPicture_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Response.Redirect(LinkBuilder.AddPhotoLink(new Guid(ddlShows.SelectedValue), PhotoType.Art, Request.Url.ToString()));
        }

        public void btnAddOther_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
                return;

            Response.Redirect(LinkBuilder.FindForShowLink(new Guid(ddlShows.SelectedValue)));
        }

        private void SetShows(Guid tourId)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var shows = myShowService.GetShowsFromMyShowsForUser(userId, tourId);

            ddlShows.Items.Clear();

            ddlShows.Items.AddRange((from s in shows
                                     select new ListItem(s.GetShowName(), s.ShowId.ToString())).ToArray());
        }
        
        private void ShowError(PlaceHolder ph, string message)
        {
            phError.Visible = true;
            lblError.Text = message;
        }

        private void ResetPanels()
        {
            phError.Visible = false;
            phSuccess.Visible = false;
            phNoTicketStubsError.Visible = false;
            //phMain.Visible = true;
        }
    }
}
