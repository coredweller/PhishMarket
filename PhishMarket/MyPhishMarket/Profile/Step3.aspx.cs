using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using PhishPond.Concrete;
using TheCore.Helpers;

namespace PhishMarket.MyPhishMarket.ProfilePages
{
    public partial class Step3 : PhishMarketBasePage
    {
        SongService songService = new SongService(Ioc.GetInstance<ISongRepository>());
        ProfileService profileService = new ProfileService(Ioc.GetInstance<IProfileRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("PhishMarket Profile Step 3");

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var albums = songService.GetAlbums();

            if (albums != null)
            {
                ddlAlbum.Items.AddRange(albums);
            }

        }

        public void rptSongs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "choose")
            {
                var profileService = new ProfileService(Ioc.GetInstance<IProfileRepository>());

                Guid songId = new Guid(e.CommandArgument.ToString());

                var songs = profileService.GetAllVersions(songId);

                ddlFavoriteChoice.Items.Clear();

                ddlFavoriteChoice.Items.AddRange((from s in songs select new ListItem(GetSongName(s.SetSongLength, s.ShowDate, s.City, s.State), string.Format("{0}^{1}", s.SetSongId, songId))).ToArray());

                phFavoriteChoice.Visible = true;
            }
        }

        public void btnChooseAlbum_Click(object sender, EventArgs e)
        {
            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var result = profileService.GetFavoriteVersions(userId, ddlAlbum.SelectedValue);

            ddlFavoriteChoice.Items.Clear();

            rptSongs.DataSource = result;

            rptSongs.DataBind();
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            FavoriteVersionService faveService = new FavoriteVersionService(Ioc.GetInstance<IFavoriteVersionRepository>());

            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            if (string.IsNullOrEmpty(ddlFavoriteChoice.SelectedValue))
                return;

            Guid setSongId = new Guid(ddlFavoriteChoice.SelectedValue.Split('^')[0]);
            Guid songId = new Guid(ddlFavoriteChoice.SelectedValue.Split('^')[1]);

            var fave = faveService.GetFavoriteVersionByUserIdAndSongId(userId, songId);

            bool success = false;

            if (fave != null)
            {
                using (IUnitOfWork uow = UnitOfWork.Begin())
                {
                    fave.SetSongId = setSongId;
                    fave.UpdatedDate = DateTime.Now;

                    uow.Commit();

                    success = true;
                }
            }
            else
            {
                FavoriteVersion faveVersion = new FavoriteVersion
                {
                    FavoriteVersionId = Guid.NewGuid(),
                    SetSongId = setSongId,
                    SongId = songId,
                    UserId = userId
                };

                faveService.SaveCommit(faveVersion, out success);
            }

            if (success)
            {
                var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", "Congratulations you have added a favorite version");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
                btnChooseAlbum_Click(null, null);
            }
            else
            {
                var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "Sorry an error has occurred saving your favorite version");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
            }
        }
    }
}
