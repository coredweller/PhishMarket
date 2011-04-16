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

            string final = "{\"records\":[{\"Show\":\"Keep your current selection\",\"ID\":\"-1\"},{\"Show\":\"12/27/2010 - DCU Center - Worcester, MA\",\"ID\":\"cb62c626-dda0-46bc-b4d9-6eed30cbcdf8\"},{\"Show\":\"12/28/2010 - DCU Center - Worcester, MA\",\"ID\":\"e852d885-7e74-4aca-903c-81bd67d31129\"},{\"Show\":\"12/30/2010 - Madison Square Garden - New York, NY\",\"ID\":\"15ce4a44-9833-4901-acf8-43cec36e1f06\"},{\"Show\":\"12/31/2010 - Madison Square Garden - New York, NY\",\"ID\":\"88314a34-07d1-464b-8c70-a88628c71036\"},{\"Show\":\"01/01/2011 - Madison Square Garden - New York, NY\",\"ID\":\"dc586a9a-497e-4257-a3d5-d9e326e12bfb\"}]}";

            //if (tourIdStr != "-1")
            //{
            //    var tourId = new Guid(tourIdStr);

            //    ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            //    var shows = showService.GetOfficialShows(tourId).ToList();


            //    if (shows != null && shows.Count > 0)
            //    {
            //        var jsonifier = new BasicJSONifier("records", "ID", "Show", "-1", "Keep your current selection");

            //        shows.ForEach(x =>
            //        {
            //            jsonifier.Add(x.ShowId.ToString(), x.GetShowName());
            //        }
            //            );

            //        final = jsonifier.GetFinalizedJSON();
            //    }
            //    else
            //    {
            //        var jsonifier = new BasicJSONifier("records", "ID", "Show");
            //        jsonifier.Add("-1", "There are no shows for this tour");
            //        final = jsonifier.GetFinalizedJSON();
            //    }
            //}
            //else
            //{
            //    var jsonifier = new BasicJSONifier("records", "ID", "Show");
            //    jsonifier.Add("-1", "Please choose a tour");
            //    final = jsonifier.GetFinalizedJSON();
            //}

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Write(final);
            //Response.End();
        }

        public override bool IsReusable { get { return true; } }
    }
}
