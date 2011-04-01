using System;
using TheCore.Interfaces;
using System.Collections.Generic;

namespace TheCore.Repository
{
    public interface ISetRepository
    {
        void Add(ISet entity);
        ISet FindBySetId(Guid id);
        void Remove(ISet entity);
        IList<ISet> FindAll();
        IList<ISet> FindByShowId(Guid showId);
    }
}
