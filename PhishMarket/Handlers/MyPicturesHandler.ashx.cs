using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCore.Infrastructure;

namespace PhishMarket.Handlers
{
    public class MyPicturesHandler : BaseHandler
    {
        public MyPicturesHandler() { Ioc.BuildUp(this); }

        public override void ProcessRequest(HttpContextBase context)
        {


            //if (string.IsNullOrEmpty(ddlShows.SelectedValue))
            //    return;

            //phNoImages.Visible = false;

            //Guid showId = new Guid(ddlShows.SelectedValue);

            //MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            //MyShowArtService myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());
            //ArtService artService = new ArtService(Ioc.GetInstance<IArtRepository>());

            //var myShow = myShowService.GetMyShow(showId, userId);

            //var myShowArts = myShowArtService.GetMyShowArtByMyShow(myShow.MyShowId);

            //IList<IArt> art = new List<IArt>();

            //myShowArts.ToList().ForEach(x =>
            //{
            //    art.Add(artService.GetArt(x.ArtId));
            //});

            //if (artId != null)
            //{
            //    art = art.Where(x => x.ArtId != artId).ToList();
            //}

            //if (art == null || art.Count <= 0)
            //{
            //    phNoImages.Visible = true;
            //}

            //rptArt.DataSource = art;
            //rptArt.DataBind();



        }

        public override bool IsReusable { get { return true; } }
    }
}
