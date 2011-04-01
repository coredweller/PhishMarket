using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhishPond.Concrete;
using TheCore.Interfaces;
using PhishPond.Repository.LinqToSql;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class MyShowRepository : BaseRepository<IMyShow, MyShow>, IMyShowRepository
    {
        LogWriter writer = new LogWriter();
        public MyShowRepository(IPhishDatabase database) : base(database) { }

        public MyShowRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IMyShow> GetAll()
        {
            return Database.MyShowDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IMyShow> FindAll()
        {
            return GetAll();
        }

        public IMyShow FindByMyShowId(Guid myShowId)
        {
            return GetAll().SingleOrDefault(myShow => myShow.MyShowId == myShowId);
        }

        public IQueryable<IMyShow> FindByShowId(Guid showId)
        {
            return GetAll().Where(myShow => myShow.ShowId == showId);
        }

        public override void Add(IMyShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(myShow => myShow.MyShowId == entity.MyShowId))
            {
                writer.WriteLine("A MyShow with an id={0}".FormatWith(entity.MyShowId));
                throw new AlreadyExistsException("A MyShow with an id={0}".FormatWith(entity.MyShowId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IMyShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
