using System;
using System.Linq;
using TheCore.Interfaces;
using PhishPond.Concrete;
using PhishPond.Repository.LinqToSql;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class AnalysisRepository : BaseRepository<IAnalysis, Analysis>, IAnalysisRepository
    {
        LogWriter writer = new LogWriter();

        public AnalysisRepository(IPhishDatabase database) : base(database) { }

        public AnalysisRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IAnalysis> GetAll()
        {
            return Database.AnalysisDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IAnalysis> FindAll()
        {
            return GetAll();
        }

        public IQueryable<IAnalysis> FindById(Guid id)
        {
            return FindAll().Where(x => x.AnalysisId == id);
        }

        public override void Add(IAnalysis entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(x => x.AnalysisId == entity.AnalysisId))
            {
                writer.WriteLine("An Analysis with an id={0}".FormatWith(entity.AnalysisId));
                throw new AlreadyExistsException("An Analysis with an id={0}".FormatWith(entity.AnalysisId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IAnalysis entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
