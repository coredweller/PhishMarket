using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCore.Repository;
using PhishPond.Repository.LinqToSql;
using TheCore.Configuration;
using TheCore.Services;
using TheCore.Helpers;
using System.Text;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace PhishMarket.Handlers
{

    public class ShowReviewsHandler : BaseHandler
    {
        private const string connKey = "PhishPond";

        public override void ProcessRequest(HttpContextBase context)
        {
            HttpRequestBase request = context.Request;
            var showIdStr = request.QueryString["s"];
            var showDateStr = request.QueryString["d"];
            HttpResponseBase response = context.Response;

            var final = string.Empty;

            if (EmptyNullUndefined(showIdStr) && EmptyNullUndefined(showDateStr))
            {
                final = GetNoImagesFound();

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.Write(final);
                response.End();
            }

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            if (string.IsNullOrEmpty(showIdStr))
            {
                DateTime date;
                var success = DateTime.TryParse(showDateStr, out date);

                if (!success)
                    return;

                var s = showService.GetShow(date);

                if (s == null)
                    return;

                showIdStr = s.ShowId.ToString();
            }

            var showId = new Guid(showIdStr);

            IPhotoRepository photoRepo = new PhotoRepository(new PhishDatabase(new ConnectionString(new AppConfigManager(), connKey)));

            PhotoService photoService = new PhotoService(photoRepo);

            var photos = photoService.GetPhotosByShow(showId).Where(x => x.Thumbnail == false).ToList();

            if (photos == null || photos.Count <= 0)
            {
                final = GetNoImagesFound();
            }

            if (string.IsNullOrEmpty(final))
            {
                var json = new ImageJSONifier("records");

                foreach (var photo in photos)
                {
                    var path = (PhotoType)photo.Type != PhotoType.TicketStub ? ShowImagesFolder : TicketStubImagesFolder;

                    json.Add(new ImageItem
                    {
                        Image = path + photo.FileName,
                        Description = photo.Notes,
                        Title = photo.NickName,
                        //Thumb =  ///LEFT OFF HERE
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
            json.Add(new ImageItem { Image = loc, Thumb = loc, Title = "No Images Found", Description = "Upload posters or pictures of your own so everyone can see what this show was like" });
            return json.GetFinalizedJSON();
        }

        public bool IsReusable { get { return true; } }
    }
}
