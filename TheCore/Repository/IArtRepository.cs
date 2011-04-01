using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IArtRepository
    {
        void Add(IArt entity);
        IArt FindByArtId(Guid id);
        void Remove(IArt entity);
        IQueryable<IArt> FindAll();
        IQueryable<IArt> FindByShowId(Guid showId);
        IQueryable<IArt> FindByUserId(Guid userId);
        IQueryable<IArt> FindByUserIdAndTourId(Guid userId, Guid tourId);
        IQueryable<IArt> FindByUserIdAndShowId(Guid userId, Guid showId);
    }
}
