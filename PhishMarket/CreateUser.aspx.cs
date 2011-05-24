using System;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace PhishMarket
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
    public partial class CreateUser : PhishMarketBasePage
    {
        public bool IsAdmin = Roles.IsUserInRole("Administrators");

        public void btnTestSend_Click(object sender, EventArgs e)
        {
            var message = new MailMessage
            {
                From = new MailAddress(FromEmailAddress),
                Subject = "Welcome to PhishMarket",
                IsBodyHtml = false
            };

            message.To.Add(new MailAddress("dperillo1785@gmail.com"));
            message.CC.Add(new MailAddress(CarbonCopyEmailAddress));

            message.Body = SetBody("testUserName", "testPassword");

            var client = new SmtpClient();

            client.Send(message);
        }

        private string SetBody(string userName, string password)
        {
            var s =
@"WELCOME TO PHISHMARKET.NET

User Name: {0}
Password: {1}";

            return string.Format(s, userName, password);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Create Phish Market User");
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

                //SendWelcomeEmail(cont);
            }
            catch (Exception ex)
            {
                log.WriteLine(string.Format("EXCEPTION THROWN WHEN SENDING TEST EMAIL, exception: {0} --------- inner exception: {1}", ex.Message, ex.InnerException));
            }
        }

        private void SendWelcomeEmail(CreateUserWizard c)
        {
            var message = new MailMessage
            {
                From = new MailAddress(FromEmailAddress),
                Subject = "Welcome to PhishMarket",
                IsBodyHtml = false
            };

            message.To.Add(new MailAddress(c.Email));
            message.CC.Add(new MailAddress(CarbonCopyEmailAddress));

            message.Body = SetBody(c.UserName, c.Password);

            var client = new SmtpClient();

            client.Send(message);
        }
    }
}
