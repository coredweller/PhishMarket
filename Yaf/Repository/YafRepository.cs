using System.Collections.Generic;
using TheCore.Helpers;
using TheCore.Repository;

namespace Yaf.Repository.LinqToSql
{
    public class YafRepository : IYafRepository
    {
        public IYafDatabase Database;
        
        public YafRepository(IYafDatabase database)
        {
            Checks.Argument.IsNotNull(database, "database");
            Database = database;
        }

        public YafRepository(IYafDatabaseFactory factory) : this(factory.Get()) { }

        public IEnumerable<IYafGetRecentPostsResult> GetRecentPosts()
        {
            return Database.GetRecentPosts;
        }

        public IEnumerable<IYafGetRecentTopicsResult> GetRecentTopics()
        {
            return Database.GetRecentTopics;
        }

        public IYafGetUserResult GetUserIdFromYafId(int yafUserId)
        {
            return Database.GetUserIdFromYafId(yafUserId);
        }
    }
}
