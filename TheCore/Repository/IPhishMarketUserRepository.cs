using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IPhishMarketUserRepository
    {
        IQueryable<IUser> FindAll();
        IUser FindByPhishMarketUserId(Guid phishMarketUserId);
    }
}
