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

            var art = artService.GetArtByUser(userId).Cast<Art>().OrderBy(x=> x.Show.ShowDate).ToList();
            var posters = posterService.GetByUser(userId).Cast<Poster>().OrderBy(x => x.Show.ShowDate).ToList();

            //If there are no art or posters then return no images found
            if ((art == null && posters == null) || (art.Count <= 0 && posters.Count <= 0))
            {
                final = GetNoImagesFound();

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.Write(final);
                response.End();
            }

            ///LEFT OFF HERE
            ///Must parse all the art and posters into imageitems then sort by showdate and then put into json
            
            var json = new ImageJSONifier("records");

            //foreach (var photo in photos)
            //{
            //    var path = (PhotoType)photo.Type != PhotoType.TicketStub ? ShowImagesFolder : TicketStubImagesFolder;

            //    json.Add(new ImageItem
            //    {
            //        Image = path + photo.FileName,
            //        Description = photo.Notes,
            //        Title = photo.NickName,
            //        //Thumb =  //This is a consideration.  If we want to go through the trouble of using the thumb or not
            //    });
            //}

            //final = json.GetFinalizedJSON();


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
