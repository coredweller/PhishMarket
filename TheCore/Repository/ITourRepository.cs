using System;
using TheCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TheCore.Repository
{
    public interface ITourRepository
    {
        void Add(ITour entity);
        ITour FindByTourId(Guid id);
        ITour FindByTourName(string tourName);
        void Remove(ITour entity);

        IQueryable<ITour> FindAll();
    }
}
