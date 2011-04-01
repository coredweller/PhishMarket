using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IMyShowPosterRepository
    {
        void Add(IMyShowPoster entity);
        void Remove(IMyShowPoster entity);
        IMyShowPoster FindByMyShowPosterId(Guid myShowPosterId);
        IQueryable<IMyShowPoster> FindAll();
    }
}
