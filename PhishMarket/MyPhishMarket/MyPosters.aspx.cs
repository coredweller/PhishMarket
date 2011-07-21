using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using TheCore.Interfaces;
using TheCore.Helpers;
using PhishPond.Concrete;

namespace PhishMarket.MyPhishMarket
{
    public partial class MyPosters : PhishMarketBasePage
    {
        Guid userId;

        public Guid ShowId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Add Posters for Phish Shows Here!");

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

        public void lnkAddMyShow_Click(object sender, EventArgs e)
        {
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var showId = new Guid(hdnShowId.Value);

            var show = showService.GetShow(showId);
            var myShow = myShowService.GetMyShow(showId, userId);

            if (myShow != null)
            {
                //phAlreadyAdded.Visible = true;
                return;
            }

            var newMyShow = new MyShow
            {
                CreatedDate = DateTime.Now,
                MyShowId = Guid.NewGuid(),
                ShowId = showId,
                UserId = userId
            };

            bool success = false;

            myShowService.SaveCommit(newMyShow, out success);

            if (success)
            {
                BindWithShowId(showId);
            }
            else
            {
                var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "There was a problem adding this show. If this happens again, then please contact the administrator.");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
            }
        }

        private void Bind()
        {
            ResetPanels();

            if (!string.IsNullOrEmpty(Request.QueryString["showId"]))
            {
                BindWithShowId(new Guid(Request.QueryString["showId"]));
            }
        }

        private void BindWithShowId(Guid showId)
        {
            ShowId = showId;

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var show = showService.GetShow(showId);

            if (show != null)
            {
                SetShows(show.ShowDate.Value.Year);

                if (!ddlShows.Items.Contains(new ListItem(show.GetShowName(), show.ShowId.ToString())))
                {
                    phAddShow.Visible = true;
                    lnkAddMyShow.Text = string.Format("Click Here to add {0} to My Shows.", show.GetShowName());
                    phAddPicture.Visible = false;
                }
                else
                {
                    phAddShow.Visible = false;
                    phAddPicture.Visible = true;
                }

                ddlShows.SelectedValue = show.ShowId.ToString();
                hdnShowId.Value = show.ShowId.ToString();
            }
        }

        public void btnAddPicture_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlShows.SelectedValue))
            {
                ShowNotSelectedMessage();
                return;
            }

            Response.Redirect(LinkBuilder.AddPhotoLink(new Guid(ddlShows.SelectedValue), PhotoType.Poster, Request.Url.ToString()));
        }

        public void btnAddOther_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
            var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "To add posters please click a year and then choose a show.");
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
            phRemoveError.Visible = false;
            phRemoveSuccess.Visible = false;
        }
    }
}
