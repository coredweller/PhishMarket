using System;
using System.Linq;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using PhishPond.Concrete;


namespace PhishMarket.TourPages
{
    public partial class GetScore : PhishMarketBasePage
    {
        public string TopicName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["return"]))
                lnkReturn.NavigateUrl = Request.QueryString["return"];

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["gid"]))
            {
                phGuessWholeShowScore.Visible = true;
                
                Guid guessId = new Guid(Request.QueryString["gid"]);
                Guid userID = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

                GuessWholeShowService guessService = new GuessWholeShowService(Ioc.GetInstance<IGuessWholeShowRepository>());
                TopicService topicService = new TopicService(Ioc.GetInstance<ITopicRepository>());

                var guess = guessService.GetGuessWholeShow(guessId);

                TopicName = topicService.GetTopic(guess.TopicId).TopicName;

                if (guess.UserId == userID)
                {
                    var officialGuess = guessService.GetOfficialGuessByTopic(guess.TopicId);

                    if (officialGuess != null)
                    {
                        var score = guess.GetScore(officialGuess);

                        if (score != null)
                        {
                            lblGuessWholeShowScore.Text = score.GetScore().ToString();
                        }
                    }
                }
                else
                {
                    //Go buck fucking wild on them
                }

                //WHEN OTHER GUESSES GET INTEGRATED INTO THIS PAGE THIS FUNCTIONALITY WILL NEED 
                // TO BE USED FOR EACH ONE SO WILL HAVE TO BE MOVED OUT
                SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

                var set = (Set)setService.GetSet(guess.SetId);

                if (set != null)
                {
                    rptSongList.DataSource = set.SetSongs.Where(x => x.Deleted == false).OrderBy(x => x.Order);
                    rptSongList.DataBind();
                }
            }

            
        }
    }
}
