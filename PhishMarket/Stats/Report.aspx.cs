using System;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using System.Linq;
using System.Collections.Generic;
using PhishPond.Concrete;
using TheCore.Interfaces;
using System.Web.UI.WebControls;

namespace PhishMarket.Stats
{
    public partial class Report : PhishMarketBasePage
    {
        SongService songService = new SongService(Ioc.GetInstance<ISongRepository>());
        public string Query { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public string DetermineShow(string showName)
        {
            return string.IsNullOrEmpty(showName) ? "None" : showName;
        }

        private void Bind()
        {
            var albums = songService.GetAlbums();

            if (albums != null)
            {
                ddlAlbum.Items.AddRange(albums);
            }

            var letterList = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            rptLetterList.DataSource = letterList;
            rptLetterList.DataBind();
        }

        public void Report_Click(object sender, EventArgs e)
        {
            IQueryable<ISong> songs;

            var link = (LinkButton)sender;

            if (link.CommandArgument.Length == 1)
            {
                songs = songService.GetSongsByFirstLetter(link.CommandArgument);
            }
            else
            {
                songs = songService.GetSongsByAlbum(link.CommandArgument);
            }

            var results = new FavoriteLiveSongList().GenerateFavoriteLiveSongList(songs);

            if (results == null || results.Count() <= 0)
                return;

            var final = FavoriteLiveSongList.GetRepeaterUsableList(results);

            if (link.CommandName == "Favorite")
            {
                SetFavorites(final);
            }
            else if (link.CommandName == "Highest")
            {
                SetHighest(final);
            }
        }

        private void SetHighest(List<LiveSongInfo> final)
        {
            rptHighestRanked.DataSource = final;
            rptHighestRanked.DataBind();

            rptFavorites.DataSource = null;
            rptFavorites.DataBind();

            phFavorite.Visible = false;
            phHighest.Visible = true;
        }

        private void SetFavorites(List<LiveSongInfo> final)
        {
            rptFavorites.DataSource = final;
            rptFavorites.DataBind();

            rptHighestRanked.DataSource = null;
            rptHighestRanked.DataBind();

            phFavorite.Visible = true;
            phHighest.Visible = false;
        }

        public void rptLetterList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            SetLinkArgument((string)e.CommandArgument);            
            phReport.Visible = true;

            ResetRepeaters();
        }

        private void ResetRepeaters()
        {
            rptFavorites.DataSource = null;
            rptFavorites.DataBind();

            rptHighestRanked.DataSource = null;
            rptHighestRanked.DataBind();
        }

        public void btnChooseAlbum_Click(object sender, EventArgs e)
        {
            SetLinkArgument(ddlAlbum.SelectedValue);
            phReport.Visible = true;

            ResetRepeaters();
        }

        private void SetLinkArgument(string argument)
        {
            lnkFavorite.CommandArgument = argument;
            lnkHighestRated.CommandArgument = argument;
        }
    }
}
