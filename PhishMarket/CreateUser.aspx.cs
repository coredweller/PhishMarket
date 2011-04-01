using System;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace PhishMarket
{
    public partial class CreateUser : PhishMarketBasePage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void createControl_ContinueButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Logout.aspx");
        }

        public void createControl_CreatedUser(object sender, EventArgs e)
        {
            try
            {
                var cont = (CreateUserWizard)sender;

                Roles.AddUsersToRole(new string[1] { cont.UserName }, base.PhishMarketRoleType);
            }
            catch (Exception ex)
            {
                //Show error message
            }
        }
    }
}
