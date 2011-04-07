using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhishPond.Repository.LinqToSql;
using TheCore.Interfaces;
using PhishPond.Concrete;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class PhishMarketUserRepository : BaseRepository<IUser, aspnet_User>, IPhishMarketUserRepository
    {
        LogWriter writer = new LogWriter();
        public PhishMarketUserRepository(IPhishDatabase database) : base(database) { }

        public PhishMarketUserRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IUser> GetAll()
        {
            return Database.UserDataSource;
        }

        public IQueryable<IUser> FindAll()
        {
            return GetAll().OrderBy(s => s.UserId);
        }

        public IUser FindByPhishMarketUserId(Guid phishMarketUserId)
        {
            return GetAll().SingleOrDefault(phishMarketUser => phishMarketUser.UserId == phishMarketUserId);
        }
    }
}
