using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using TheCore.Helpers;
using PhishPond.Concrete;

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

            var userId = new Guid(userIdStr);

            var artService = new ArtService(Ioc.GetInstance<IArtRepository>());
            var posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());

            var art = artService.GetArtByUser(userId).Cast<Art>().Where(y => y.Show != null).OrderBy(x=> x.Show.ShowDate.Value).ToList();
            var posters = posterService.GetByUser(userId).Cast<Poster>().ToList();  /// LEFT OFF HERE TRYING TO FIGURE OUT WHY SHOW IS NULL FOR ALL THE POSTERS
            var posters2 = posters.Where(z => z.Show != null).OrderBy(v => v.Show.ShowDate.Value).ToList();

            //If there are no art or posters then return no images found
            if ((art == null && posters == null) || (art.Count <= 0 && posters.Count <= 0))
            {
                final = GetNoImagesFound();

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.Write(final);
                response.End();
            }

            var allImages = (from a in art
                             select new ImageItem
                             {
                                 Image = ShowImagesFolder + a.Photo.FileName,
                                 Description = a.Photo.Notes,
                                 Title = a.Photo.NickName,
                                 ShowDate = a.Show.ShowDate.Value
                             });

            allImages.Concat(from p in posters
                                select new ImageItem
                                {
                                    Image = ShowImagesFolder + p.Photo.FileName,
                                    Description = p.Photo.Notes,
                                    Title = p.Photo.NickName,
                                    ShowDate = p.Show.ShowDate.Value
                                });

            
            var json = new ImageJSONifier("records", allImages.OrderBy(y => y.ShowDate));

            final = json.GetFinalizedJSON();

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Write(final);
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
