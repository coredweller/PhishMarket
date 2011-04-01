using System;
using TheCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TheCore.Repository
{
    public interface IPosterRepository
    {
        void Add(IPoster entity);
        IPoster FindByPosterId(Guid id);
        IQueryable<IPoster> FindByReleaseDate(DateTime date);
        IQueryable<IPoster> FindByCreator(string creator);
        IQueryable<IPoster> FindAll();
        void Remove(IPoster entity);
        IQueryable<IPoster> FindByUserIdAndTourId(Guid userId, Guid tourId);
        IQueryable<IPoster> FindByUserIdAndShowId(Guid userId, Guid showId);
    }
}
