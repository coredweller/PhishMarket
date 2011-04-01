using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IMyShowRepository
    {
        void Add(IMyShow entity);
        void Remove(IMyShow entity);
        IMyShow FindByMyShowId(Guid myShowId);
        IQueryable<IMyShow> FindByShowId(Guid showId);
        IQueryable<IMyShow> FindAll();
    }
}
