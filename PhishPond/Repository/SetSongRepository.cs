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
    public class SetSongRepository : BaseRepository<ISetSong, SetSong>, ISetSongRepository
    {
        LogWriter writer = new LogWriter();
        public SetSongRepository(IPhishDatabase database) : base(database) { }

        public SetSongRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<ISetSong> GetAll()
        {
            return Database.SetSongDataSource.Where(setSong => setSong.Deleted == false);
        }

        public IQueryable<ISetSong> FindAll()
        {
            return GetAll();
        }

        public ISetSong FindBySetSongId(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.SetSongId == id);
        }

        public ISetSong FindBySetId(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.SetId == id);
        }

        public ISetSong FindBySongId(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.SongId == id);
        }

        public ISetSong FindBySongIdAndSetId(Guid songId, Guid setId)
        {
            return GetAll().SingleOrDefault(x => x.SongId == songId && x.SetId == setId);
        }


        public IQueryable<ISetSong> FindAllSetSongsBySongId(Guid songId)
        {
            return GetAll().Where(x => x.SongId == songId);
        }

        public override void Add(ISetSong entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;


            if (GetAll().Any(x => x.SetSongId == entity.SetSongId))
            {
                writer.WriteLine("A SetSong with an id={0}".FormatWith(entity.SetSongId));
                throw new AlreadyExistsException("A SetSong with an id={0}".FormatWith(entity.SetSongId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ISetSong entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
