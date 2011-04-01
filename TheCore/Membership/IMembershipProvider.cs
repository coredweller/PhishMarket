using System.Web.Security;

namespace TheCore.Membership
{
    public interface IMembershipProvider
    {
        string GeneratePassword();
        int MinRequiredPasswordLength { get; }
        int MinimumUserNameLength { get; }
        MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);
        void UpdateUser(MembershipUser user);
        bool DeleteUser(string username, bool deleteAllRelatedData);
        bool ValidateUser(string userName, string password);
        MembershipUser GetUser(string username);
        MembershipUser GetUser(string username, bool userIsOnline);
        MembershipUser GetUserByEmail(string email);
        string GetUserNameByEmail(string email);
        MembershipUserCollection GetAllUsers();
        MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);

        string ResetPassword(string username, string passwordAnswer);
        string GetPassword(string username, string passwordAnswer);

    }
}
