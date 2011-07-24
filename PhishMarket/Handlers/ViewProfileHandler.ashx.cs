using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace PhishMarket.Handlers
{
    public class ViewProfileHandler : BaseHandler
    {

        public override void ProcessRequest(HttpContextBase context)
        {
            HttpRequestBase request = context.Request;
            var userIdStr = request.QueryString["u"];
            HttpResponseBase response = context.Response;

            var final = string.Empty;

            if (EmptyNullUndefined(userIdStr))
            {
                final = GetNoImagesFound();

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.Write(final);
                response.End();
            }


        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
