﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCore.Infrastructure;
using TheCore.Services;
using TheCore.Interfaces;
using TheCore.Repository;
using System.Text;
using TheCore.Helpers;
using PhishPond.Concrete;

namespace PhishMarket.Handlers
{
    public class MyPicturesHandler : BaseHandler
    {
        public override void ProcessRequest(HttpContextBase context)
        {
            //System.Threading.Thread.Sleep(4000);
            HttpRequestBase request = context.Request;
            var showIdStr = request.QueryString["s"];
            var userIdStr = request.QueryString["u"];
            HttpResponseBase response = context.Response;

            var final = string.Empty;

            if ( EmptyNullUndefined(showIdStr) || EmptyNullUndefined(userIdStr) )
            {
                final = GetNoImagesFound();

                response.ContentType = "application/json";
                response.ContentEncoding = Encoding.UTF8;
                response.Write(final);
                response.End();
            }

            var showId = new Guid(showIdStr);
            var userId = new Guid(userIdStr);

            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            MyShowArtService myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());
            ArtService artService = new ArtService(Ioc.GetInstance<IArtRepository>());

            var myShow = myShowService.GetMyShow(showId, userId);
            IList<KeyValuePair<Art, IMyShowArt>> art = new List<KeyValuePair<Art, IMyShowArt>>();

            if (myShow != null)
            {
                var myShowArts = myShowArtService.GetMyShowArtByMyShow(myShow.MyShowId);

                myShowArts.ToList().ForEach(x =>
                {
                    art.Add(new KeyValuePair<Art, IMyShowArt>((Art)artService.GetArt(x.ArtId), x));
                });
            }

            if (art == null || art.Count <= 0)
            {
                final = GetNoImagesFound();
            }

            //If there are images and no errors so far then process
            if (string.IsNullOrEmpty(final))
            {
                var json = new ImageJSONifier("records");

                foreach (var a in art)
                {
                    if (a.Key == null || a.Key.Photo == null) continue;

                    json.Add(new ImageItem 
                    { 
                        Image = "/images/Shows/" + a.Key.Photo.FileName,
                        Description = a.Key.Notes,
                        Title = a.Key.Photo.NickName,
                        Link = string.Format("DeletePicture.aspx?picid={0}&showId={1}", a.Value.MyShowArtId.ToString(), showId.ToString())
                    });
                }

                final = json.GetFinalizedJSON();
            }

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Write(final);

        }

        public override bool IsReusable { get { return true; } }
    }
}
