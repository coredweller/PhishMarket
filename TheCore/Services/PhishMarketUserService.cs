using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;

namespace TheCore.Services
{
    public class PhishMarketUserService
    {
        IPhishMarketUserRepository _repo;

        public PhishMarketUserService(IPhishMarketUserRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IUser> GetAllUsers()
        {
            return _repo.FindAll();
        }

        public IUser GetUserById(Guid userId)
        {
            return _repo.FindByPhishMarketUserId(userId);
        }
    }
}
