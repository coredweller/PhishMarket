using System;
using System.Web.Security;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.MyPhishMarket
{
    public partial class MyPicturesByShow : PhishMarketBasePage
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
            string showIdStr = Request.QueryString["id"];

            if (string.IsNullOrEmpty(showIdStr))
                return;

            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            Guid showId = new Guid(showIdStr);

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var show = showService.GetShow(showId);

            Page.Title = "My Pictures for " + show.GetShowName();

            SlideShowExtender1.ContextKey = string.Format("{0};{1}", userId, showId);
        }

    }
}
