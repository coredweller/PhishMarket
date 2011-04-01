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
    public class ShowRepository : BaseRepository<IShow, Show>,  IShowRepository
    {
        LogWriter writer = new LogWriter();
        public ShowRepository(IPhishDatabase database) : base(database) { }

        public ShowRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IShow> GetAll()
        {
            return Database.PhishShowDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IShow> FindAll()
        {
            return GetAll().OrderBy(s => s.ShowDate);
        }

        public IShow FindByShowId(Guid id)
        {
            return GetAll().SingleOrDefault(show => show.ShowId == id);
        }

        public IQueryable<IShow> FindShowsBeforeDate(DateTime date)
        {
            return GetAll().Where(shows => shows.ShowDate <= date).OrderBy(s => s.Order);
        }

        public IQueryable<IShow> FindShowsAfterDate(DateTime date)
        {
            return GetAll().Where(shows => shows.ShowDate >= date).OrderBy(s => s.Order);
        }

        public IShow FindByShowDate(DateTime date)
        {
            return GetAll().SingleOrDefault(show => show.ShowDate == date);
        }

        public IQueryable<IShow> FindByTourId(Guid tourId)
        {
            return GetAll().Where(x => x.TourId == tourId);
        }

        public override void Add(IShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(show => show.ShowId == entity.ShowId))
            {
                writer.WriteLine("A Show with an id={0}".FormatWith(entity.ShowId));
                throw new AlreadyExistsException("A Show with an id={0}".FormatWith(entity.ShowId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
