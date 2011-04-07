using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.MyPhishMarket
{
    public partial class EditArt : PhishMarketBasePage
    {
        ArtService artService = new ArtService(Ioc.GetInstance<IArtRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Edit your Picture");

            if (!IsPostBack)
            {
                Bind();
                BindArt();
            }
        }

        private void BindArt()
        {
            var artId = string.IsNullOrEmpty(Request.QueryString["id"]) ? new Guid() : new Guid(Request.QueryString["id"]);

            if (artId == EmptyGuid)
            {
                ShowError("There was an error with your request please go back and try again.");
                phMain.Visible = false;
                return;
            }

            hdnId.Value = artId.ToString();

            var art = artService.GetArt(artId);

            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var photo = photoService.GetPhoto(art.PhotoId.Value);

            txtNotes.Text = art.Notes;
            ddlShow.SelectedValue = photo.ShowId.ToString();
            imgDisplayFull.ImageUrl = LinkBuilder.GetImageLink(art.PhotoId.Value);
            txtCreator.Text = art.Creator;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();

            var art = artService.GetArt(new Guid(hdnId.Value));

            Guid? showId = null;
            if (ddlShow.SelectedValue != "0")
                showId = new Guid(ddlShow.SelectedValue);

            bool success = false;

            if (art != null)
            {
                using (IUnitOfWork uow = UnitOfWork.Begin())
                {
                    art.Notes = txtNotes.Text;
                    art.ShowId = showId;
                    art.UpdatedDate = DateTime.Now;
                    art.Creator = txtCreator.Text;

                    uow.Commit();

                    success = true;

                    phSuccess.Visible = true;
                }
            }

            if (success)
                phSuccess.Visible = true;
            else
                ShowError("There was an error editing the art.");

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
