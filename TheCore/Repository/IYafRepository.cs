using System.Collections.Generic;

namespace TheCore.Repository
{
    public interface IYafRepository
    {
        IEnumerable<IYafGetRecentPostsResult> GetRecentPosts();
        IEnumerable<IYafGetRecentTopicsResult> GetRecentTopics();
        IYafGetUserResult GetUserIdFromYafId(int yafUserId);
    }
}
