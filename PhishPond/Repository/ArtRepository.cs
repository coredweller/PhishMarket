using System;
using System.Collections.Generic;
using System.Linq;
using PhishPond.Repository.LinqToSql;
using TheCore.Interfaces;
using PhishPond.Concrete;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class ArtRepository : BaseRepository<IArt, Art>, IArtRepository
    {
        private IShowRepository _showRepo;
        LogWriter writer = new LogWriter();

        public ArtRepository(IPhishDatabase database, IShowRepository showRepo) : base(database) { _showRepo = showRepo; }

        public ArtRepository(IPhishDatabaseFactory factory, IShowRepository showRepo) : base(factory) { _showRepo = showRepo; }

        private IQueryable<IArt> GetAll()
        {
            return Database.ArtDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IArt> FindAll()
        {
            return GetAll();
        }

        public IArt FindByArtId(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.ArtId == id);
        }

        public IQueryable<IArt> FindByShowId(Guid showId)
        {
            return GetAll().Where(x => x.ShowId == showId);
        }

        public IQueryable<IArt> FindByUserId(Guid userId)
        {
            return GetAll().Where(x => x.UserId == userId);
        }

        public IQueryable<IArt> FindByUserIdAndTourId(Guid userId, Guid tourId)
        {
            var shows = _showRepo.FindByTourId(tourId);

            var showIds = (from s in shows select s.ShowId).ToList();

            return (from a in FindByUserId(userId)
                    where showIds.Contains(a.ShowId.Value)
                    select a).Cast<IArt>();
        }

        public IQueryable<IArt> FindByUserIdAndShowId(Guid userId, Guid showId)
        {
            return GetAll().Where(x => x.UserId == userId && x.ShowId == showId);
        }

        public override void Add(IArt entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(art => art.ArtId == entity.ArtId))
            {
                writer.WriteLine("Art with an id={0}".FormatWith(entity.ArtId));
                throw new AlreadyExistsException("Art with an id={0}".FormatWith(entity.ArtId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IArt entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
    
}
