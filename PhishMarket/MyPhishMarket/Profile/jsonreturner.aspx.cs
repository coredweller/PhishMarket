using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TheCore.Services;
using TheCore.Repository;
using TheCore.Infrastructure;
using TheCore.Helpers;

namespace PhishMarket.MyPhishMarket.ProfilePages
{
    public partial class jsonreturner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var tourIdStr = Request.QueryString["t"];
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

            Response.ContentType = "application/json";
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(final);
            Response.End();
        }

        //private StringBuilder CreateJson(StringBuilder sb, string id, string text)
        //{
        //    sb.Append("{");
        //    sb.AppendFormat("\"Show\":\"{0}\",", text);
        //    sb.AppendFormat("\"ID\":\"{0}\"", id);
        //    sb.Append("},");

        //    return sb;
        //}

        //private void BackUpBitch()
        //{
        //    var tourIdStr = Request.QueryString["t"];
        //    StringBuilder sb = new StringBuilder();

        //    if (tourIdStr != "-1")
        //    {
        //        var tourId = new Guid(tourIdStr);

        //        ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());
        //        var shows = showService.GetOfficialShows(tourId).ToList();
        //        var jsonifier = new BasicJSONifier("records", "ID", "Show", "-1", "Keep your current selection");

        //        sb.Append("{\"records\":");

        //        if (shows != null && shows.Count > 0)
        //        {
        //            sb.Append("[");

        //            sb.Append("{");
        //            sb.AppendFormat("\"Show\":\"{0}\",", "Keep your current selection");
        //            sb.AppendFormat("\"ID\":\"{0}\"", "-1");
        //            sb.Append("},");

        //            shows.ForEach(x =>
        //            {
        //                CreateJson(sb, x.ShowId.ToString(), x.GetShowName());
        //                //sb.Append("{");
        //                //sb.AppendFormat("\"Show\":\"{0}\",", x.GetShowName());
        //                //sb.AppendFormat("\"ID\":\"{0}\"", x.ShowId.ToString());
        //                //sb.Append("},");
        //            }
        //                );

        //            sb.Append("]}");
        //        }
        //        else
        //        {
        //            sb.Append("[");

        //            sb.Append("{");
        //            sb.Append("\"Show\":\"There are no shows for this tour\",");
        //            sb.Append("\"ID\":\"-1\"");
        //            sb.Append("},");

        //            sb.Append("]}");
        //        }
        //    }
        //    else
        //    {
        //        sb.Append("[");

        //        sb.Append("{");
        //        sb.Append("\"Show\":\"Please choose a tour\",");
        //        sb.Append("\"ID\":\"-1\"");
        //        sb.Append("},");

        //        sb.Append("]}");
        //    }

        //    Response.ContentType = "application/json";
        //    Response.ContentEncoding = Encoding.UTF8;
        //    Response.Write(sb.ToString());
        //    Response.End();
        //}
    }
}
