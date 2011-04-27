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
            Response.Redirect(LinkBuilder.MyPicturesLink(new Guid(hdnShowId.Value)));
        }

        public void btnYes_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnId.Value))
            {
                var pictureId = new Guid(hdnId.Value);

                var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

                var myShowArt = (MyShowArt)myShowArtService.GetMyShowArt(pictureId);

                //Since MyShowArt allows for you to get the Art and Photo objects
                //  Consider in the future deleting those when the user deletes the MyShowArt
                //  Not handling it now b/c it might be rare and complicated

                myShowArtService.DeleteCommit(myShowArt);

                log.Write("Deleted myShowArt Id: " + myShowArt.MyShowArtId.ToString());

                Response.Redirect(LinkBuilder.MyPicturesLink(new Guid(hdnShowId.Value)));
            }
        }

        private void BindPicture(Guid pictureId)
        {
            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            var myShowArt = (MyShowArt)myShowArtService.GetMyShowArt(pictureId);

            imgImage.ImageUrl = LinkBuilder.GetImageLink(myShowArt.Art.PhotoId);

            hdnId.Value = myShowArt.MyShowArtId.ToString();
        }

        private void BindPoster(Guid posterId)
        {

        }
    }
}
