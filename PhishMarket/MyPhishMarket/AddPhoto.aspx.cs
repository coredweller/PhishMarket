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
            PhotoService photoService = new PhotoService(photoRepo);

            bool compiledSuccess = true;

            if (!uploadedFile.HasFile) return; //and throw error

            log.WriteLine("Submitted a picture with name " + uploadedFile.FileName);

            var fileExt = Path.GetExtension(uploadedFile.FileName.ToLower());
            Guid userID = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            string userName = Membership.GetUser(User.Identity.Name).UserName;
            var ticks = DateTime.Now.Ticks;
            Guid thumbImageId = Guid.NewGuid();
            Guid fullImageId = Guid.NewGuid();

            if (string.IsNullOrEmpty(fileExt) || !imageMediaFormats.HasExtension(fileExt))
             {
                 return;
                //ERROR MESSAGE
                //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "File was not uploaded." } };
            }

            //save file 
            var newFileName = userName + "-" + ticks + fileExt;
            log.WriteLine("thumb image New file name: " + newFileName);

            if (uploadedFile.ContentLength > 0)
            {

                //try to resize the image
                //var tmpResizeBuffer = new byte[posted.ContentLength];
                //posted.InputStream.Read(tmpResizeBuffer, 0, posted.ContentLength);
                //var thumbResizedBuffer = imageResizerService.ResizeImage(tmpResizeBuffer, new ThumbnailSize(), mediaFormat);
                //var fullResizedBuffer = imageResizerService.ResizeImage(tmpResizeBuffer, new FullImageSize(), mediaFormat);


                IPhoto thumbImage = null;
                IPhoto fullImage = null;
                //TODO: save fullImage

                using (var unitOfWork = TheCore.Infrastructure.UnitOfWork.Begin())
                {
                    Guid? showId = new Guid(hdnShowId.Value);



                    /*
                     * fullsize
                     */


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

                    bool success = false;
                    log.WriteLine("full image created, about to save full image");
                    photoService.Save(fullImage, out success);
                    log.WriteLine("Saved full image, success = " + success.ToString());

                    log.WriteLine("Compiled success = " + compiledSuccess.ToString());

                    if (!success)
                    {
                        //log.Error("The following validation conditions cased the offering image upload to fail during a call to CreateOfferingImageUpload: {0}".FormatWith(validationState.GetMessages()));
                        //ERROR MESSAGE NEEDED
                        //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "An error occurred while trying to save you upload. Please try again." } };
                    }
                    else
                    {
                        log.WriteLine("Compiled success was true, about to commit the unit of work");
                        unitOfWork.Commit();
                        log.WriteLine("Unit of work is committed");

                        bool valid = false;

                        var fullImageTemp = photoService.GetPhoto(fullImageId);

                        string destDir = Path.Combine(Request.PhysicalApplicationPath, "images\\Shows");
                        string destPath = Path.Combine(destDir, fullImageTemp.FileName);

                        log.WriteLine("Saving image to the path = " + destPath);
                        
                        log.WriteLine("Saving full image to the path");
                        uploadedFile.MoveTo(destPath, MoveToOptions.Overwrite);
                        log.WriteLine("Saving full image was valid = " + valid.ToString());

                        if (valid)
                        {
                            imgDisplayFull.ImageUrl = LinkBuilder.GetImageLinkByFileName(fullImageTemp.FileName);

                            fullImageTemp.Image = null;

                            DetermineTypeOfPhoto(fullImage, showId);
                        }
                        else
                        {
                            //FREAK OUT!!
                        }

                    }
                }
            }

            //ERROR MESSAGE NEEDED
            //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "No file uploaded." } };
        }

        public void DetermineTypeOfPhoto(IPhoto photo, Guid? showId)
        {
            switch ((PhotoType)(int.Parse(hdnPhotoType.Value)))
            {
                case PhotoType.Poster:
                    CreatePoster(photo, showId);
                    break;
                case PhotoType.TicketStub:
                    CreateTicketStub(photo, showId);
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

                bool success = false;
                posterService.Save(p, out success);

                MyShowPoster myShowPoster = new MyShowPoster
                {
                    CreatedDate = DateTime.Now,
                    MyShowId = myShowId,
                    MyShowPosterId = Guid.NewGuid(),
                    PosterId = posterId
                };

                bool secondSuccess = false;
                spService.Save(myShowPoster, out secondSuccess);

                final = success && secondSuccess;

                if (final)
                {
                    uow.Commit();
                    lnkEditPoster.NavigateUrl = LinkBuilder.EditPosterLink(posterId);
                    phEditPoster.Visible = true;
                }
            }

            return final;
        }

        public bool CreateTicketStub(IPhoto photo, Guid? showId)
        {
            bool final = false;
            var ticketStubId = Guid.NewGuid();

            var ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var spService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var myShowId = myShowService.GetMyShow(showId.Value, userId).MyShowId;

            TicketStub p = new TicketStub
            {
                CreatedDate = DateTime.Now,
                PhotoId = photo.PhotoId,
                TicketStubId = ticketStubId,
                Notes = photo.Notes,
                UserId = photo.UserId,
                ShowId = showId
                //Creator = txtCreator.Text,
                //Length = string.IsNullOrEmpty(txtLength.Text) ? 0 : double.Parse(txtLength.Text),
                //Width = string.IsNullOrEmpty(txtWidth.Text) ? 0 : double.Parse(txtWidth.Text),
                //Total = string.IsNullOrEmpty(txtTotal.Text) ? 0 : int.Parse(txtTotal.Text),
                //Number = string.IsNullOrEmpty(txtNumber.Text) ? 0 : int.Parse(txtNumber.Text),
                //Technique = txtTechnique.Text,
                //Title = txtTitle.Text
            };

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {

                bool success = false;
                ticketStubService.Save(p, out success);

                MyShowTicketStub myShowTicketStub = new MyShowTicketStub
                {
                    CreatedDate = DateTime.Now,
                    MyShowId = myShowId,
                    MyShowTicketStubId = Guid.NewGuid(),
                    TicketStubId = ticketStubId
                };

                bool secondSuccess = false;
                spService.Save(myShowTicketStub, out secondSuccess);

                final = success && secondSuccess;

                if (final)
                {
                    uow.Commit();
                    lnkEditTicketStub.NavigateUrl = LinkBuilder.EditTicketStubLink(ticketStubId);
                    phEditTicketStub.Visible = true;
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
                //Creator = txtCreator.Text,
                //Length = string.IsNullOrEmpty(txtLength.Text) ? 0 : double.Parse(txtLength.Text),
                //Width = string.IsNullOrEmpty(txtWidth.Text) ? 0 : double.Parse(txtWidth.Text),
                //Total = string.IsNullOrEmpty(txtTotal.Text) ? 0 : int.Parse(txtTotal.Text),
                //Number = string.IsNullOrEmpty(txtNumber.Text) ? 0 : int.Parse(txtNumber.Text),
                //Technique = txtTechnique.Text,
                //Title = txtTitle.Text
            };

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {

                bool success = false;
                artService.Save(p, out success);

                MyShowArt myShowArt = new MyShowArt
                {
                    CreatedDate = DateTime.Now,
                    MyShowId = myShowId,
                    MyShowArtId = Guid.NewGuid(),
                    ArtId = artId
                };

                bool secondSuccess = false;
                spService.Save(myShowArt, out secondSuccess);

                final = success && secondSuccess;

                if (final)
                {
                    uow.Commit();
                    lnkEditArt.NavigateUrl = LinkBuilder.EditArtLink(artId);
                    phEditArt.Visible = true;
                }
            }

            return final;
        }

        public void CreateCompanyImageUpload(HttpPostedFile fileData)
        {

        }
    }
}
