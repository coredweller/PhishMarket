//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

using System.Net;
using System.Web;
using TheCore.Helpers;


namespace PhishMarket.Handlers
{
    public abstract class BaseHandler : IHttpHandler
    {
        public virtual bool IsReusable { get { return false; } }

        protected const string ShowImagesFolder = "/images/Shows/";
        protected const string TicketStubImagesFolder = "/images/TicketStubs/";

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequest(new HttpContextWrapper(context));
        }

        public abstract void ProcessRequest(HttpContextBase context);

        public static bool HandleIfNotModified(HttpContextBase context, string etag)
        {
            bool notModified = false;

            if (!string.IsNullOrEmpty(etag))
            {
                string ifNoneMatch = context.Request.Headers["If-None-Match"];

                if (!string.IsNullOrEmpty(ifNoneMatch) && (string.CompareOrdinal(ifNoneMatch, etag) == 0))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotModified;
                    notModified = true;
                }
            }

            return notModified;
        }

        protected string NoImagesLocation
        {
            get { return System.Configuration.ConfigurationManager.AppSettings.Get("SlideServiceNoImagesFoundLocation") ?? string.Empty; }
        }

        protected bool EmptyNullUndefined(string brih)
        {
            if (string.IsNullOrEmpty(brih) || brih == "undefined")
                return true;

            return false;
        }

        protected string GetNoImagesFound()
        {
            var json = new ImageJSONifier("records");
            var loc = NoImagesLocation;
            json.Add(new ImageItem { Image = loc, Thumb = loc, Title = "No Images Found", Description = "Upload posters or pictures of your own so everyone can see what this show was like" });
            return json.GetFinalizedJSON();
        }

        //protected static UrlHelper CreateUrlHelper(HttpContextBase context)
        //{
        //    RequestContext viewContext = new RequestContext(context, new RouteData());

        //    return new UrlHelper(viewContext);
        //}
    }
}
