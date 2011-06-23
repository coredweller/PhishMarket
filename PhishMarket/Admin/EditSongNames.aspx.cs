using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.Admin
{
    public partial class EditSongNames : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
                
        public void btnFixSetSong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSongName.Text))
                return;

            if (string.IsNullOrEmpty(hdnSetSongIdToFix.Value))
                return;

            var changeSongNameToo = false;

            if (chkSongToo.Checked)
                changeSongNameToo = true;

            var newSongName = txtSongName.Text.Trim();
            var setSongId = new Guid(hdnSetSongIdToFix.Value);
                        
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

                var setSong = setSongService.GetSetSong(setSongId);

                setSong.SongName = newSongName;
                var setsongs = setSongService.GetAllSetSongs().Where(x => x.SongName.Contains(txtSearchSongName.Text));

                if (changeSongNameToo)
                {
                    var songService = new SongService(Ioc.GetInstance<ISongRepository>());
                    var song = songService.GetSong(setSong.SongId.Value);
                    song.SongName = newSongName;
                }

                uow.Commit();

                rptSongs.DataSource = setsongs;
                rptSongs.DataBind();
            }
        }

        public void rptSongs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

            var setSong = setSongService.GetSetSong(new Guid(e.CommandArgument.ToString()));

            if (e.CommandName.ToLower() == "fix")
            {
                txtSongName.Text = setSong.SongName;
                hdnSetSongIdToFix.Value = setSong.SetSongId.ToString();
            }
            else if (e.CommandName.ToLower() == "delete")
            {
                ///TEST THIS SECTION
                var songService = new SongService(Ioc.GetInstance<ISongRepository>());
                var song = songService.GetSong(setSong.SongId.Value);

                using (IUnitOfWork uow = UnitOfWork.Begin())
                {
                    setSongService.Delete(setSong);

                    if (song != null)
                        songService.Delete(song);

                    uow.Commit();
                }

                var setsongs = setSongService.GetAllSetSongs().Where(x => x.SongName.Contains(txtSearchSongName.Text));

                rptSongs.DataSource = setsongs;
                rptSongs.DataBind();
            }
        }

        public void rptSongs_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var del = (LinkButton)e.Item.FindControl("lnkDelete");

                del.Attributes.Add("onclick", "return confirmDelete();");
            }
        }

        public void btnSearchSongName_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchSongName.Text))
                return;

            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

            var setsongs = setSongService.GetAllSetSongs().Where(x => x.SongName.Contains(txtSearchSongName.Text));

            rptSongs.DataSource = setsongs;
            rptSongs.DataBind();
        }
    }
}
