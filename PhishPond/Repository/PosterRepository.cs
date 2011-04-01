using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Interfaces;
using TheCore.Helpers;
using PhishPond.Concrete;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class PosterRepository : BaseRepository<IPoster,Poster>, IPosterRepository
    {
        LogWriter writer = new LogWriter();
        private IShowRepository _showRepo;

        public PosterRepository(IPhishDatabase database, IShowRepository showRepo) : base(database) {_showRepo = showRepo; }

        public PosterRepository(IPhishDatabaseFactory factory, IShowRepository showRepo) : base(factory) {_showRepo = showRepo; }

        private IQueryable<IPoster> GetAll()
        {
            return Database.PhishPosterDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IPoster> FindAll()
        {
            return GetAll().OrderBy(poster => poster.ReleaseDate);
        }

        public IPoster FindByPosterId(Guid id)
        {
            return GetAll().SingleOrDefault(poster => poster.PosterId == id);
        }

        public IQueryable<IPoster> FindByReleaseDate(DateTime date)
        {
            return GetAll().Where(poster => poster.ReleaseDate == date);
        }

        public IQueryable<IPoster> FindByCreator(string creator)
        {
            return GetAll().Where(poster => poster.Creator == creator);
        }

        public IQueryable<IPoster> FindAllByUserId(Guid userId)
        {
            return GetAll().Where(poster => poster.UserId == userId);
        }

        public IQueryable<IPoster> FindByUserIdAndTourId(Guid userId, Guid tourId)
        {
            var shows = _showRepo.FindByTourId(tourId);

            var showIds = (from s in shows select s.ShowId).ToList();

            return (from p in FindAllByUserId(userId)
                    where showIds.Contains(p.ShowId.Value)
                    select p).Cast<IPoster>();
        }

        public IQueryable<IPoster> FindByUserIdAndShowId(Guid userId, Guid showId)
        {
            return GetAll().Where(x => x.UserId == userId && x.ShowId == showId);
        }

        public override void Add(IPoster entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            if (GetAll().Any(poster => poster.PosterId == entity.PosterId))
            {
                writer.WriteLine("A Poster with an id={0}".FormatWith(entity.PosterId));
                throw new AlreadyExistsException("A Poster with an id={0}".FormatWith(entity.PosterId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IPoster entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
