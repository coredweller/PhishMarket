using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCore.Infrastructure;
using TheCore.Services;
using TheCore.Helpers;
using TheCore.Repository;
using System.Text;

namespace PhishMarket.Handlers
{
    public class ShowHandler : BaseHandler
    {
        public ShowHandler() { Ioc.BuildUp(this); }

        public override void ProcessRequest(HttpContextBase context)
        {
            HttpRequestBase request = context.Request;
            var tourIdStr = request.QueryString["t"];
            HttpResponseBase response = context.Response;

            string final;

            if (tourIdStr != "-1")
            {
                var tourId = new Guid(tourIdStr);

                ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());
                var shows = showService.GetOfficialShows(tourId).ToList();


                if (shows != null && shows.Count > 0)
                {
                    var jsonifier = new BasicJSONifier("records", "ID", "Show", "-1", "Keep your current selection");

                    shows.ForEach(x =>
                    {
                        jsonifier.Add(x.ShowId.ToString(), x.GetShowName());
                    }
                        );

                    final = jsonifier.GetFinalizedJSON();
                }
                else
                {
                    var jsonifier = new BasicJSONifier("records", "ID", "Show");
                    jsonifier.Add("-1", "There are no shows for this tour");
                    final = jsonifier.GetFinalizedJSON();
                }
            }
            else
            {
                var jsonifier = new BasicJSONifier("records", "ID", "Show");
                jsonifier.Add("-1", "Please choose a tour");
                final = jsonifier.GetFinalizedJSON();
            }

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Write(final);
            //Response.End();
        }

        public override bool IsReusable { get { return true; } }
    }
}
