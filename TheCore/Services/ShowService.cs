using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class ShowService
    {
        IShowRepository _repo;

        public ShowService(IShowRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");

            _repo = repo;
        }

        public IQueryable<IShow> GetAllShows()
        {
            return _repo.FindAll();
        }

        public IShow GetShow(Guid id)
        {
            return _repo.FindByShowId(id);
        }

        public IShow GetShow(DateTime date)
        {
            return _repo.FindByShowDate(date);
        }

        public IList<IShow> GetShows(IList<DateTime> dates)
        {
            return (from date in dates
                         select GetShow(date)).ToList();
        }

        public IList<IShow> GetShows(IList<Guid> showIds)
        {
            return (from showId in showIds
                    select GetShow(showId)).ToList();
        }

        public IQueryable<IShow> GetShowsByYear(int year)
        {
            if (year != 2003)
            {
                return GetAllShows().Where(x => x.ShowDate.Value.Year == year).OrderBy(y => y.ShowDate);
            }

            //Hack to get 2002 New Years Eve into 2003 shows
            return GetAllShows().Where(x => x.ShowDate.Value.Year == 2003 || x.ShowDate.Value.Year == 2002).OrderBy(y => y.ShowDate);
        }

        public IQueryable<IShow> GetOfficialShows(Guid tourId)
        {
            return _repo.FindAll().Where(x => x.TourId == tourId && x.Official == true);
        }

        public IQueryable<IShow> GetOfficialShows()
        {
            return _repo.FindAll().Where(x => x.Official == true);
        }

        public void SaveCommit(IShow show, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(show, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IShow show, out bool success)
        {
            Checks.Argument.IsNotNull(show, "show");

            success = false;

            if (null == _repo.FindByShowId(show.ShowId))
            {
                try
                {
                    _repo.Add(show);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void Delete(IShow show)
        {
            Checks.Argument.IsNotNull(show, "show");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(show);
                u.Commit();
            }
        }

        
    }
}
