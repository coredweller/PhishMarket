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

        private void Bind()
        {
            //var tourService = new TourService(Ioc.GetInstance<ITourRepository>());

            //var tours = tourService.GetAllToursDescending();

            //foreach (var tour in tours)
            //{
            //    string tourName = tour.TourName;

            //    ddlTours.Items.Add(new ListItem(tourName, tour.TourId.ToString()));
            //}

            //var item = new ListItem("Choose Tour", "-1");
            //item.Selected = true;
            //ddlTours.Items.Add(item);
        }

        //public void btnTourSubmit_Click(object sender, EventArgs e)
        //{
        //    if (ddlTours.SelectedValue != "-1")
        //    {
        //        var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

        //        var shows = showService.GetOfficialShows(new Guid(ddlTours.SelectedValue));

        //        foreach (var show in shows)
        //        {
        //            string showName = show.GetShowName();

        //            ddlShows.Items.Add(new ListItem(showName, show.ShowId.ToString()));
        //        }

        //        var item = new ListItem("Choose Show", "-1");
        //        item.Selected = true;
        //        ddlShows.Items.Add(item);
        //    }
        //}
      
        //public void btnShowSubmit_Click(object sender, EventArgs e)
        //{
        //    if (ddlShows.SelectedValue != "-1")
        //    {
        //        var setService = new SetService(Ioc.GetInstance<ISetRepository>());

        //        var sets = setService.GetSetsForShow(new Guid(ddlShows.SelectedValue)); ;

        //        foreach (var set in sets)
        //        {
        //            string setName = "Set:" + set.SetNumber;

        //            if (set.Encore)
        //                setName += "   ENCORE";

        //            ddlSets.Items.Add(new ListItem(setName, set.SetId.ToString()));
        //        }

        //        var item = new ListItem("Choose Set", "-1");
        //        item.Selected = true;
        //        ddlSets.Items.Add(item);
        //    }
        //}

        //public void btnSetSubmit_Click(object sender, EventArgs e)
        //{
        //    if (ddlSets.SelectedValue != "-1")
        //    {
        //        var songService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

        //        var songs = songService.GetSetSongsBySet(new Guid(ddlSets.SelectedValue));

        //        rptSongs.DataSource = songs;
        //        rptSongs.DataBind();
        //    }
        //}
    }
}
