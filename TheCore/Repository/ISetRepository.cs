using System;
using TheCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TheCore.Repository
{
    public interface ISetRepository
    {
        void Add(ISet entity);
        ISet FindBySetId(Guid id);
        void Remove(ISet entity);
        IQueryable<ISet> FindAll();
        IQueryable<ISet> FindByShowId(Guid showId);
    }
}
