using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Security;

namespace PhishMarket
{
    public partial class PhishNetImporterTestPage : PhishMarketBasePage
    {
        string core = "http://api.phish.net/api.js?api=2.0&method=pnet.shows.setlists.get&format=json&apikey=6D31B9439E9F9B550B42&callback=ShowCallBack&showdate=1983-12-02";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }

        }

        private void Bind()
        {
            if (!Roles.IsUserInRole("Administrators"))
                Response.Redirect(LinkBuilder.DashboardLink());

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(core);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            String ver = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            var resp = reader.ReadToEnd();


        }

        public void ShowCallBack()
        {

        }
    }
}
