using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using System.Web.Security;
using System.Text;
using TheCore.Repository;

namespace PhishMarket.Admin
{
    public partial class CreateSet : PhishMarketBasePage
    {
        private string returnUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            returnUrl = Request.Url.ToString();

            Guid UserID = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void ResetPanels()
        {
            phAddSongs.Visible = false;
            phError.Visible = false;
            phSuccess.Visible = false;
        }

        private void Bind()
        {
            ShowService service = new ShowService(Ioc.GetInstance<IShowRepository>());
            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            var shows = service.GetAllShows();
            var sets = setService.GetAllSets();

            foreach (var show in shows)
            {
                ddlShows.Items.Add(new ListItem(show.GetShowName(), show.ShowId.ToString()));
            }

            ListItem item = new ListItem("Please select a show", "-1");

            ddlShows.Items.Insert(0, item);

            item.Selected = true;

            foreach (Set set in sets)
            {
                StringBuilder setName = new StringBuilder();
                
                setName.Append(set.Notes);

                if (set.Official)
                    setName.Append("***");

                if (set.Encore)
                    setName.Append(" (E)");

                if (set.ShowId != null)
                    setName.Append(" (S)");

                if (set.SetNumber > 0)
                    setName.Append(string.Format(" - {0}", set.SetNumber));

                ddlSets.Items.Add(new ListItem(setName.ToString(), set.SetId.ToString()));
            }
            ListItem item2 = new ListItem("Please select a set", "-1");

            ddlSets.Items.Insert(0, item2);

            item2.Selected = true;
        }

        public void btnModifySet_Click(object sender, EventArgs e)
        {
            ResetPanels();

            string setValue = ddlSets.SelectedValue;

            if (setValue != "-1")
            {
                Guid g = new Guid(setValue);
                Response.Redirect(LinkBuilder.AddSongsToSetControlLink(g, returnUrl));
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();

            bool success = false;
            Guid? showId;
            Guid setId = Guid.NewGuid();

            SetService service = new SetService(Ioc.GetInstance<ISetRepository>());

            if (Validated())
            {
                showId = ddlShows.SelectedValue != "-1" ? new Guid(ddlShows.SelectedValue) : EmptyGuid;

                short? setNum = ddlSetNumber.SelectedValue != "0" ? short.Parse(ddlSetNumber.SelectedValue) : (short)0;

                Set set = new Set()
                {
                    SetId = setId,
                    Encore = chkEncore.Checked,
                    Notes = txtNotes.Text.Trim(),
                    Official = chkOfficial.Checked,
                    SetNumber = setNum == 0 ? null : setNum,
                    ShowId = showId == EmptyGuid ? null : showId
                };
                
                service.SaveCommit(set, out success);
            }

            if (success)
            {
                phSuccess.Visible = true;
                phError.Visible = false;
                phAddSongs.Visible = true;
                lnkAddSongsToSet.NavigateUrl = LinkBuilder.AddSongsToSetControlLink(setId, returnUrl);
                lnkAddSetToGuess.NavigateUrl = LinkBuilder.AddSetToGuessLink(setId);
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
            
        }

        private bool Validated()
        {
            bool valid = true;

            return valid;
        }
    }
}
