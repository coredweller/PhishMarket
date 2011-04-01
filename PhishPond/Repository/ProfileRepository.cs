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
    public class ProfileRepository : BaseRepository<IProfile, Profile>, IProfileRepository
    {
        LogWriter writer = new LogWriter();
        public ProfileRepository(IPhishDatabase database) : base(database) { }

        public ProfileRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IProfile> GetAll()
        {
            return Database.ProfileDataSource.Where(x => x.Deleted == false);
        }

        //public IList<IProfile> FindAll()
        //{
        //    return GetAll().OrderBy(profile => profile.ReleaseDate).ToList();
        //}

        public IProfile FindByProfileId(Guid id)
        {
            return GetAll().SingleOrDefault(profile => profile.ProfileId == id);
        }

        public IEnumerable<IGetFavoriteVersionResult> GetFavoriteVersions(Guid userId, string album)
        {
            return Database.GetFavoriteVersions(userId, album);
        }

        public IEnumerable<IGetAllVersions> GetAllVersions(Guid songId)
        {
            return Database.GetAllVersions(songId);
        }

        public override void Add(IProfile entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            if (GetAll().Any(profile => profile.ProfileId == entity.ProfileId))
            {
                writer.WriteLine("A Profile with an id={0}".FormatWith(entity.ProfileId));
                throw new AlreadyExistsException("A Profile with an id={0}".FormatWith(entity.ProfileId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public IProfile FindByUserId(Guid userId)
        {
            return GetAll().SingleOrDefault(profile => profile.UserId == userId);
        }

        public override void Remove(IProfile entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
