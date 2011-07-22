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

            IImageMediaFormats imageMediaFormats = Ioc.GetInstance<IImageMediaFormats>();
            IPhotoRepository photoRepo = Ioc.GetInstance<IPhotoRepository>();

            ImageResizerService imageResizerService = new ImageResizerService();
            
            bool compiledSuccess = true;

            if (!uploadedFile.HasFile)
            {
                var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "Please choose a file to upload.");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
                return;
            }

            log.WriteLine("Submitted a picture with name " + uploadedFile.FileName);

            var fileExt = Path.GetExtension(uploadedFile.FileName.ToLower());
            Guid userID = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            string userName = Membership.GetUser(User.Identity.Name).UserName;
            var ticks = DateTime.Now.Ticks;
            Guid thumbImageId = Guid.NewGuid();
            Guid fullImageId = Guid.NewGuid();

            if (string.IsNullOrEmpty(fileExt) || !imageMediaFormats.HasExtension(fileExt))
            {
                var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "This file does not have a valid extension.  Please enter a correct file.");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
                return;
            }

            var newFileName = userName + "-" + ticks + fileExt;
            log.WriteLine("thumb image New file name: " + newFileName);

            if (uploadedFile.ContentLength > 0)
            {
                int intContentLength;
                if(!int.TryParse(uploadedFile.ContentLength.ToString(), out intContentLength))
                {
                    var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "This file is too large.  Please try another photo.");
                    Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
                    return;
                }
               


                IPhoto fullImage = null;

                Guid? showId = new Guid(hdnShowId.Value);

                log.WriteLine("About to create full image");
                fullImage = new Photo
                {
                    PhotoId = fullImageId,
                    CreatedDate = DateTime.Now,
                    UserId = userID,
                    FileName = newFileName,
                    ContentType = uploadedFile.ContentType,
                    Type = short.Parse(hdnPhotoType.Value),
                    NickName = txtNickName.Text.Trim(),
                    Notes = txtNotes.Text.Trim(),
                    Quality = short.Parse(ddlQuality.SelectedValue),
                    Thumbnail = false,
                    ShowId = showId
                };

                string destDir = Path.Combine(Request.PhysicalApplicationPath, "images\\Shows");
                string destPath = Path.Combine(destDir, newFileName);
                
                log.WriteLine("Saving image to the path = " + destPath);

                var phishMarketInputFile = new PhishMarketInputFile(uploadedFile);

                uploadedFile.MoveTo(destPath, MoveToOptions.Overwrite);

                log.WriteLine("Image saved to the path and now determining what type of photo it is");
                DetermineTypeOfPhoto(fullImage, showId);

                imgDisplayFull.ImageUrl = LinkBuilder.GetImageLinkByFileName(newFileName);

                var scriptHelper2 = new ScriptHelper("SuccessAlert", "alertDiv", "You have successfully saved the image.");
                Page.RegisterStartupScript(scriptHelper2.ScriptName, scriptHelper2.GetSuccessScript());
                return;
            }

            var scriptHelper3 = new ScriptHelper("ErrorAlert", "alertDiv", "There was an error and the picture could not be uploaded.");
            Page.RegisterStartupScript(scriptHelper3.ScriptName, scriptHelper3.GetFatalScript());
        }

        public void DetermineTypeOfPhoto(IPhoto photo, Guid? showId)
        {
            switch ((PhotoType)(int.Parse(hdnPhotoType.Value)))
            {
                case PhotoType.Poster:
                    CreatePoster(photo, showId);
                    break;
                case PhotoType.Art:
                    CreateArt(photo, showId);
                    break;
            }
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

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                var combinedSuccess = true;
                bool success = false;

                PhotoService photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());
                photoService.Save(photo, out success);

                combinedSuccess = combinedSuccess && success;
                
                posterService.Save(p, out success);

                combinedSuccess = combinedSuccess && success;

                MyShowPoster myShowPoster = new MyShowPoster
                {
                    CreatedDate = DateTime.Now,
                    MyShowId = myShowId,
                    MyShowPosterId = Guid.NewGuid(),
                    PosterId = posterId
                };

                spService.Save(myShowPoster, out success);

                combinedSuccess = combinedSuccess && success;

                final = combinedSuccess;

                if (final)
                {
                    uow.Commit();
                    lnkEditPoster.NavigateUrl = LinkBuilder.EditPosterLink(posterId);
                    phEditPoster.Visible = true;
                }
            }

            return final;
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

            Art p = new Art
            {
                CreatedDate = DateTime.Now,
                PhotoId = photo.PhotoId,
                ArtId = artId,
                Notes = photo.Notes,
                UserId = photo.UserId,
                ShowId = showId
            };

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                var combinedSuccess = true;
                bool success = false;

                PhotoService photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());
                photoService.Save(photo, out success);

                combinedSuccess = combinedSuccess && success;

                artService.Save(p, out success);

                combinedSuccess = combinedSuccess && success;

                MyShowArt myShowArt = new MyShowArt
                {
                    CreatedDate = DateTime.Now,
                    MyShowId = myShowId,
                    MyShowArtId = Guid.NewGuid(),
                    ArtId = artId
                };

                spService.Save(myShowArt, out success);

                combinedSuccess = combinedSuccess && success;

                final = combinedSuccess;

                if (final)
                {
                    uow.Commit();
                    lnkEditArt.NavigateUrl = LinkBuilder.EditArtLink(artId);
                    phEditArt.Visible = true;
                }
            }

            return final;
        }
    }
}
