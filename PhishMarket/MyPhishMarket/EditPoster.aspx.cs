using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Repository;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using TheCore.Interfaces;

namespace PhishMarket.MyPhishMarket
{
    public partial class EditPoster : PhishMarketBasePage
    {
        PosterService posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Edit your Poster");

            if (!IsPostBack)
            {
                Bind();
                BindPoster();
            }
        }

        private void BindPoster()
        {
            var posterId = string.IsNullOrEmpty(Request.QueryString["id"]) ? new Guid() : new Guid(Request.QueryString["id"]);

            if (posterId == EmptyGuid)
            {
                ShowError("There was an error with your request please go back and try again.");
                phMain.Visible = false;
                return;
            }

            hdnId.Value = posterId.ToString();

            var poster = posterService.GetPoster(posterId);

            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var photo = photoService.GetPhoto(poster.PhotoId);

            imgDisplayFull.ImageUrl = LinkBuilder.GetImageLink(poster.PhotoId);

            ddlShow.SelectedValue = photo.ShowId.ToString();

            txtCreator.Text = poster.Creator;
            txtLength.Text = poster.Length.ToString();
            txtNotes.Text = poster.Notes;
            txtNumber.Text = poster.Number.ToString();
            txtReleaseDate.Text = poster.ReleaseDate != null ? poster.ReleaseDate.Value.ToString("MM/dd/yyyy") : string.Empty;
            txtTechnique.Text = poster.Technique;
            txtTitle.Text = poster.Title;
            txtTotal.Text = poster.Total.ToString();
            txtWidth.Text = poster.Width.ToString();
            ddlStatus.SelectedValue = poster.Status.ToString();
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

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();

            var poster = posterService.GetPoster(new Guid(hdnId.Value));

            Guid? showId = null;
            if (ddlShow.SelectedValue != "0")
                showId = new Guid(ddlShow.SelectedValue);

            bool success = false;

            if (poster != null)
            {
                using (IUnitOfWork uow = UnitOfWork.Begin())
                {
                    var p = ValidatePoster();

                    if (p != null)
                    {
                        poster.Creator = txtCreator.Text;
                        poster.Length = p.Length;
                        poster.Notes = txtNotes.Text;
                        poster.Number = p.Number;
                        poster.ReleaseDate = p.ReleaseDate;
                        poster.Status = p.Status;
                        poster.Technique = txtTechnique.Text;
                        poster.Title = txtTitle.Text;
                        poster.Total = p.Total;
                        poster.UpdatedDate = DateTime.Now;
                        poster.Width = p.Width;

                        uow.Commit();

                        success = true;

                        phSuccess.Visible = true;
                    }
                }   
            }

            if (success)
                phSuccess.Visible = true;
            else
                ShowError("There was an error editing the poster.");
        }

        private Poster ValidatePoster()
        {
            try
            {
                double? length = string.IsNullOrEmpty(txtLength.Text) ? null : (double?)double.Parse(txtLength.Text);
                double? width = string.IsNullOrEmpty(txtWidth.Text) ? null : (double?)double.Parse(txtWidth.Text);

                int? number = string.IsNullOrEmpty(txtNumber.Text) ? null : (int?)int.Parse(txtNumber.Text);
                int? total = string.IsNullOrEmpty(txtTotal.Text) ? null : (int?)int.Parse(txtTotal.Text);

                DateTime? releaseDate = string.IsNullOrEmpty(txtReleaseDate.Text) ? null : (DateTime?)DateTime.Parse(txtReleaseDate.Text);

                PosterStatus status = (PosterStatus)Enum.Parse(typeof(PosterStatus), ddlStatus.SelectedValue);

                return new Poster
                {
                    Length = length,
                    Width = width,
                    Number = number,
                    ReleaseDate = releaseDate,
                    Status = (short?)status,
                    Total = total
                };
            }
            catch (FormatException fex)
            {
                return null;
            }
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

            ddlStatus.Items.AddRange(GetDropDownFromEnum(typeof(PosterStatus), 0, "Select a Status"));
        }
    }
}
