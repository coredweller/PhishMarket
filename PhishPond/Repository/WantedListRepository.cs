using System;
using System.Linq;
using PhishPond.Concrete;
using TheCore.Interfaces;
using PhishPond.Repository.LinqToSql;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class WantedListRepository : BaseRepository<IWantedList, WantedList>, IWantedListRepository
    {
        LogWriter writer = new LogWriter();
        public WantedListRepository(IPhishDatabase database) : base(database) { }

        public WantedListRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IWantedList> GetAll()
        {
            return Database.WantedListDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IWantedList> FindAll()
        {
            return GetAll();
        }

        public IQueryable<IWantedList> FindById(Guid id)
        {
            return FindAll().Where(x => x.WantedId == id);
        }

        public override void Add(IWantedList entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(x => x.WantedId == entity.WantedId))
            {
                writer.WriteLine("A WantedList with an id={0}".FormatWith(entity.WantedId));
                throw new AlreadyExistsException("A WantedList with an id={0}".FormatWith(entity.WantedId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IWantedList entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }

    }
}
