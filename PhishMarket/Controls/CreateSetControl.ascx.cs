using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;

namespace PhishMarket.Controls
{
    public partial class CreateSetControl : System.Web.UI.UserControl
    {
        protected short? FinalOrder { get; set; }
        SetSongService setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            hdnId.Value = Request.QueryString["id"];
            lnkReturn.NavigateUrl = Request.QueryString["return"];

            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void rptSongList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ResetPanels();

            bool success = true;
            bool needToCommit = false;

            using (IUnitOfWork uow = TheCore.Infrastructure.UnitOfWork.Begin())
            {

                Guid g = new Guid(e.CommandArgument.ToString());
                var song = (SetSong)setSongService.GetSetSong(g);
                var set = song.Set;

                if (song != null)
                {
                    if (e.CommandName.ToLower() == "remove")
                    {
                        needToCommit = true;
                        RemoveSong(g, song, set);
                    }
                    else if (e.CommandName.ToLower() == "up")
                    {
                        needToCommit = true;
                        MoveSongUp(g, song, set);
                    }
                    else if (e.CommandName.ToLower() == "down")
                    {
                        needToCommit = true;
                        MoveSongDown(g, song, set);
                    }

                    try
                    {
                        if (needToCommit)
                        {
                            uow.Commit();

                            success = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                    }
                }
            }

            DetermineSuccess(success);

            Bind();
        }

        private void DetermineSuccess(bool success)
        {
            if (success)
            {
                phError.Visible = false;
                phSuccess.Visible = true;
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
        }

        private void MoveSongDown(Guid g, SetSong song, Set set)
        {
            if (song.Order != set.SetSongs.OrderBy(x => x.Order).Last().Order)
            {
                var songAfter = (SetSong)setSongService.GetSetSong(set.SetSongs.Where(x => x.Order == song.Order + 1).First().SetSongId);

                song.Order++;
                songAfter.Order--;
            }
        }

        private void MoveSongUp(Guid g, SetSong song, Set set)
        {
            if (song.Order != 1)
            {
                var songBefore = (SetSong)setSongService.GetSetSong(set.SetSongs.Where(x => x.Order == song.Order - 1).First().SetSongId);

                song.Order--;
                songBefore.Order++;
            }
        }

        private void RemoveSong(Guid g, SetSong song, Set set)
        {
            song.Deleted = true;
            song.DeletedDate = DateTime.Now;

            setSongService.Delete(song);

            var songList = set.SetSongs.Where(x => x.SetSongId != song.SetSongId).OrderBy(x => x.Order);

            short? order = 1;

            foreach (var s in songList)
            {
                s.Order = order;
                order++;
            }
        }

        private void ResetPanels()
        {
            phError.Visible = false;
            phSuccess.Visible = false;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();

            TopicService tourService = new TopicService(Ioc.GetInstance<ITopicRepository>());
            SongService songService = new SongService(Ioc.GetInstance<ISongRepository>());
            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());
            SetSongService setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

            bool success = false;
            bool compiledSuccess = true;

            if (string.IsNullOrEmpty(hdnId.Value))
            {
                Bind();
                return;
            }

            var set = (Set)setService.GetSet(new Guid(hdnId.Value));

            if (set == null)
            {
                Bind();
                return;
            }

            if (lstSongs.Items.Count <= 0)
            {
                Bind();
                return;
            }

            using (IUnitOfWork uow = TheCore.Infrastructure.UnitOfWork.Begin())
            {

                foreach (ListItem item in lstSongs.Items)
                {
                    if (!item.Selected) { continue; }

                    var song = songService.GetSong(new Guid(item.Value));

                    if (song == null) { continue; }

                    short? order = 1;

                    if (set.SetSongs.Count > 0)
                    {
                        order = set.SetSongs.OrderBy(x => x.Order).Last().Order;
                        order++;
                    }

                    SetSong setSong = new SetSong()
                        {
                            Album = song.Album,
                            CreatedDate = DateTime.UtcNow,
                            SetSongId = Guid.NewGuid(),
                            SongId = song.SongId,
                            SongName = song.SongName,
                            Order = order,
                            Set = set,
                            SetId = set.SetId,
                            Segue = chkSegue.Checked
                        };

                    setSongService.Save(setSong, out success);

                    compiledSuccess = compiledSuccess && success;
                }

                if (compiledSuccess)
                {
                    uow.Commit();
                    phSuccess.Visible = true;
                    phError.Visible = false;
                }
                else
                {
                    phError.Visible = true;
                    phSuccess.Visible = false;
                }
            }
            
            Bind();
        }

        private void Bind()
        {
            BindSongs();

            BindSet();
        }

        private void BindSet()
        {
            if (!string.IsNullOrEmpty(hdnId.Value))
            {
                SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

                Guid g = new Guid(hdnId.Value);

                var set = (Set)setService.GetSet(g);

                if (set != null)
                {
                    if (set.SetSongs.Count > 0)
                    {
                        FinalOrder = set.SetSongs.OrderBy(x => x.Order).Last().Order;

                        rptSongList.DataSource = set.SetSongs.OrderBy(x => x.Order);
                        rptSongList.DataBind();
                    }
                }
            }
        }

        private void BindSongs()
        {
            SongService service = new SongService(Ioc.GetInstance<ISongRepository>());

            var songs = service.GetAllSongs().OrderBy(x => x.SongName).ToList();

            int finalPosition = 0;
            ListItem[] collection;

            if (songs != null)
            {
                //Need extra one for the "Please select a song" item
                collection = new ListItem[songs.Count + 1];

                //use song.Count because it has the exact correct amount. Collections length has an extra one for the first item
                for (int i = 0; i < songs.Count; i++)
                {
                    collection[i] = new ListItem(songs[i].SongName, songs[i].SongId.ToString());
                    finalPosition = i + 1;
                }

                ListItem item = new ListItem("Please select a song", "-1");

                collection[finalPosition] = item;

                item.Selected = true;

                lstSongs.Items.AddRange(collection);

            }
        }
    }
}