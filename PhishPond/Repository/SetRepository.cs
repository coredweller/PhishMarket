using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Interfaces;
using PhishPond.Concrete;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class SetRepository : BaseRepository<ISet, Set>, ISetRepository
    {
        LogWriter writer = new LogWriter();
        public SetRepository(IPhishDatabase database) : base(database) { }

        public SetRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<ISet> GetAll()
        {
            return Database.PhishSetDataSource.Where(x => x.Deleted == false);
        }

        public IList<ISet> FindAll()
        {
            return GetAll().OrderBy(s => s.SetId).ToList();
        }

        public ISet FindBySetId(Guid setId)
        {
            return GetAll().SingleOrDefault(set => set.SetId == setId);
        }

        public IList<ISet> FindByShowId(Guid showId)
        {
            return GetAll().Where(set => set.ShowId == showId).OrderBy(x => x.SetNumber).ToList();
        }

        public override void Add(ISet entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(set => set.SetId == entity.SetId))
            {
                writer.WriteLine("A Set with an id={0}".FormatWith(entity.SetId));
                throw new AlreadyExistsException("A Set with an id={0}".FormatWith(entity.SetId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ISet entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
