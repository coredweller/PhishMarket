using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Interfaces;
using PhishPond.Concrete;
using PhishPond.Repository.LinqToSql;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class FavoriteVersionRepository : BaseRepository<IFavoriteVersion, FavoriteVersion>, IFavoriteVersionRepository
    {
        LogWriter writer = new LogWriter();
        public FavoriteVersionRepository(IPhishDatabase database) : base(database) { }

        public FavoriteVersionRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IFavoriteVersion> GetAll()
        {
            return Database.FavoriteVersionDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IFavoriteVersion> FindAll()
        {
            return GetAll();
        }

        public IFavoriteVersion FindByFavoriteVersionId(Guid favoriteVersionId)
        {
            return GetAll().Where(x => x.FavoriteVersionId == favoriteVersionId).SingleOrDefault();
        }

        public IQueryable<IFavoriteVersion> FindByUserId(Guid userId)
        {
            var items = FindAll().Where(x => x.UserId == userId);

            return items == null ? null : items;
        }

        public IFavoriteVersion FindAllByUserIdAndSongId(Guid userId, Guid songId)
        {
            return GetAll().Where(x => x.UserId == userId && x.SongId == songId).SingleOrDefault();
        }

        public override void Add(IFavoriteVersion entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(favoriteVersion => favoriteVersion.FavoriteVersionId == entity.FavoriteVersionId))
            {
                writer.WriteLine("A FavoriteVersion with an id={0}".FormatWith(entity.FavoriteVersionId));
                throw new AlreadyExistsException("A FavoriteVersion with an id={0}".FormatWith(entity.FavoriteVersionId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IFavoriteVersion entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
