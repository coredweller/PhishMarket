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
        public MyPicturesHandler() { Ioc.BuildUp(this); }

        public override void ProcessRequest(HttpContextBase context)
        {
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

            var myShowArts = myShowArtService.GetMyShowArtByMyShow(myShow.MyShowId);

            IList<Art> art = new List<Art>();

            myShowArts.ToList().ForEach(x =>
            {
                art.Add((Art)artService.GetArt(x.ArtId));
            });

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
                    json.Add(new ImageItem 
                    { 
                        Image = "/../../images/Shows/" + a.Photo.FileName,
                        Description = a.Notes,
                        Title = a.Photo.NickName
                    });
                }
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
            json.Add(new ImageItem { Image = "/../.." + NoImagesLocation, Thumb = NoImagesLocation, Title = "No Images Found", Description = "No Images Found" });
            return json.GetFinalizedJSON();
        }

        public override bool IsReusable { get { return true; } }
    }
}