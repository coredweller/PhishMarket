using System;
using System.Collections.Generic;
using System.Linq;
using PhishPond.Concrete;
using TheCore.Interfaces;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class TourRepository : BaseRepository<ITour, Tour>, ITourRepository
    {
        LogWriter writer = new LogWriter();
        private readonly IShowRepository _showRepo;

        public TourRepository(IPhishDatabase database, IShowRepository showRepo) : base(database) { _showRepo = showRepo; }

        public TourRepository(IPhishDatabaseFactory factory, IShowRepository showRepo) : base(factory) { _showRepo = showRepo; }

        private IQueryable<ITour> GetAll()
        {
            return Database.PhishTourDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<ITour> FindAll()
        {
            return GetAll();
        }

        public ITour FindByTourId(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.TourId == id);
        }

        public ITour FindByTourName(string tourName)
        {
            return GetAll().SingleOrDefault(x => x.TourName.ToLower() == tourName.ToLower());
        }

        public override void Add(ITour entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            entity = ValidateTour(entity);

            if (GetAll().Any(tour => tour.TourId == entity.TourId))
            {
                writer.WriteLine("A Tour with an id={0}".FormatWith(entity.TourId));
                throw new AlreadyExistsException("A Tour with an id={0}".FormatWith(entity.TourId));
            }
            else
            {
                base.Add(entity);
            }
        }

        private ITour ValidateTour(ITour tour)
        {
            if (!tour.EndDate.HasValue)
                tour.EndDate = null;

            if (!tour.StartDate.HasValue)
                tour.StartDate = null;

            return tour;
        }

        public override void Remove(ITour entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
