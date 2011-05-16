using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using PhishPond.Concrete;
using System.IO;
using TheCore.Interfaces;
using TheCore.Infrastructure.Images;

namespace PhishMarket.Admin
{
    public partial class CreateTicketStub : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (EmptyNullUndefined(ddlShows.SelectedValue) || fuPicture.PostedFile == null)
            {
                //Show Some error here eventually, 
                //  but for now, no NOOOOOO No error for you!
                return;
            }

            var ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());
            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var showId = new Guid(ddlShows.SelectedValue);

            Brih(showId);
        }

        public void Brih(Guid showId)
        {
            IImageMediaFormats imageMediaFormats = Ioc.GetInstance<IImageMediaFormats>();
            IPhotoRepository photoRepo = Ioc.GetInstance<IPhotoRepository>();

            ImageResizerService imageResizerService = new ImageResizerService();
            PhotoService photoService = new PhotoService(photoRepo);

            bool compiledSuccess = true;

            var posted = fuPicture.PostedFile;

            if (posted == null)
            {
                return;
                //ERROR MESSAGE
                //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "File was not uploaded." } };
            }

            var fileExt = Path.GetExtension(posted.FileName.ToLower());

            if (string.IsNullOrEmpty(fileExt))
            {
                //ERROR MESSAGE
                //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "File was not uploaded." } };
            }
            else
            { //check type
                if (imageMediaFormats.HasExtension(fileExt))
                {
                    var mediaFormat = imageMediaFormats.GetSpecByExtension(fileExt);

                    Guid userID = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
                    string userName = Membership.GetUser(User.Identity.Name).UserName;
                    var ticks = DateTime.Now.Ticks;
                    Guid thumbImageId = Guid.NewGuid();
                    Guid fullImageId = Guid.NewGuid();

                    //save file 
                    var newFileName = posted.FileName;

                    if (posted.ContentLength > 0)
                    {

                        //try to resize the image
                        var tmpResizeBuffer = new byte[posted.ContentLength];
                        posted.InputStream.Read(tmpResizeBuffer, 0, posted.ContentLength);
                        var thumbResizedBuffer = imageResizerService.ResizeImage(tmpResizeBuffer, new ThumbnailSize(), mediaFormat);
                        var fullResizedBuffer = imageResizerService.ResizeImage(tmpResizeBuffer, new FullImageSize(), mediaFormat);

                        IPhoto thumbImage = null;
                        IPhoto fullImage = null;
                        //TODO: save fullImage

                        using (var unitOfWork = TheCore.Infrastructure.UnitOfWork.Begin())
                        {
                            /*
                             * thumbnail
                             */
                            if (thumbImage == null)
                            {
                                thumbImage = new Photo
                                {
                                    PhotoId = thumbImageId,
                                    CreatedDate = DateTime.Now,
                                    UserId = userID,
                                    FileName = "thumb" + newFileName,
                                    ContentType = mediaFormat.ContentType,
                                    ContentLength = thumbResizedBuffer.Length,
                                    Image = new byte[thumbResizedBuffer.Length],
                                    Type = (short)PhotoType.TicketStub,
                                    Thumbnail = true,
                                    ShowId = showId
                                };
                            }
                            else
                            { //update entity
                                thumbImage.ContentType = mediaFormat.ContentType;
                                thumbImage.ContentLength = thumbResizedBuffer.Length;
                            }

                            //assign new image buffer
                            thumbImage.Image = thumbResizedBuffer;

                            bool success = false;
                            photoService.Save(thumbImage, mediaFormat, out success);

                            compiledSuccess = compiledSuccess && success;

                            /*
                             * fullsize
                             */
                            if (fullImage == null)
                            {
                                //newFileName = userName + "-" + DateTime.Now.Ticks + fileExt;

                                fullImage = new Photo
                                {
                                    PhotoId = fullImageId,
                                    CreatedDate = DateTime.Now,
                                    UserId = userID,
                                    FileName = newFileName,
                                    ContentType = mediaFormat.ContentType,
                                    ContentLength = fullResizedBuffer.Length,
                                    Image = new byte[fullResizedBuffer.Length],
                                    Type = (short)PhotoType.TicketStub,
                                    Thumbnail = false,
                                    ShowId = showId
                                };
                            }
                            else
                            { //update entity
                                fullImage.ContentType = mediaFormat.ContentType;
                                fullImage.ContentLength = fullResizedBuffer.Length;
                            }

                            //assign new image buffer
                            fullImage.Image = fullResizedBuffer;

                            photoService.Save(fullImage, mediaFormat, out success);

                            compiledSuccess = compiledSuccess && success;

                            if (!compiledSuccess)
                            {
                                //log.Error("The following validation conditions cased the offering image upload to fail during a call to CreateOfferingImageUpload: {0}".FormatWith(validationState.GetMessages()));
                                //ERROR MESSAGE NEEDED
                                //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "An error occurred while trying to save you upload. Please try again." } };
                            }
                            else
                            {
                                unitOfWork.Commit();

                                var thumbImageTemp = photoService.GetPhoto(thumbImageId);

                                string path = string.Format("{0}{1}", DefaultTicketStubImageLocation, thumbImageTemp.FileName);

                                bool valid = photoService.SaveAs(thumbImageTemp, HttpContext.Current.Server.MapPath(path));

                                if (valid)
                                {
                                    var fullImageTemp = photoService.GetPhoto(fullImageId);

                                    path = string.Format("{0}{1}", DefaultTicketStubImageLocation, fullImageTemp.FileName);

                                    valid = photoService.SaveAs(fullImageTemp, HttpContext.Current.Server.MapPath(path));

                                    if (valid)
                                    {
                                        //imgDisplayThumb.ImageUrl = LinkBuilder.GetImageLink(thumbImageId);
                                        imgTheImage.ImageUrl = LinkBuilder.GetImageLink(fullImageId);

                                        CreateTicketStubNow(fullImage, showId);

                                        //Here I should delete the image from the database
                                        //  Keep the row but get rid of the image bytes to save space
                                        //    But that is for another day
                                    }
                                    else
                                    {
                                        //FREAK OUT!!
                                    }
                                }
                            }
                        }
                    }

                    //ERROR MESSAGE NEEDED
                    //return new FileUploadJsonResult { Data = new { Src = Url.TempImages(tempId), ErrorMessage = "" } };

                }
                else
                {
                    //ERROR MESSAGE NEEDED
                    //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "The image you are trying to upload is listed as an invalid image type.  Please use the approved types:{0}".FormatWith(_imageMediaFormats.ImageFormatSpecs.ToDebugString(",")) } };
                }

            }

            //ERROR MESSAGE NEEDED
            //return new FileUploadJsonResult { Data = new { Src = "", ErrorMessage = "No file uploaded." } };
        }

        public bool CreateTicketStubNow(IPhoto photo, Guid? showId)
        {
            bool final = false;
            var ticketStubId = Guid.NewGuid();

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                var ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());
                var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
                var spService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());

                var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
                //var myShowId = myShowService.GetMyShow(showId.Value, userId).MyShowId;

                TicketStub p = new TicketStub
                {
                    CreatedDate = DateTime.Now,
                    PhotoId = photo.PhotoId,
                    TicketStubId = ticketStubId,
                    Notes = photo.Notes,
                    UserId = photo.UserId,
                    ShowId = showId
                };

                bool success = false;
                ticketStubService.Save(p, out success);

                if (success)
                {
                    uow.Commit();
                }
            }

            return final;
        }

        private void Bind()
        {
            var tourService = new TourService(Ioc.GetInstance<ITourRepository>());

            var tours = tourService.GetAllToursDescending();

            ddlTours.Items.AddRange((from t in tours
                                     select new ListItem(t.TourName, t.TourId.ToString())).ToArray());

            var noneItem = new ListItem("None", "none");

            ddlTours.Items.Insert(0, noneItem);

            noneItem.Selected = true;
        }

        private void SetShows(Guid tourId)
        {

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var shows = showService.GetOfficialShows(tourId);

            ddlShows.Items.Clear();

            ddlShows.Items.AddRange((from s in shows
                                     select new ListItem(s.GetShowName(), s.ShowId.ToString())).ToArray());
        }

        public void ddlTours_SelectedIndexChanged(object sender, EventArgs e)
        {
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

    }
}
