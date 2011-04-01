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
    public class PhotoRepository : BaseRepository<IPhoto, Photo>, IPhotoRepository
    {
        LogWriter writer = new LogWriter();
        public PhotoRepository(IPhishDatabase database) : base(database) { }

        public PhotoRepository(IPhishDatabaseFactory factory) : 
            base(factory) { }

        private IQueryable<IPhoto> GetAll()
        {
            return Database.PhishPhotoDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IPhoto> FindAll()
        {
            return GetAll().OrderBy(photo => photo.PhotoId);
        }

        public IPhoto FindByPhotoId(Guid id)
        {
            return GetAll().SingleOrDefault(photo => photo.PhotoId == id);
        }

        public IList<IPhoto> FindAllByUserIdAndShowId(Guid userId, Guid showId)
        {
            return GetAll().Where(photo => photo.UserId == userId && photo.ShowId == showId).ToList();
        }

        public IPhoto FindByFileName(string fileName)
        {
            return GetAll().SingleOrDefault(photo => photo.FileName == fileName);
        }

        public override void Add(IPhoto entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            if (GetAll().Any(photo => photo.PhotoId == entity.PhotoId))
            {
                writer.WriteLine("A Photo with an id={0}".FormatWith(entity.PhotoId));
                throw new AlreadyExistsException("A Photo with an id={0}".FormatWith(entity.PhotoId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IPhoto entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
