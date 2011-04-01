using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Infrastructure;
using TheCore.Helpers;
using TheCore.Interfaces;
using System.Linq;

namespace TheCore.Services
{
    public class FavoriteVersionService
    {
        IFavoriteVersionRepository _repo;

        public FavoriteVersionService(IFavoriteVersionRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IFavoriteVersion> GetAllFavoriteVersions()
        {
            return _repo.FindAll();
        }

        public IFavoriteVersion GetFavoriteVersion(Guid favoriteVersionId)
        {
            return _repo.FindByFavoriteVersionId(favoriteVersionId);
        }

        public IFavoriteVersion GetFavoriteVersionByUserIdAndSongId(Guid userId, Guid songId)
        {
            return _repo.FindAllByUserIdAndSongId(userId, songId);
        }

        public IQueryable<IFavoriteVersion> GetFavoriteVersionsByUserId(Guid userId)
        {
            return _repo.FindByUserId(userId);
        }

        public void SaveCommit(IFavoriteVersion favoriteVersion, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(favoriteVersion, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IFavoriteVersion favoriteVersion, out bool success)
        {
            Checks.Argument.IsNotNull(favoriteVersion, "favoriteVersion");

            success = false;

            if (null == _repo.FindByFavoriteVersionId(favoriteVersion.FavoriteVersionId))
            {
                try
                {
                    _repo.Add(favoriteVersion);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IFavoriteVersion favoriteVersion)
        {
            Checks.Argument.IsNotNull(favoriteVersion, "favoriteVersion");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(favoriteVersion);
                u.Commit();
            }
        }

        public void Delete(IFavoriteVersion favoriteVersion)
        {
            Checks.Argument.IsNotNull(favoriteVersion, "favoriteVersion");

            _repo.Remove(favoriteVersion);
        }
    }
}
