using System;
using System.Web.Security;

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

            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            Guid showId = new Guid(showIdStr);

            SlideShowExtender1.ContextKey = string.Format("{0};{1}", userId, showId);
        }

    }
}
