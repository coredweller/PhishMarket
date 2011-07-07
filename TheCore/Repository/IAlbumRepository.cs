using System;
using System.Linq;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IAlbumRepository
    {
        IQueryable<IAlbum> FindAll();
        IAlbum FindByAlbumId(Guid albumId);
        void Add(IAlbum entity);
        void Remove(IAlbum entity);
    }
}
