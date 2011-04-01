using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using TheCore.Helpers;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using TheCore.Interfaces;
using System.Text;
using PhishPond.Repository.LinqToSql;

namespace PhishMarket
{
    public class PhishMarketBasePage : System.Web.UI.Page
    {
        protected readonly string PhishMarketRoleType = "Registered";
        protected readonly string YAFDefaultPassword = "coredweller";
        protected readonly string DefaultShowImageLocation = "~/images/Shows/";

        protected readonly Guid EmptyGuid = new Guid("00000000-0000-0000-0000-000000000000");

        protected LinkBuilder LinkBuilder = new LinkBuilder();
        protected LogWriter log = new LogWriter();

        //var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

        public ListItem[] GetDropDownFromEnum(Type type, int startingEnumIndex, string firstItemMessage)
        {
            var jamTypeNames = Enum.GetNames(type);
            var jamTypeValue = Enum.GetValues(type);

            int numItems = jamTypeValue.Length;

            ListItem[] items = new ListItem[numItems];

            for (int i = startingEnumIndex; i < numItems; i++)
            {
                var jam = jamTypeNames[i];
                var jamValue = (int)jamTypeValue.GetValue(i);

                items[i] = new ListItem(jam, jamValue.ToString());
            }

            int val = (int)Enum.GetValues(type).GetValue(0);

            ListItem item = new ListItem(firstItemMessage, val.ToString());

            items[0] = item;

            item.Selected = true;

            return items;
        }

        public string FormatDate(DateTime? date)
        {
            return date != null ? date.Value.ToString("MM/dd/yyyy") : string.Empty;
        }

        public IProfile GetProfile()
        {
            ProfileService profileService = new ProfileService(Ioc.GetInstance<IProfileRepository>());

            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            return profileService.GetProfileByUserId(userId);
        }

        public string GetSongName(double? length, DateTime? showDate, string city, string state)
        {
            if (length == null && showDate == null && city == null && state == null)
                return string.Empty;

            StringBuilder s = new StringBuilder();
            string showDateStr = showDate.Value.ToString("MM/dd/yyyy");

            if (length != null)
                s.Append(length.Value);

            if (showDate != null)
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(showDateStr);
            }

            if (!string.IsNullOrEmpty(city))
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(city);

                if (!string.IsNullOrEmpty(state))
                {
                    s.Append(", " + state);
                }
            }

            return s.ToString();
        }

        public int DetermineRating(int? rating)
        {
            if (rating == null)
                return 0;

            return rating.Value;
        }

        public string ShortDescription(string notes, int desiredLength)
        {
            if (string.IsNullOrEmpty(notes)) return string.Empty;

            int lastIndex = notes.Length <= desiredLength? notes.Length : desiredLength;
            return notes.Substring(0, lastIndex);
        }

    }
}
