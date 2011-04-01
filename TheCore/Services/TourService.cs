using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;
using System.Linq;

namespace TheCore.Services
{
    public class TourService
    {
        ITourRepository _repo;

        public TourService(ITourRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<ITour> GetAllTours()
        {
            return _repo.FindAll();
        }

        public IList<ITour> GetAllToursDescending()
        {
            return GetAllTours().OrderByDescending(x => x.StartDate).ToList();
        }

        public ITour GetTour(Guid id)
        {
            return _repo.FindByTourId(id);
        }

        public ITour GetTour(string name)
        {
            return _repo.FindByTourName(name);
        }

        public void SaveCommit(ITour tour, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(tour, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(ITour tour, out bool success)
        {
            Checks.Argument.IsNotNull(tour, "tour");

            success = false;

            if (null == _repo.FindByTourId(tour.TourId))
            {
                try
                {
                    _repo.Add(tour);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete( ITour tour )
        {
            Checks.Argument.IsNotNull(tour, "tour");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(tour);
                u.Commit();
            }
        }
    }
}
