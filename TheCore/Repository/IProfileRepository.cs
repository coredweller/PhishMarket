using System;
using System.Collections.Generic;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IProfileRepository
    {
        IProfile FindByProfileId(Guid id);
        void Add(IProfile entity);
        void Remove(IProfile entity);
        IProfile FindByUserId(Guid userId);
        IEnumerable<IGetFavoriteVersionResult> GetFavoriteVersions(Guid userId, string album);
        IEnumerable<IGetAllVersions> GetAllVersions(Guid songId);
    }
}
