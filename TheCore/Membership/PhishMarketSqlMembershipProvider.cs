using System;
using System.Text;
using System.Web.Security;

namespace TheCore.Membership
{


    public class PhishMarketSqlMembershipProvider : SqlMembershipProvider
    {
        /// Create an array of characters to user for password reset.
        /// Exclude confusing or ambiguous characters such as 1 0 l o i
        string[] characters = new string[] { "2", "3", "4", "5", "6", "7", "8",
        "9", "a", "b", "c", "d", "e", "f", "g", "h", "j", "k", "m", "n",
        "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

        const int Default_ResetPasswordLength = 10;
        const int Default_UserNameMinimumLength = 6;
        private int ResetPasswordLength;
        private int UserNameMinimumLength;

        public PhishMarketSqlMembershipProvider()
            : base()
        {
            
        }
                                        

        public override string GeneratePassword() {
            StringBuilder passwordBuilder = new StringBuilder();
            string newPassword = string.Empty;
            Random rnd = new Random();

            for ( int i = 0 ; i < ResetPasswordLength ; i++ ) {
                passwordBuilder.Append( characters[rnd.Next( characters.GetUpperBound( 0 ) )].ToString() );
            }

            newPassword = passwordBuilder.ToString().ToLowerInvariant(); //make all lower case - all chars in the characters are lower case so this really isn't needed
            return newPassword;
        }

        /// <summary>
        /// Initializes the SQL Server membership provider with the property values specified 
        /// in the ASP.NET application's configuration file. This method is not intended to 
        /// be used directly from your code.
        /// </summary>
        /// <param name="name">The name of the System.Web.Security.SqlMembershipProvider instance to initialize.</param>
        /// <param name="config">A System.Collections.Specialized.NameValueCollection that contains the names and values of 
        /// configuration options for the membership provider.</param>
        /// <exception cref="System.ArgumentNullException">config is null.</exception>
        /// <exception cref="System.Configuration.Provider.ProviderException">
        /// The enablePasswordRetrieval, enablePasswordReset, requiresQuestionAndAnswer, or requiresUniqueEmail attribute 
        /// is set to a value other than a Boolean.  - or - The maxInvalidPasswordAttempts or the passwordAttemptWindow 
        /// attribute is set to a value other than a positive integer.  - or - The minRequiredPasswordLength attribute is 
        /// set to a value other than a positive integer, or the value is greater than 128.  - or - 
        /// The minRequiredNonalphanumericCharacters attribute is set to a value other than zero or a 
        /// positive integer, or the value is greater than 128.  - or - The value for the 
        /// passwordStrengthRegularExpression attribute is not a valid regular expression.  - or - 
        /// The applicationName attribute is set to a value that is greater than 256 characters.  - or - 
        /// The passwordFormat attribute specified in the application configuration file is an invalid 
        /// System.Web.Security.MembershipPasswordFormat enumeration.  - or - The passwordFormat attribute 
        /// is set to System.Web.Security.MembershipPasswordFormat.Hashed and the enablePasswordRetrieval 
        /// attribute is set to true in the application configuration.  - or - The passwordFormat attribute 
        /// is set to Encrypted and the machineKey configuration element specifies AutoGenerate for the 
        /// decryptionKey attribute.  - or - The connectionStringName attribute is empty or does not exist 
        /// in the application configuration.  - or - The value of the connection string for the connectionStringName 
        /// attribute value is empty, or the specified connectionStringName does not exist in the application 
        /// configuration file.  - or - The value for the commandTimeout attribute is set to a value other than 
        /// zero or a positive integer.  - or - The application configuration file for this 
        /// System.Web.Security.SqlMembershipProvider instance contains an unrecognized attribute.</exception>
        /// <exception cref="System.Web.HttpException">The current trust level is less than Low.</exception>
        /// <exception cref="System.InvalidOperationException">The provider has already been initialized prior to the current 
        /// call to the System.Web.Security.SqlMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection) method.
        /// </exception>
        public override void Initialize( string name,
                                        System.Collections.Specialized.NameValueCollection config ) {
            ResetPasswordLength = Default_ResetPasswordLength;
            UserNameMinimumLength = Default_UserNameMinimumLength;

            //string ResetPasswordLengthFromConfig = config["resetPasswordLength"];
            //if ( ResetPasswordLengthFromConfig != null ) {
            //    // remember to remove this customer configuration entry because it won't be understood by the
            //    // provider we are inheriting from.  If this removal is not performed an exception will be thrown
                config.Remove( "resetPasswordLength" );

            //    if ( !int.TryParse( ResetPasswordLengthFromConfig,
            //    out ResetPasswordLength ) ) {
            //        // If TryParse fails ResetPasswordLength will be set to 0 so use default instead
            //        ResetPasswordLength = Default_ResetPasswordLength;
            //    }
            //}
            
            //string UserNameMinimumLengthFromConfig = config["userNameMinLength"];
            //if ( UserNameMinimumLengthFromConfig != null ) {
            //    // remember to remove this customer configuration entry because it won't be understood by the
            //    // provider we are inheriting from.  If this removal is not performed an exception will be thrown
                config.Remove( "userNameMinLength" );

            //    if ( !int.TryParse( UserNameMinimumLengthFromConfig,
            //    out UserNameMinimumLength ) ) {
            //        // If TryParse fails UserNameMinimumLength will be set to 0 so use default instead
            //        UserNameMinimumLength = Default_UserNameMinimumLength;
            //    }
            //}

                                            try
                                            {
                                                base.Initialize(name, config);
                                            }
                                            catch (Exception ex)
                                            {
                                                throw ex;
                                            }
        }


        public override MembershipUser CreateUser( string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status ) {
            if ( username.Length < UserNameMinimumLength ) {
                status = MembershipCreateStatus.InvalidUserName;
                return null; 
            }

            if ( !email.IsEmail() ) {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }
            string passwordLowerCase = password.ToLowerInvariant();
            MembershipUser newUser = base.CreateUser( username, passwordLowerCase, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status );
            return newUser;
        }

        public override void UpdateUser( MembershipUser user ) {
            if ( user.UserName.Length < UserNameMinimumLength ) {
                throw new ArgumentException("UserName must be at least {0} characters long.".FormatWith(UserNameMinimumLength));
            }
            base.UpdateUser( user );
        }

        public override bool DeleteUser( string username, bool deleteAllRelatedData ) {
            MembershipUser user = this.GetUser( username, false );
            if ( user == null ) {
                return false;
            }            
            bool retVal = base.DeleteUser( user.UserName, deleteAllRelatedData );
            return retVal;
        }
        
        public virtual MembershipUser GetUser( string username ) {
            return base.GetUser( username, false );
        }

        public override string GetUserNameByEmail( string email ) {
            return base.GetUserNameByEmail( email );
        }

        public MembershipUserCollection GetAllUsers() {
            int totalRecords = 0;
            return this.GetAllUsers(0, Int32.MaxValue, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            return base.GetAllUsers( pageIndex, pageSize, out totalRecords );
        }
        
        public override string ResetPassword( string username, string passwordAnswer ) {
            return base.ResetPassword( username, passwordAnswer );
        }

        public int MinimumUserNameLength {
            get { return UserNameMinimumLength; }
        }
        
    }
}
