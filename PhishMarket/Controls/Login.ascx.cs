using System;
using TheCore.Helpers;

namespace PhishMarket.Controls
{
    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Login_LoggedIn(object sender, EventArgs e)
        {
            //Response.Redirect(new LinkBuilder().DashboardLink());

            //System.Web.UI.WebControls.Login loginControl = (System.Web.UI.WebControls.Login)sender;
            //String password = FormsAuthentication.HashPasswordForStoringInConfigFile(loginControl.Password, "md5");
            //Object userID = YAF.Classes.Data.DB.user_login(1, loginControl.UserName, password);

            //if (userID == DBNull.Value)
            //{
            //    YAF.Classes.Base.AdminPage adminPage = new YAF.Classes.Base.AdminPage();
            //    userID = YAF.Classes.Data.DB.user_register(adminPage, 1, loginControl.UserName,
            //        base.YAFDefaultPassword, Membership.GetUser(loginControl.UserName).Email, "", "", "-300", false);
            //}

            //string idName = string.Format("{0};{1};{2}", userID, 1, loginControl.UserName);
            //FormsAuthentication.SetAuthCookie(idName, loginControl.RememberMeSet);
        }


    }
}