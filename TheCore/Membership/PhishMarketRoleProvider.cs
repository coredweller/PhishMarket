namespace TheCore.Membership
{

    public class PhishMarketRoleProvider : IRoleProvider
    {
        private readonly PhishMarketSqlRoleProvider provider;

        public PhishMarketRoleProvider() {
            provider = System.Web.Security.Roles.Provider as PhishMarketSqlRoleProvider;
        }
        public bool IsUserInRole( string username, string roleName ) {
            return provider.IsUserInRole( username, roleName );
        }
        public string[] GetAllRoles() {
            return provider.GetAllRoles();
        }
        public string[] GetUsersInRole( string roleName ) {
            return provider.GetUsersInRole(roleName);
        }

        public bool RoleExists( string roleName ) {
            return provider.RoleExists( roleName );
        }
        public void CreateRole( string roleName ) {
            provider.CreateRole( roleName );
        }
        
        public void AddUsersToRoles( string[] usernames, string[] roleNames ) {
            provider.AddUsersToRoles( usernames, roleNames );
        }

        public void RemoveUsersFromRoles( string[] usernames, string[] roleNames ) {
            provider.RemoveUsersFromRoles( usernames, roleNames );
        }
        public bool DeleteRole( string roleName ) {
            return provider.DeleteRole( roleName );
        }
    }

    public interface IRoleProvider
    {
        bool IsUserInRole( string username, string roleName );
        string[] GetAllRoles();
        string[] GetUsersInRole( string roleName );
        bool RoleExists( string roleName );
        void CreateRole( string roleName );
        void AddUsersToRoles( string[] usernames, string[] roleNames );
        void RemoveUsersFromRoles( string[] usernames, string[] roleNames );
        bool DeleteRole( string roleName);
    }
    
}
