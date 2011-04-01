using System;
using System.Web.Services;

namespace PhishMarket
{
    
    public partial class JQueryTestForm : System.Web.UI.Page
    {
        private static log4net.ILog _log
= log4net.LogManager.GetLogger(
  System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            ///THIS PAGE MIGHT NOT WORK.. DELETED IOCTESTER ON 1/8/10
            /// DID NOT TEST IF THIS PAGE WORKED OR NOT SO KEEP THAT IN MIND BRIH!
            //var i = new IOCTESTER();

            Response.Redirect("~/Test3.aspx");
            _log.Debug("I AM HERE");
        }

        [WebMethod]
        public static Person GetName()
        {
            Person p = new Person("Dan", "Perillo");

            return p;
        }
    }

    public partial class Person
    {
        public string firstName;
        public string lastName;

        public Person(string first, string last)
        {
            firstName = first;
            lastName = last;
        }
    }
}

