using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Helpers;
using TheCore.Interfaces;
using PhishPond.Concrete;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class VideoRepository : BaseRepository<IVideo,Video>, IVideoRepository
    {
        LogWriter writer = new LogWriter();
        public VideoRepository(IPhishDatabase database) : base(database) { }
        public VideoRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IVideo> GetAll()
        {
            return Database.PhishVideoDataSource.Where(x => x.Deleted == false);
        }

        public IList<IVideo> FindAll()
        {
            return GetAll().OrderBy(video => video.VideoId).ToList();
        }

        public IVideo FindByVideoId(Guid id)
        {
            return GetAll().SingleOrDefault(video => video.VideoId == id);
        }

        public override void Add(IVideo entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            if (GetAll().Any(video => video.VideoId == entity.VideoId))
            {
                writer.WriteLine("A Video with an id={0}".FormatWith(entity.VideoId));
                throw new AlreadyExistsException("A Video with an id={0}".FormatWith(entity.VideoId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IVideo entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
