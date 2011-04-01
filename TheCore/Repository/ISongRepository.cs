using System;
using TheCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TheCore.Repository
{
    public interface ISongRepository
    {
        void Add(ISong entity);
        ISong FindBySongId(Guid id);
        ISong FindBySongName(string songName);
        void Remove(ISong entity);
        IQueryable<ISong> FindAll();
    }
}
