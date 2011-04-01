using System;
using TheCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TheCore.Repository
{
    public interface IPhotoRepository
    {
        void Add(IPhoto entity);
        IPhoto FindByFileName(string fileName);
        IPhoto FindByPhotoId(Guid id);
        IQueryable<IPhoto> FindAll();
        void Remove(IPhoto entity);
        IList<IPhoto> FindAllByUserIdAndShowId(Guid userId, Guid showId);
    }
}
