using System;
using System.Linq;
using TheCore.Interfaces;
using PhishPond.Concrete;
using PhishPond.Repository.LinqToSql;
using TheCore.Helpers;
using TheCore.Exceptions;
using TheCore.Repository;

namespace PhishPond.Repository
{
    public class AlbumRepository : BaseRepository<IAlbum, Album>, IAlbumRepository
    {
        LogWriter writer = new LogWriter();
        public AlbumRepository(IPhishDatabase database) : base(database) { }

        public AlbumRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IAlbum> GetAll()
        {
            return Database.AlbumDataSource;
        }

        public IQueryable<IAlbum> FindAll()
        {
            return GetAll().OrderBy(s => s.AlbumId);
        }

        public IAlbum FindByAlbumId(Guid albumId)
        {
            return GetAll().SingleOrDefault(album => album.AlbumId == albumId);
        }

        public override void Add(IAlbum entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(album => album.AlbumId == entity.AlbumId))
            {
                writer.WriteLine("An Album with an id={0}".FormatWith(entity.AlbumId));
                throw new AlreadyExistsException("An Album with an id={0}".FormatWith(entity.AlbumId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IAlbum entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
