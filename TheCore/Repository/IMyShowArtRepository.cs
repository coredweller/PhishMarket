using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IMyShowArtRepository
    {
        void Add(IMyShowArt entity);
        void Remove(IMyShowArt entity);
        IMyShowArt FindByMyShowArtId(Guid myShowArtId);
        IQueryable<IMyShowArt> FindAll();
    }
}
