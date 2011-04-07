using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket
{
    public partial class News : PhishMarketBasePage
    {
        PostService service = new PostService(Ioc.GetInstance<IPostRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("What's New with Phish Market");

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var posts = service.GetAllPosts();

            rptPosts.DataSource = posts;
            rptPosts.DataBind();
        }
    }
}
