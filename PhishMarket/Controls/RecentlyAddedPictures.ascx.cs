using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.Controls
{
    public partial class RecentlyAddedPictures : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var photos = photoService.GetRecentlyAddedPhotos().ToList();
            var showIds = (from p in photos select p.ShowId.Value).ToList();
            var shows = showService.GetShows(showIds);

            var showPhotos = new List<ShowPhoto>();

            showPhotos = (from p in photos
                          from s in shows
                          where p.ShowId == s.ShowId
                          select new ShowPhoto { NickName = p.NickName, ShowDate = s.ShowDate.Value.ToShortDateString(), ShowId = s.ShowId.ToString() }).ToList();

            rptRecentlyAddedPictures.DataSource = showPhotos;
            rptRecentlyAddedPictures.DataBind();
        }
    }

    public class ShowPhoto
    {
        public string ShowId { get; set; }
        public string NickName { get; set; }
        public string ShowDate { get; set; }
    }
}