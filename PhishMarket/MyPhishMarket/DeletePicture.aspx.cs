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

            //Since MyShowPoster allows for you to get the Poster and Photo objects
            //  Consider in the future deleting those when the user deletes the MyShowPoster
            //  Not handling it now b/c it might be rare and complicated

            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());
            myShowPosterService.DeleteCommit(myShowPoster);

            log.Write("Deleted myShowPoster Id: " + myShowPoster.MyShowPosterId.ToString());

            Response.Redirect(LinkBuilder.MyPostersLink(new Guid(hdnShowId.Value)));

        }

        private void DeletePic(string pictureIdStr)
        {
            var pictureId = new Guid(pictureIdStr);

            var myShowArt = GetPicture(pictureId);

            //Since MyShowArt allows for you to get the Art and Photo objects
            //  Consider in the future deleting those when the user deletes the MyShowArt
            //  Not handling it now b/c it might be rare and complicated

            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());
            myShowArtService.DeleteCommit(myShowArt);

            log.Write("Deleted myShowArt Id: " + myShowArt.MyShowArtId.ToString());

            Response.Redirect(LinkBuilder.MyPicturesLink(new Guid(hdnShowId.Value)));
        }

        private void BindPicture(Guid pictureId)
        {
            var myShowArt = GetPicture(pictureId);

            imgImage.ImageUrl = LinkBuilder.GetImageLink(myShowArt.Art.PhotoId);

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

            imgImage.ImageUrl = LinkBuilder.GetImageLink(myShowPoster.Poster.PhotoId);

            hdnId.Value = "poster=" + myShowPoster.MyShowPosterId.ToString();
        }

        private MyShowPoster GetPoster(Guid posterId)
        {
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());

            return (MyShowPoster)myShowPosterService.GetMyShowPoster(posterId);
        }
    }
}
