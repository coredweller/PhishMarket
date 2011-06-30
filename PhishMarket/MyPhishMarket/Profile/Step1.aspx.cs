using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;
using TheCore.Helpers;

namespace PhishMarket.MyPhishMarket.ProfilePages
{
    public partial class Step1 : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("PhishMarket Profile Step 1");

            //btnSubmit.Attributes.Add("onmouseover", "this.src = '/images/buttons/greySaveButtonRollover.jpg'");
            //btnSubmit.Attributes.Add("onmouseout", "this.src = '/images/buttons/greySaveButton.jpg'");

            if (!IsPostBack)
            {
                //BindLists();
                BindProfile();
            }
        }

        //public void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //{
        //    Response.Redirect(LinkBuilder.ProfileStep2Link());
        //}

        //public void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //{
        //    Response.Redirect(LinkBuilder.ChangeProfileLink());
        //}

        //private void BindLists()
        //{
        //    SetSongService setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
        //    SongService songService = new SongService(Ioc.GetInstance<ISongRepository>());

        //    var albums = songService.GetAlbums();

        //    if (albums != null)
        //    {
        //        ddlFavoriteAlbums.Items.AddRange(albums);
        //    }

        //    var item = new ListItem("Please select an album", "-1");

        //    ddlFavoriteAlbums.Items.Insert(0, item);

        //    var studioSongs = songService.GetAllSongs().Where(y => y.Album.ToLower() != "live only").OrderBy(x => x.SongName).ToList();

        //    if (studioSongs != null)
        //    {
        //        studioSongs.ForEach(x => ddlFavoriteStudioSong.Items.Add(new ListItem(x.SongName, x.SongId.ToString())));
        //    }

        //    item = new ListItem("Please select a song", "-1");

        //    ddlFavoriteStudioSong.Items.Insert(0, item);

        //    item.Selected = true;
        //}

        private void BindProfile()
        {
            var profile = GetProfile();

            if (profile != null)
            {
                txtName.Text = profile.Name;
                txtEmail.Text = profile.Email;
                //ddlFavoriteAlbums.SelectedValue = !string.IsNullOrEmpty(profile.FavoriteAlbum) ? profile.FavoriteAlbum : string.Empty;
                //ddlFavoriteStudioSong.SelectedValue = profile.FavoriteStudioSong != null ? profile.FavoriteStudioSong.ToString() : string.Empty;
            }
        }

        public void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            using (IUnitOfWork uow = TheCore.Infrastructure.UnitOfWork.Begin())
            {
                var profile = (Profile)GetProfile();

                profile.Name = txtName.Text;
                profile.Email = txtEmail.Text;

                //if (ddlFavoriteAlbums.SelectedValue != "-1")
                //    profile.FavoriteAlbum = ddlFavoriteAlbums.SelectedValue;

                //if (ddlFavoriteStudioSong.SelectedValue != "-1")
                //    profile.FavoriteStudioSong = new Guid(ddlFavoriteStudioSong.SelectedValue);

                uow.Commit();
            }

            var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", "You have successfully saved your profile. Proceed to Step 2 by clicking NEXT below!");
            Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
        }
    }
}
