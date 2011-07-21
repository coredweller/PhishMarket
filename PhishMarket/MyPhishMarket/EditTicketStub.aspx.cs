using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.MyPhishMarket
{
    public partial class EditTicketStub : PhishMarketBasePage
    {
        TicketStubService ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Edit your Ticket Stub");

            if (!IsPostBack)
            {
                Bind();
                BindTicketStub();
            }
        }

        private void BindTicketStub()
        {
            var ticketStubId = string.IsNullOrEmpty(Request.QueryString["id"]) ? EmptyGuid : new Guid(Request.QueryString["id"]);

            if (ticketStubId == EmptyGuid)
            {
                ShowError("There was an error with your request please go back and try again.");
                phMain.Visible = false;
                return;
            }

            hdnId.Value = ticketStubId.ToString();

            var ticketStub = ticketStubService.GetTicketStub(ticketStubId);

            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var photo = photoService.GetPhoto(ticketStub.PhotoId);

            txtNotes.Text = ticketStub.Notes;
            ddlShow.SelectedValue = photo.ShowId.ToString();
            imgDisplayFull.ImageUrl = LinkBuilder.GetImageLinkByFileName(photo.FileName);
            chkPTBM.Checked = ticketStub.Original;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();

            var ticketStub = ticketStubService.GetTicketStub(new Guid(hdnId.Value));

            Guid? showId = null;
            if (ddlShow.SelectedValue != "0")
                showId = new Guid(ddlShow.SelectedValue);

            bool success = false;

            if (ticketStub != null)
            {
                using (IUnitOfWork uow = UnitOfWork.Begin())
                {
                    ticketStub.Notes = txtNotes.Text;
                    ticketStub.Original = chkPTBM.Checked;
                    ticketStub.ShowId = showId;
                    ticketStub.UpdatedDate = DateTime.Now;

                    uow.Commit();

                    success = true;

                    phSuccess.Visible = true;
                }
            }

            if (success)
                phSuccess.Visible = true;
            else
                ShowError("There was an error editing the ticket stub.");

        }

        private void ShowError(string message)
        {
            phError.Visible = true;
            lblError.Text = message;
        }

        private void ResetPanels()
        {
            phError.Visible = false;
            phSuccess.Visible = false;
        }

        private void Bind()
        {
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var shows = showService.GetAllShows();

            foreach (var show in shows)
            {
                ddlShow.Items.Add(new ListItem(show.GetShowName(), show.ShowId.ToString()));
            }

            var item = new ListItem("None", "0");

            ddlShow.Items.Insert(0, item);

            item.Selected = true;
        }
    }
}
