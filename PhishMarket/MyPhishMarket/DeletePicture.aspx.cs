using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;

namespace PhishMarket.MyPhishMarket
{
    public partial class DeletePicture : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var pictureIdStr = Request.QueryString["picid"];
            var posterIdStr = Request.QueryString["posid"];

            hdnShowId.Value = Request.QueryString["showId"];

            if (!string.IsNullOrEmpty(pictureIdStr))
            {
                var pictureId = new Guid(pictureIdStr);
                BindPicture(pictureId);
                return;
            }
            else if (!string.IsNullOrEmpty(posterIdStr))
            {
                var posterId = new Guid(posterIdStr);
                BindPoster(posterId);
                return;
            }

            Response.Redirect(LinkBuilder.DashboardLink());
            
        }

        public void btnEditPicture_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnId.Value))
            {
                var type = hdnId.Value.Split('=')[0];
                var id = hdnId.Value.Split('=')[1];

                switch (type)
                {
                    case "picture":
                        var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());
                        var myShowArt = myShowArtService.GetMyShowArt(new Guid(id));
                        Response.Redirect(LinkBuilder.EditArtLink(myShowArt.ArtId));
                        break;
                    case "poster":
                        var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());
                        var myShowPoster = myShowPosterService.GetMyShowPoster(new Guid(id));
                        Response.Redirect(LinkBuilder.EditPosterLink(myShowPoster.PosterId));
                        break;
                }
            }
        }

        public void btnNo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["picid"]))
            {
                Response.Redirect(LinkBuilder.MyPicturesLink(new Guid(hdnShowId.Value)));
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["posid"]))
            {
                Response.Redirect(LinkBuilder.MyPostersLink(new Guid(hdnShowId.Value)));
            }
        }

        public void btnYes_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnId.Value))
            {
                var type = hdnId.Value.Split('=')[0];
                var id = hdnId.Value.Split('=')[1];

                switch (type)
                {
                    case "picture": DeletePic(id);
                        break;
                    case "poster": DeletePoster(id);
                        break;
                }
            }
        }

        private void DeletePoster(string posterIdStr)
        {
            var posterId = new Guid(posterIdStr);

            var myShowPoster = GetPoster(posterId);
            var myShowPosterId = myShowPoster.MyShowPosterId.ToString();
            var photoId = myShowPoster.Poster.Photo.PhotoId.ToString();
            var filename = myShowPoster.Poster.Photo.FileName.ToString();

            var posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());
            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                var photo = myShowPoster.Poster.Photo;
                var poster = myShowPoster.Poster;

                photoService.Delete(photo);
                posterService.Delete(poster);
                myShowPosterService.Delete(myShowPoster);

                uow.Commit();
            }

            log.WriteLine("Deleted myShowPoster Id: " + myShowPosterId);
            log.WriteLine("Deleted photo Id: " + photoId + "and filename: " + filename);
            log.WriteLine("Deleted picture Id: " + posterId);

            Response.Redirect(LinkBuilder.MyPostersLink(new Guid(hdnShowId.Value)));

        }

        private void DeletePic(string pictureIdStr)
        {
            var pictureId = new Guid(pictureIdStr);

            var myShowArt = GetPicture(pictureId);
            var myShowArtId = myShowArt.MyShowArtId.ToString();
            var photoId = myShowArt.Art.Photo.PhotoId.ToString();
            var filename = myShowArt.Art.Photo.FileName.ToString();

            var artService = new ArtService(Ioc.GetInstance<IArtRepository>());
            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());
            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                photoService.Delete(myShowArt.Art.Photo);
                artService.Delete(myShowArt.Art);
                myShowArtService.Delete(myShowArt);

                uow.Commit();
            }

            log.WriteLine("Deleted myShowArt Id: " + myShowArtId);
            log.WriteLine("Deleted photo Id: " + photoId + "and filename: " + filename);
            log.WriteLine("Deleted picture Id: " + pictureId);

            Response.Redirect(LinkBuilder.MyPicturesLink(new Guid(hdnShowId.Value)));
        }

        private void BindPicture(Guid pictureId)
        {
            var myShowArt = GetPicture(pictureId);

            imgImage.ImageUrl = LinkBuilder.GetImageLinkByFileName(myShowArt.Art.Photo.FileName);

            hdnId.Value = "picture=" + myShowArt.MyShowArtId.ToString();
        }

        private MyShowArt GetPicture(Guid pictureId)
        {
            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            return (MyShowArt)myShowArtService.GetMyShowArt(pictureId);
        }

        private void BindPoster(Guid posterId)
        {
            var myShowPoster = GetPoster(posterId);

            imgImage.ImageUrl = LinkBuilder.GetImageLinkByFileName(myShowPoster.Poster.Photo.FileName);

            hdnId.Value = "poster=" + myShowPoster.MyShowPosterId.ToString();
        }

        private MyShowPoster GetPoster(Guid posterId)
        {
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());

            return (MyShowPoster)myShowPosterService.GetMyShowPoster(posterId);
        }
    }
}
