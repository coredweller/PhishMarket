using System;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishMarket.Controls
{
    public partial class RecentTopics : BaseControl
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
            YafService service = new YafService(Ioc.GetInstance<IYafRepository>());

            var topics = service.GetRecentTopics();

            rptRecentTopics.DataSource = topics;

            rptRecentTopics.DataBind();
        }
    }
}