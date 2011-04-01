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
    public class SongRepository : BaseRepository<ISong, Song>, ISongRepository
    {
        LogWriter writer = new LogWriter();
        public SongRepository(IPhishDatabase database) : base(database) { }

        public SongRepository(IPhishDatabaseFactory factory) : base(factory) { }

        public IQueryable<ISong> GetAll()
        {
            return Database.PhishSongDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<ISong> FindAll()
        {
            return GetAll();
        }

        public ISong FindBySongId(Guid id)
        {
            return GetAll().SingleOrDefault(song => song.SongId == id);
        }

        public ISong FindBySongName(string songName)
        {
            return GetAll().FirstOrDefault(song => song.SongName == songName);
        }

        public override void Add(ISong entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            ValidateSong(entity);

            if (GetAll().Any(song => song.SongId == entity.SongId))// || song.SongName == entity.SongName))
            {
                writer.WriteLine("A Song with an id={0}".FormatWith(entity.SongId));
                throw new AlreadyExistsException("A Song with an id={0}".FormatWith(entity.SongId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public void ValidateSong(ISong song)
        {
            if (!song.Length.HasValue)
                song.Length = 0;
        }

        public override void Remove(ISong entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
