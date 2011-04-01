using System.Linq;
using TheCore.Interfaces;
using System;

namespace TheCore.Repository
{
    public interface IAnalysisRepository
    {
        IQueryable<IAnalysis> FindById(Guid id);
        IQueryable<IAnalysis> FindAll();
        void Add(IAnalysis entity);
        void Remove(IAnalysis entity);
    }
}
