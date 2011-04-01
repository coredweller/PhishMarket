using System;
using System.Web.Security;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Linq;
using PhishPond.Concrete;

namespace PhishMarket.MyPhishMarket
{
    public partial class WantedListPage : PhishMarketBasePage
    {
        Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            if (!IsPostBack)
            {
                Bind();
            }

        }

        public void selectAlbum_SongSelected(object sender, PhishPond.Concrete.EventArgs.SelectAlbumCommandEventArgs e)
        {
            ResetPanels();

            var wantedListService = new WantedListService(Ioc.GetInstance<IWantedListRepository>());

            if (wantedListService.SongAlreadyExistsForUser(userId, e.SongId))
            {
                phDuplicateError.Visible = true;
                return;
            }

            var rank = rptWantedList.Items.Count + 1;

            var wantedList = new WantedList 
            { 
                CreatedDate = DateTime.Now,
                Rank = rank,
                SongId = e.SongId,
                UserId = userId,
                WantedId = Guid.NewGuid()
            };

            bool success = false;

            wantedListService.SaveCommit(wantedList, out success);

            if (success)
            {
                phAddSongSuccess.Visible = true;
            }
            else
            {
                phAddSongError.Visible = true;
            }

            BindWantedList();
        }

        private void ResetPanels()
        {
            phAddSongError.Visible = false;
            phAddSongSuccess.Visible = false;
            phDuplicateError.Visible = false;
        }

        public void rptWantedList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BindWantedList()
        {
            var wantedListService = new WantedListService(Ioc.GetInstance<IWantedListRepository>());

            var wantedList = wantedListService.GetByUserId(userId);

            var songService = new SongService(Ioc.GetInstance<ISongRepository>());

            var songsWantedList = (from w in wantedList
                                   from s in songService.GetAllSongs()
                                   where s.SongId.Equals(w.SongId)
                                   select new { Song = s, Wanted = w });

            rptWantedList.DataSource = songsWantedList;
            rptWantedList.DataBind();
        }

        private void BindArchiveList()
        {
            var wantedListService = new WantedListService(Ioc.GetInstance<IWantedListRepository>());

            var wantedList = wantedListService.GetByUserId(userId);

            var archivedList = wantedListService.GetArchivedByUserId(userId);

            rptArchive.DataSource = archivedList;
            rptArchive.DataBind();
        }

        private void Bind()
        {
            BindWantedList();

            

        }
    }
}
