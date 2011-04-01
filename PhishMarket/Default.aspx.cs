using System;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket
{

    public partial class _Default : PhishMarketBasePage
    {
        PostService service = new PostService(Ioc.GetInstance<IPostRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //if (Page != null)
            //{
            //    string val = "PhishMarket3";

            //    if (!string.IsNullOrEmpty(ddlStyles.SelectedValue))
            //    {
            //        val = ddlStyles.SelectedValue;
            //    }

            //    Page.MasterPageFile = string.Format("~/Master/{0}.Master", val);
            //}
        }

        private void Bind()
        {
            //var posts = service.GetAllPosts();

            //rptPosts.DataSource = posts;
            //rptPosts.DataBind();
        }
    }
}
