using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCore.Helpers;
using System.Text;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;
using TheCore.Interfaces;

namespace PhishMarket.Handlers
{
    
    public class MyPostersHandler : BaseHandler
    {

        public override void ProcessRequest(HttpContextBase context)
        {
            HttpRequestBase request = context.Request;
            var showIdStr = request.QueryString["s"];
            var userIdStr = request.QueryString["u"];
            HttpResponseBase response = context.Response;

            var final = string.Empty;

            if (EmptyNullUndefined(showIdStr) || EmptyNullUndefined(userIdStr))
            {
                final = GetNoImagesFound();

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.Write(final);
                response.End();
            }

            var showId = new Guid(showIdStr);
            var userId = new Guid(userIdStr);

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());
            var posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);

            var myShowPosters = myShowPosterService.GetMyShowPosterByMyShow(myShow.MyShowId);

            IList<KeyValuePair<Poster, IMyShowPoster>> posters = new List<KeyValuePair<Poster, IMyShowPoster>>();

            myShowPosters.ToList().ForEach(x =>
            {
                posters.Add(new KeyValuePair<Poster, IMyShowPoster>((Poster)posterService.GetPoster(x.PosterId), x));
            });

            if (posters == null || posters.Count <= 0)
            {
                final = GetNoImagesFound();
            }

            //If there are images and no errors so far then process
            if (string.IsNullOrEmpty(final))
            {
                var json = new ImageJSONifier("records");

                foreach (var a in posters)
                {
                    json.Add(new ImageItem
                    {
                        Image = "/images/Shows/" + a.Key.Photo.FileName,
                        Description = a.Key.Notes,
                        Title = a.Key.Photo.NickName,
                        Link = string.Format("DeletePicture.aspx?posid={0}&showId={1}", a.Value.MyShowPosterId.ToString(), showId.ToString())
                    });
                }

                final = json.GetFinalizedJSON();
            }

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Write(final);
        }

        private bool EmptyNullUndefined(string brih)
        {
            if (string.IsNullOrEmpty(brih) || brih == "undefined")
                return true;

            return false;
        }

        private string GetNoImagesFound()
        {
            var json = new ImageJSONifier("records");
            var loc = NoImagesLocation;
            json.Add(new ImageItem { Image = loc, Thumb = loc, Title = "No Images Found", Description = "Upload posters from others or upload your own above" });
            return json.GetFinalizedJSON();
        }

        public override bool IsReusable { get { return true; } }
    }
}
