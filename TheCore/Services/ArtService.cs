using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Helpers;
using TheCore.Infrastructure;
using TheCore.Interfaces;
using TheCore.Repository;

namespace TheCore.Services
{
    public class ArtService
    {
        IArtRepository _repo;

        public ArtService(IArtRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IArt> GetAllArt()
        {
            return _repo.FindAll();
        }

        public IArt GetArt(Guid artId)
        {
            return _repo.FindByArtId(artId);
        }

        public IQueryable<IArt> GetArtFromShow(Guid showId)
        {
            return _repo.FindByShowId(showId);
        }
            
        public IQueryable<IArt> GetArtByUserAndShow(Guid userId, Guid showId)
        {
            return _repo.FindByUserIdAndShowId(userId, showId);
        }

        public IQueryable<IArt> GetArtByUser(Guid userId)
        {
            return _repo.FindByUserId(userId);
        }

        public IQueryable<IArt> GetArtByUserAndTour(Guid userId, Guid tourId)
        {
            return _repo.FindByUserIdAndTourId(userId, tourId);
        }

        public void SaveCommit(IArt art, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(art, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IArt art, out bool success)
        {
            Checks.Argument.IsNotNull(art, "art");

            success = false;

            if (null == _repo.FindByArtId(art.ArtId))
            {
                try
                {
                    _repo.Add(art);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IArt art)
        {
            Checks.Argument.IsNotNull(art, "art");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(art);
                u.Commit();
            }
        }

        public void Delete(IArt art)
        {
            Checks.Argument.IsNotNull(art, "art");

            _repo.Remove(art);
        }
    }
}
