using System;
using System.Linq;
using TheCore.Services;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using TheCore.Repository;
using System.Web.Security;

namespace PhishMarket.TourPages
{
    public partial class PredictWholeShow : PhishMarketBasePage
    {
        private string returnUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
                Response.Redirect(LinkBuilder.PredictTourLink());

            string showId = Request.QueryString["id"];

            returnUrl = string.Format("/Tour/PredictWholeShow.aspx?id={0}", showId);
            hdnId.Value = showId;
            
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            Guid showId = new Guid(Request.QueryString["id"]);
            Guid userID = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            TopicService topicService = new TopicService(Ioc.GetInstance<ITopicRepository>());
            GuessWholeShowService guessService = new GuessWholeShowService(Ioc.GetInstance<IGuessWholeShowRepository>());

            var topic = topicService.GetTopicByShow(showId);

            if (topic != null)
            {
                var guess = guessService.GetGuessWholeShowByTopicIdAndUserId(topic.TopicId, userID);

                if (guess != null)
                {
                    BindSet((GuessWholeShow)guess.First());
                }
                else
                {
                    bool s = false;

                    GuessWholeShow g = CreateNewGuess(showId, topic.TopicId, userID, out s);

                    if (!s)
                    {
                        Response.Redirect(LinkBuilder.PredictTourLink());
                    }
                    else
                    {
                        BindSet(g);
                    }
                }
            }
            else
            {
                Response.Redirect(LinkBuilder.PredictTourLink());
            }
        }

        private void BindSet(GuessWholeShow guess)
        {
            lnkGetScore.NavigateUrl = LinkBuilder.GetScoreLink(guess.GuessWholeShowId, Request.Url.ToString());

            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            var set = (Set)setService.GetSet(guess.SetId);

            if (set != null)
            {
                rptSongList.DataSource = set.SetSongs.Where(x => x.Deleted == false).OrderBy(x => x.Order);
                rptSongList.DataBind();
            }

            lnkAddSongsToSet.NavigateUrl = LinkBuilder.AddSongsToSetControlLink(set.SetId, returnUrl);
            phAddSongs.Visible = true;
            
        }

        private GuessWholeShow CreateNewGuess(Guid showId, Guid topicId, Guid userId, out bool s)
        {
            GuessWholeShowService guessService = new GuessWholeShowService(Ioc.GetInstance<IGuessWholeShowRepository>());

            s = false;
            bool compiledSuccess = true;

            Guid setId = CreateNewSet(showId, out s);
            Guid guessWholeShowId = Guid.NewGuid();

            compiledSuccess = compiledSuccess && s;

            GuessWholeShow newGuess = new GuessWholeShow()
            {
                GuessWholeShowId = guessWholeShowId,
                TopicId = topicId,
                SetId = setId,
                UserId = userId
            };

            guessService.SaveCommit(newGuess, out s);

            compiledSuccess = compiledSuccess && s;

            return newGuess;
        }

        private Guid CreateNewSet(Guid showId, out bool success)
        {
            success = false;
            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            Guid setId = Guid.NewGuid();

            Set newSet = new Set()
            {
                SetId = setId,
                Encore = false,
                Official = false,
                SetNumber = 0,
                ShowId = showId
            };

            setService.Save(newSet, out success);

            return setId;
        }
    }
}
