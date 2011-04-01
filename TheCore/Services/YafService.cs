using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;

namespace TheCore.Services
{
    public class YafService
    {
        IYafRepository _repo;

        public YafService(IYafRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");

            _repo = repo;
        }

        public IEnumerable<IYafGetRecentPostsResult> GetRecentPosts()
        {
            return _repo.GetRecentPosts();
        }

        public IEnumerable<IYafGetRecentTopicsResult> GetRecentTopics()
        {
            return _repo.GetRecentTopics();
        }

        public IYafGetUserResult GetUserIdFromYafId(int yafUserId)
        {
            return _repo.GetUserIdFromYafId(yafUserId);
        }
    }
}
