using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class ProfileService
    {
        IProfileRepository _repo;

        public ProfileService(IProfileRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IProfile GetProfileByProfileId(Guid profileId)
        {
            return _repo.FindByProfileId(profileId);
        }

        public IProfile GetProfileByUserId(Guid userId)
        {
            return _repo.FindByUserId(userId);
        }


        ///LEFT OFF HERE    left to get all nonalbum songs into Live Only then coming back here
        //public IList<IGetFavoriteVersionResult> GetFavoriteVersionsByAlbum(string album)
        //{
        //    _
        //}

        public IList<IGetFavoriteVersionResult> GetFavoriteVersions(Guid userId, string album)
        {
            if (album == "Live ONLY")
            {
                return _repo.GetFavoriteVersions(userId, album).OrderBy(x => x.SongName).ToList();
            }
            
            return _repo.GetFavoriteVersions(userId, album).ToList();
        }

        public IList<IGetAllVersions> GetAllVersions(Guid songId)
        {
            return _repo.GetAllVersions(songId).ToList();
        }

        public void SaveCommit(IProfile profile, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(profile, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IProfile profile, out bool success)
        {
            Checks.Argument.IsNotNull(profile, "profile");

            success = false;

            if (null == _repo.FindByProfileId(profile.ProfileId))
            {
                try
                {
                    _repo.Add(profile);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IProfile profile)
        {
            Checks.Argument.IsNotNull(profile, "profile");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(profile);
                u.Commit();
            }
        }

        public void Delete(IProfile profile)
        {
            Checks.Argument.IsNotNull(profile, "profile");

            _repo.Remove(profile);
        }
    }
}
