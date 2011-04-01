using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Text;
using PhishPond.Concrete.EventArgs;

namespace PhishMarket.Controls
{
    public partial class SelectAlbumControl : System.Web.UI.UserControl
    {
        public delegate void SelectAlbumCommandEventHandler(object sender, SelectAlbumCommandEventArgs e);
        public event SelectAlbumCommandEventHandler SongSelected;

        protected virtual void OnSelectAlbumSelected(SelectAlbumCommandEventArgs e)
        {
            if (SongSelected != null) SongSelected(this, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void btnChooseAlbum_Click(object sender, EventArgs e)
        {
            var songService = new SongService(Ioc.GetInstance<ISongRepository>());

            var songs = songService.GetSongsByAlbum(ddlAlbum.SelectedValue);

            rptSongs.DataSource = songs;
            rptSongs.DataBind();
        }

        public void rptSongs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "choose")
            {
                Guid songId = new Guid(e.CommandArgument.ToString());

                OnSelectAlbumSelected(new SelectAlbumCommandEventArgs { SongId = songId });
            }
        }

        private void Bind()
        {
            SongService songService = new SongService(Ioc.GetInstance<ISongRepository>());

            var albums = songService.GetAlbums();

            if (albums != null)
            {
                ddlAlbum.Items.AddRange(albums);
            }
        }

        public string GetSongName(double? length, DateTime? showDate, string city, string state)
        {
            if (length == null && showDate == null && city == null && state == null)
                return string.Empty;

            StringBuilder s = new StringBuilder();
            string showDateStr = showDate.Value.ToString("MM/dd/yyyy");

            if (length != null)
                s.Append(length.Value);

            if (showDate != null)
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(showDateStr);
            }

            if (!string.IsNullOrEmpty(city))
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(city);

                if (!string.IsNullOrEmpty(state))
                {
                    s.Append(", " + state);
                }
            }

            return s.ToString();
        }
    }
}