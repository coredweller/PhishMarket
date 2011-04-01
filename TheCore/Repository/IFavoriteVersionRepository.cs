using System;
using System.Collections.Generic;
using TheCore.Interfaces;
using System.Linq;

namespace TheCore.Repository
{
    public interface IFavoriteVersionRepository
    {
        void Add(IFavoriteVersion entity);
        void Remove(IFavoriteVersion entity);
        IFavoriteVersion FindAllByUserIdAndSongId(Guid userId, Guid songId);
        IFavoriteVersion FindByFavoriteVersionId(Guid favoriteVersionId);
        IQueryable<IFavoriteVersion> FindAll();
        IQueryable<IFavoriteVersion> FindByUserId(Guid userId);
    }
}
