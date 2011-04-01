using System;
using System.Linq;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IWantedListRepository
    {
        IQueryable<IWantedList> FindAll();
        IQueryable<IWantedList> FindById(Guid id);
        void Add(IWantedList entity);
        void Remove(IWantedList entity);
    }
}
