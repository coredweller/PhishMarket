using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Infrastructure;
using TheCore.Interfaces;

namespace TheCore.Services
{
    public class PosterService
    {
        IPosterRepository _repo;

        public PosterService(IPosterRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IPoster> GetAllPosters()
        {
            return _repo.FindAll();
        }

        public IPoster GetPoster(Guid id)
        {
            return _repo.FindByPosterId(id);
        }

        public IQueryable<IPoster> GetPostersByReleaseDate(DateTime date)
        {
            return _repo.FindByReleaseDate(date);
        }

        public IQueryable<IPoster> GetPostersByCreator(string creator)
        {
            return _repo.FindByCreator(creator);
        }

        public IQueryable<IPoster> GetByUserAndTour(Guid userId, Guid tourId)
        {
            return _repo.FindByUserIdAndTourId(userId, tourId);
        }

        public IQueryable<IPoster> GetByUserAndShow(Guid userId, Guid showId)
        {
            return _repo.FindByUserIdAndShowId(userId, showId);
        }

        public IQueryable<IPoster> GetByUser(Guid userId)
        {
            return _repo.FindAll().Where(x => x.UserId == userId);
        }

        public void SaveCommit(IPoster poster, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(poster, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(IPoster poster, out bool success)
        {
            Checks.Argument.IsNotNull(poster, "poster");

            success = false;

            if (null == _repo.FindByPosterId(poster.PosterId))
            {
                try
                {
                    _repo.Add(poster);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete(IPoster poster)
        {
            Checks.Argument.IsNotNull(poster, "poster");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(poster);
                u.Commit();
            }
        }
    }
}
