using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using PhishPond.Concrete;
using TheCore.Infrastructure;
using TheCore.Repository;
using TheCore.Helpers;

namespace PhishMarket.Admin
{
    public partial class CreateAlbum : PhishMarketBasePage
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
            var yearService = new YearService();
            ddlYearReleased.Items.AddRange(yearService.GetAllPhishYears());

            var item = new ListItem("Please select a year", "-1");
            ddlYearReleased.Items.Add(item);
            item.Selected = true;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (EmptyNullUndefined(txtAlbumName.Text) || ddlYearReleased.SelectedValue == "-1")
                return;

            var album = new Album
            {
                AlbumId = Guid.NewGuid(),
                AlbumName = txtAlbumName.Text,
                CreatedDate = DateTime.UtcNow,
                YearReleased = int.Parse(ddlYearReleased.SelectedValue)
            };

            var albumService = new AlbumService(Ioc.GetInstance<IAlbumRepository>());

            bool success;
            albumService.SaveCommit(album, out success);

            if (success)
            {
                var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", "You have successfully created an album.");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
            }
            else
            {
                var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "There was an error, try again later.");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
            }
        }
    }
}
