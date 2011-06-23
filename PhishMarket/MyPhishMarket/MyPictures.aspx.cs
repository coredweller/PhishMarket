using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Security;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using TheCore.Interfaces;
using TheCore.Helpers;

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

        public void yearSelector_YearSelected(object sender, PhishPond.Concrete.EventArgs.SelectYearCommandEventArgs e)
        {
            SetShows(e.Year);
        }

        private void SetShows(int year)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var shows = myShowService.GetShowsFromMyShowsForUser(userId, year);

            ddlShows.Items.Clear();

            if (shows != null && shows.Count > 0)
            {
                ddlShows.Items.AddRange((from s in shows
                                         select new ListItem(s.GetShowName(), s.ShowId.ToString())).ToArray());
            }
        }

        private void Bind()
        {
            ResetPanels();

            if (!string.IsNullOrEmpty(Request.QueryString["showId"]))
            {
                var showId = new Guid(Request.QueryString["showId"]);

                ShowId = showId;

                var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

                var show = showService.GetShow(showId);

                if (show != null)
                {
                    SetShows(show.ShowDate.Value.Year);

                    if (!ddlShows.Items.Contains(new ListItem(show.GetShowName(), show.ShowId.ToString())))
                    {
                        phAddShow.Visible = true;
                    }

                    ddlShows.SelectedValue = show.ShowId.ToString();
                }
            }
        }

        public void btnAddPicture_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
            {
                ShowNotSelectedMessage();
                return;
            }

            Response.Redirect(LinkBuilder.AddPhotoLink(new Guid(ddlShows.SelectedValue), PhotoType.Art, Request.Url.ToString()));
        }

        public void btnAddOther_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
            {
                ShowNotSelectedMessage();
                return;
            }

            Response.Redirect(LinkBuilder.FindForShowLink(new Guid(ddlShows.SelectedValue)));
        }

        private void ShowNotSelectedMessage()
        {
            var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "To add pictures please choose a show below.");
            Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
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
