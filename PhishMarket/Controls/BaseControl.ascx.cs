using TheCore.Helpers;

namespace PhishMarket.Controls
{
    public partial class BaseControl : System.Web.UI.UserControl
    {
        public LinkBuilder LinkBuilder = new LinkBuilder();

        public string ShortDescription(string notes)
        {
            int lastIndex = notes.Length <= 17 ? notes.Length : 17;
            return notes.Substring(0, lastIndex);
        }
    }
}