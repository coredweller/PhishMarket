using System;
using TheCore.Interfaces;
using System.Collections.Generic;

namespace TheCore.Repository
{
    public interface IVideoRepository
    {
        void Add(IVideo entity);
        IVideo FindByVideoId(Guid id);
        IList<IVideo> FindAll();
        void Remove(IVideo entity);
    }
}
