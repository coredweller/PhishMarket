namespace TheCore.Membership
{
    using System.Web.Security;

    public class PhishMarketSqlRoleProvider : SqlRoleProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        #region IRoleProvider Members


        public bool DeleteRole(string roleName)
        {
            return base.DeleteRole(roleName, true);
        }

        #endregion

    }
}
