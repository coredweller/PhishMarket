using System;
using System.Collections.Generic;
using TheCore.Interfaces;
using System.Linq;

namespace TheCore.Repository
{
    public interface ISetSongRepository
    {
        IQueryable<ISetSong> FindAll();
        ISetSong FindBySetSongId(Guid id);
        ISetSong FindBySetId(Guid id);
        ISetSong FindBySongId(Guid id);
        ISetSong FindBySongIdAndSetId(Guid songId, Guid setId);
        IQueryable<ISetSong> FindAllSetSongsBySongId(Guid songId);
        void Add(ISetSong entity);
        void Remove(ISetSong entity);
    }
}
