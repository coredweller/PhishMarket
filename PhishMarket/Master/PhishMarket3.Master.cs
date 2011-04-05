using System;
using System.Web.Security;

namespace PhishMarket.Master
{
    public partial class PhishMarket3 : System.Web.UI.MasterPage
    {
        public bool IsAdmin = Roles.IsUserInRole("Administrators");

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}
