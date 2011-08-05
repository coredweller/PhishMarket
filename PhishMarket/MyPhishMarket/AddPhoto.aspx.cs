using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using TheCore.Services;
using TheCore.Infrastructure.Images;
using TheCore.Infrastructure;
using TheCore.Interfaces;
using System.Web.Security;
using PhishPond.Concrete;
using TheCore.Repository;
using Brettle.Web.NeatUpload;
using TheCore.Helpers;
using TheCore.Validators;

namespace PhishMarket.MyPhishMarket
{
    public partial class AddPhoto : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Add a Photo");

            if (!IsPostBack)
            {
                if (Request.QueryString["showId"] == null || Request.QueryString["type"] == null)
                    Response.Redirect(LinkBuilder.DashboardLink());

                var showId = new Guid(Request.QueryString["showId"]);
                var photoType = (PhotoType)(int.Parse(Request.QueryString["type"]));

                switch (photoType)
                {
                    case PhotoType.Poster:
                        phPosterExtras.Visible = true;
                        break;
                    case PhotoType.TicketStub:
                        break;
                    case PhotoType.Art:
                        break;
                }

                lnkReturn.NavigateUrl = Request.QueryString["returnUrl"];

                hdnShowId.Value = showId.ToString();
                hdnPhotoType.Value = (int.Parse(Request.QueryString["type"]).ToString());

                ddlQuality.Items.AddRange(GetDropDownFromEnum(typeof(PhotoQuality), 1, "Please choose a quality"));
            }
        }

        private void ShowError(string error)
        {
            phError.Visible = true;
            lblError.Text = error;
        }

        private void ResetPanels()
        {
            phEditArt.Visible = false;
            phEditPoster.Visible = false;
            phEditTicketStub.Visible = false;
            phError.Visible = false;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();

            bool compiledSuccess = true;

            if (uploadedFiles.Files.Count() <= 0)
            {
                var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "Please choose a file to upload.");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
                return;
            }

            var errorMessage = string.Empty;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                foreach (var file in uploadedFiles.Files)
                {
                    var validator = ProcessFile(file);

                    compiledSuccess = compiledSuccess && validator.Success;

                    if (!compiledSuccess)
                    {
                        errorMessage = validator.Message;
                        break;
                    }
                }

                if (!compiledSuccess)
                {
                    var scriptHelper3 = new ScriptHelper("ErrorAlert", "alertDiv", errorMessage + "Please try again without that image.");
                    Page.RegisterStartupScript(scriptHelper3.ScriptName, scriptHelper3.GetFatalScript());
                    return;
                }

                var scriptHelper2 = new ScriptHelper("SuccessAlert", "alertDiv", "You have successfully saved the images");
                Page.RegisterStartupScript(scriptHelper2.ScriptName, scriptHelper2.GetSuccessScript());

                uow.Commit();
            }
        }

        private ImageItemValidator ProcessFile(UploadedFile file)
        {
            var imageMediaFormats = Ioc.GetInstance<IImageMediaFormats>();
            var photoRepo = Ioc.GetInstance<IPhotoRepository>();

            log.WriteLine("Submitted a picture with name " + file.FileName);

            var fileExt = Path.GetExtension(file.FileName.ToLower());
            var userID = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var userName = Membership.GetUser(User.Identity.Name).UserName;
            var ticks = DateTime.Now.Ticks;
            var thumbImageId = Guid.NewGuid();
            var fullImageId = Guid.NewGuid();

            if (string.IsNullOrEmpty(fileExt) || !imageMediaFormats.HasExtension(fileExt))
            {
                return new ImageItemValidator(false, file.FileName + " does not have a valid extension. ");
            }

            var newFileName = userName + "-" + ticks + fileExt;
            log.WriteLine("thumb image New file name: " + newFileName);

            if (file.ContentLength > 0)
            {
                int intContentLength;
                if (!int.TryParse(file.ContentLength.ToString(), out intContentLength))
                {
                    return new ImageItemValidator(false, file.FileName + " is too large. ");
                }

                IPhoto fullImage = null;

                Guid? showId = new Guid(hdnShowId.Value);

                var notes = Request.Form["txtNotes"].ToString();

                log.WriteLine("About to create full image");
                fullImage = new Photo
                {
                    PhotoId = fullImageId,
                    CreatedDate = DateTime.Now,
                    UserId = userID,
                    FileName = newFileName,
                    ContentType = file.ContentType,
                    Type = short.Parse(hdnPhotoType.Value),
                    NickName = txtNickName.Text.Trim(),
                    Notes = notes,
                    Quality = short.Parse(ddlQuality.SelectedValue),
                    Thumbnail = false,
                    ShowId = showId
                };

                string destDir = Path.Combine(Request.PhysicalApplicationPath, "images\\Shows");
                string destPath = Path.Combine(destDir, newFileName);

                log.WriteLine("Saving image to the path = " + destPath);

                file.MoveTo(destPath, MoveToOptions.Overwrite);

                log.WriteLine("Image saved to the path and now determining what type of photo it is");
                DetermineTypeOfPhoto(fullImage, showId);

                //imgDisplayFull.ImageUrl = LinkBuilder.GetImageLinkByFileName(newFileName);


                return new ImageItemValidator(true, "You have successfully saved " + file.FileName);
            }

            return new ImageItemValidator(false, "There was an error processing " + file.FileName + ". ");
        }

        public bool DetermineTypeOfPhoto(IPhoto photo, Guid? showId)
        {
            switch ((PhotoType)(int.Parse(hdnPhotoType.Value)))
            {
                case PhotoType.Poster:
                    return CreatePoster(photo, showId);
                case PhotoType.Art:
                    return CreateArt(photo, showId);
            }

            return false;
        }

        public bool CreatePoster(IPhoto photo, Guid? showId)
        {
            bool final = false;
            var posterId = Guid.NewGuid();

            var posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var spService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var myShowId = myShowService.GetMyShow(showId.Value, userId).MyShowId;

            var date = DateTime.UtcNow;

            Poster p = new Poster
            {
                CreatedDate = DateTime.Now,
                PhotoId = photo.PhotoId,
                PosterId = posterId,
                Notes = photo.Notes,
                UserId = photo.UserId,
                Creator = txtCreator.Text,
                Length = string.IsNullOrEmpty(txtLength.Text) ? 0 : double.Parse(txtLength.Text),
                Width = string.IsNullOrEmpty(txtWidth.Text) ? 0 : double.Parse(txtWidth.Text),
                Total = string.IsNullOrEmpty(txtTotal.Text) ? 0 : int.Parse(txtTotal.Text),
                Number = string.IsNullOrEmpty(txtNumber.Text) ? 0 : int.Parse(txtNumber.Text),
                Technique = txtTechnique.Text,
                Title = txtTitle.Text,
                ShowId = showId
            };

            var combinedSuccess = true;
            bool success = false;

            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());
            photoService.Save(photo, out success);

            combinedSuccess = combinedSuccess && success;

            posterService.Save(p, out success);

            combinedSuccess = combinedSuccess && success;

            var myShowPoster = new MyShowPoster
            {
                CreatedDate = date,
                UpdatedDate = date,
                MyShowId = myShowId,
                MyShowPosterId = Guid.NewGuid(),
                PosterId = posterId
            };

            spService.Save(myShowPoster, out success);

            combinedSuccess = combinedSuccess && success;

            return combinedSuccess;
        }

        public bool CreateArt(IPhoto photo, Guid? showId)
        {
            bool final = false;
            var artId = Guid.NewGuid();

            var artService = new ArtService(Ioc.GetInstance<IArtRepository>());
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var spService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var myShowId = myShowService.GetMyShow(showId.Value, userId).MyShowId;

            var date = DateTime.UtcNow;

            Art p = new Art
            {
                CreatedDate = date,
                UpdatedDate = date,
                PhotoId = photo.PhotoId,
                ArtId = artId,
                Notes = photo.Notes,
                UserId = photo.UserId,
                ShowId = showId
            };


            var combinedSuccess = true;
            bool success = false;

            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());
            photoService.Save(photo, out success);

            combinedSuccess = combinedSuccess && success;

            artService.Save(p, out success);

            combinedSuccess = combinedSuccess && success;

            var myShowArt = new MyShowArt
            {
                CreatedDate = date,
                UpdatedDate = date,
                MyShowId = myShowId,
                MyShowArtId = Guid.NewGuid(),
                ArtId = artId
            };

            spService.Save(myShowArt, out success);

            combinedSuccess = combinedSuccess && success;

            return combinedSuccess;
        }
    }
}
