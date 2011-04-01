using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;
using System.Web.Security;

namespace PhishMarket.Admin
{
    public partial class CreateGuessWholeShow : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void ResetPanels()
        {
            phNoSet.Visible = false;
            phSuccess.Visible = false;
            phError.Visible = false;
            phNoTopic.Visible = false;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ResetPanels();
            bool success = false;

            if (Validated())
            {
                success = CreateGuess();
            }

            if (success)
            {
                phSuccess.Visible = true;
                phError.Visible = false;
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
        }

        private bool CreateGuess()
        {
            bool success = false;

            Guid setId;

            if (ddlSets.SelectedValue == "-1")
            {
                if (!string.IsNullOrEmpty(hdnSetId.Value))
                {
                    setId = new Guid(hdnSetId.Value);
                }
                else
                {
                    phNoSet.Visible = true;
                    return false;
                }
            }
            else
            {
                setId = new Guid(ddlSets.SelectedValue);
            }

            Guid guessId = Guid.NewGuid();
            Guid topicId = new Guid(ddlTopics.SelectedValue);
            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            GuessWholeShowService guessService = new GuessWholeShowService(Ioc.GetInstance<IGuessWholeShowRepository>());

            GuessWholeShow guess = new GuessWholeShow()
            {
                GuessWholeShowId = guessId,
                TopicId = topicId,
                SetId = setId,
                UserId = userId,
                Official = chkOfficial.Checked
            };

            guessService.SaveCommit(guess, out success);

            return success;
        }

        private bool Validated()
        {
            bool valid = true;

            if (ddlTopics.SelectedValue == "-1")
            {
                valid = false;
                phNoTopic.Visible = true;
            }

            return valid;
        }

        private void Bind()
        {
            SetupTopicList();

            SetupSets();
        }

        private void SetupTopicList()
        {
            TopicService topicService = new TopicService(Ioc.GetInstance<ITopicRepository>());

            var topics = topicService.GetAllTopics().OrderByDescending(x => x.CreatedDate);

            foreach (var topic in topics)
            {
                ddlTopics.Items.Add(new ListItem(topic.TopicName, topic.TopicId.ToString()));
            }

            ListItem item = new ListItem("Please select a Topic", "-1");

            ddlTopics.Items.Insert(0, item);

            item.Selected = true;
        }

        private void SetupSets()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Guid setId = new Guid(Request.QueryString["id"]);

                hdnSetId.Value = setId.ToString();
            }

            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            var sets = setService.GetAllSets();

            foreach (var set in sets)
            {
                string notes = "<NO NAME>";

                if (!string.IsNullOrEmpty(set.Notes))
                {
                    if (set.Notes.Length <= 15 && set.Notes.Length > 0)
                        notes = set.Notes;
                    else if (set.Notes.Length > 15)
                        notes = set.Notes.Substring(0, 14);
                }

                string setName = string.Format("{0} - {1}", notes, set.SetNumber);

                ddlSets.Items.Add(new ListItem(setName, set.SetId.ToString()));
            }

            ListItem item = new ListItem("Please select a Set", "-1");

            ddlSets.Items.Insert(0, item);

            item.Selected = true;
        }
    }
}
