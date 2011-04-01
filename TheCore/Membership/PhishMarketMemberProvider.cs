

namespace TheCore.Membership
{
    using System.Web.Security;

    public class PhishMarketMemberProvider : IMembershipProvider
    {
        private readonly PhishMarketSqlMembershipProvider provider;

        public PhishMarketMemberProvider()
        {
            provider = Membership.Provider as PhishMarketSqlMembershipProvider;
        }
        public string GeneratePassword() {
            return provider.GeneratePassword();
        }
        public int MinRequiredPasswordLength {
            get {
                return provider.MinRequiredPasswordLength;
            }
        }

        public int MinimumUserNameLength {
            get { return provider.MinimumUserNameLength; }
        }

        public MembershipUser CreateUser( string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status ) {
            return provider.CreateUser( username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status );
        }
        public void UpdateUser( MembershipUser user ) {
            provider.UpdateUser( user );
        }
        public bool DeleteUser( string username, bool deleteAllRelatedData ) {
            return provider.DeleteUser( username, deleteAllRelatedData );
        }
        public bool ValidateUser( string userName, string password ) {
            return provider.ValidateUser( userName, password );
        }

        public MembershipUser GetUser( string username ) {
            return this.GetUser( username, false );
        }

        public MembershipUser GetUser( string username, bool userIsOnline ) {
            return provider.GetUser( username, userIsOnline );
        }

        public MembershipUser GetUserByEmail( string email ) {
            var username = provider.GetUserNameByEmail( email );
            if ( username == null ) { return null; }
            return this.GetUser( username );
        }

        public string GetUserNameByEmail( string email ) {
            return provider.GetUserNameByEmail( email );
        }

        public MembershipUserCollection GetAllUsers() {
            return provider.GetAllUsers();
        }
        public MembershipUserCollection GetAllUsers( int pageIndex, int pageSize, out int totalRecords ) {
            return provider.GetAllUsers( pageIndex, pageSize, out totalRecords );
        }
        
        public string ResetPassword( string username, string passwordAnswer ) {
            return provider.ResetPassword( username, passwordAnswer );
        }

        
        //
        // Summary:
        //     Returns the password for the specified user name from the SQL Server membership
        //     database.
        //
        // Parameters:
        //   username:
        //     The user to retrieve the password for.
        //
        //   passwordAnswer:
        //     The password answer for the user.
        //
        // Returns:
        //     The password for the specified user name.
        //
        // Exceptions:
        //   System.Web.Security.MembershipPasswordException:
        //     passwordAnswer is invalid. - or -The membership user identified by username
        //     is locked out.
        //
        //   System.NotSupportedException:
        //     System.Web.Security.SqlMembershipProvider.EnablePasswordRetrieval is set
        //     to false.
        //
        //   System.Configuration.Provider.ProviderException:
        //     username is not found in the membership database.- or -An error occurred
        //     while retrieving the password from the database.
        //
        //   System.ArgumentException:
        //     One of the parameter values exceeds the maximum allowed length.- or -username
        //     is an empty string (""), contains a comma, or is longer than 256 characters.-
        //     or -passwordAnswer is an empty string and System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer
        //     is true.- or -passwordAnswer is greater than 128 characters.- or -The encoded
        //     version of passwordAnswer is greater than 128 characters.
        //
        //   System.ArgumentNullException:
        //     username is null.- or -passwordAnswer is null and System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer
        //     is true.
        public string GetPassword( string username, string passwordAnswer ) {
            return provider.GetPassword( username, passwordAnswer );
        }
    }
}
