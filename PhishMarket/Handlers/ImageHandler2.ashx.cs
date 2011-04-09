using System;
using System.Web;
using TheCore.Infrastructure;
using TheCore.Services;
using TheCore.Repository;
using System.IO;

namespace PhishMarket.Handlers
{

    public class ImageHandler2 : BaseHandler
    {
        public ImageHandler2() { Ioc.BuildUp(this); }

        public PhotoService PService { get; set; }

        public override void ProcessRequest(HttpContextBase context)
        {
            HttpRequestBase request = context.Request;
            string id = request.QueryString["id"];

            HttpResponseBase response = context.Response;

            Guid idG = new Guid(id);
            if (idG == Guid.Empty)
            { //invalid guid
                return;
            }

            PService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var tmpImage = PService.GetPhoto(idG);

            if (tmpImage == null) { return; }

            using (var msIn = new MemoryStream(tmpImage.Image))
            {
                response.ContentType = tmpImage.ContentType;
                msIn.WriteTo(response.OutputStream);
            }
        }

        public bool IsReusable { get { return true; } }
    }
}