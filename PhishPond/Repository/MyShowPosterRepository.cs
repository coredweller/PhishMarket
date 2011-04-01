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
    public class MyShowPosterRepository : BaseRepository<IMyShowPoster, MyShowPoster>, IMyShowPosterRepository
    {
        LogWriter writer = new LogWriter();
        public MyShowPosterRepository(IPhishDatabase database) : base(database) { }

        public MyShowPosterRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IMyShowPoster> GetAll()
        {
            return Database.MyShowPosterDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IMyShowPoster> FindAll()
        {
            return GetAll().OrderBy(s => s.MyShowId);
        }

        public IMyShowPoster FindByMyShowPosterId(Guid myShowPosterId)
        {
            return GetAll().SingleOrDefault(myShow => myShow.MyShowPosterId == myShowPosterId);
        }

        public override void Add(IMyShowPoster entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(myShow => myShow.MyShowPosterId == entity.MyShowPosterId))
            {
                writer.WriteLine("A MyShowPoster with an id={0}".FormatWith(entity.MyShowPosterId));
                throw new AlreadyExistsException("A MyShowPoster with an id={0}".FormatWith(entity.MyShowPosterId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IMyShowPoster entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
