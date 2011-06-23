using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class MyShowService
    {
        IMyShowRepository _repo;

        public MyShowService(IMyShowRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IMyShow> GetAllMyShows()
        {
            return _repo.FindAll();
        }

        public IMyShow GetMyShow(Guid myShowId)
        {
            return _repo.FindByMyShowId(myShowId);
        }

        public IQueryable<IMyShow> GetMyShowsForShow(Guid showId)
        {
            return _repo.FindByShowId(showId);
        }

        public IQueryable<IMyShow> GetMyShowsForUser(Guid userId)
        {
            return _repo.FindAll().Where(x => x.UserId == userId);
        }

        public IList<IShow> GetShowsNotInUsersMyShows(Guid userId, IList<IShow> shows)
        {
            var userShows = GetMyShowsForUser(userId);

            var ids = (from u in userShows
                       select u.ShowId);

            return (from show in shows
                         where !ids.Contains(show.ShowId)
                         select show).OrderBy(x => x.ShowDate).ToList();
        }

        public IList<IShow> GetShowsFromMyShowsForUser(Guid userId, int year)
        {
            var showIds = GetShowIdsFromMyShows(userId);

            if (showIds == null || showIds.Count() <= 0) return null;

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var shows = showService.GetShowsByYear(year).ToList();

            return (from show in shows
                    where showIds.Contains(show.ShowId)
                    select show).OrderBy(x => x.ShowDate.Value).ToList();
        }

        public IList<IShow> GetShowsFromMyShowsForUser(Guid userId, Guid tourId)
        {
            var showIds = GetShowIdsFromMyShows(userId);

            if (showIds == null || showIds.Count() <= 0) return null;

            TourService service = new TourService(Ioc.GetInstance<ITourRepository>());
            var tour = service.GetTour(tourId);

            return (from show in tour.Shows
                         where showIds.Contains(show.ShowId)
                         select show).OrderBy(x => x.ShowDate).ToList();
        }

        public IList<IShow> GetShowsFromMyShowsForUser(Guid userId)
        {
            var showIds = GetShowIdsFromMyShows(userId);

            if (showIds == null || showIds.Count() <= 0) return null;

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            return (from show in showService.GetAllShows().ToList()
                         where showIds.Contains(show.ShowId)
                         select show).OrderBy(x => x.ShowDate).ToList();
        }


        private IList<Guid> GetShowIdsFromMyShows(Guid userId)
        {
            var myShows = GetMyShowsForUser(userId);

            if (myShows == null || myShows.Count() <= 0)
                return null;

            return myShows.Select(x => x.ShowId).ToList();
        }

        public IList<IShow> GetShowsNotInUsersMyShows(Guid userId, Guid tourId)
        {
            TourService service = new TourService(Ioc.GetInstance<ITourRepository>());
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var myShows = GetMyShowsForUser(userId);

            var tour = service.GetTour(tourId);

            if (myShows == null || myShows.Count() <= 0) //then just return all of them
                return showService.GetAllShows().Where(x => x.TourId == tourId).OrderBy(y => y.ShowDate).ToList();

            var showIds = (from show in myShows
                           select show.ShowId).ToList();

            return showService.GetAllShows().Where(x => x.TourId == tourId && !showIds.Contains(x.ShowId)).OrderBy(y => y.ShowDate).ToList();
        }

        public IList<KeyValuePair<IShow, IMyShow>> GetMyShowsFromMyShowsForUser(Guid userId)
        {
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShows = myShowService.GetMyShowsForUser(userId);

            if (myShows == null || myShows.Count() <= 0)
                return null;

            var showIds = (from show in myShows
                           select show.ShowId).ToList();

            var shows = (from show in showService.GetAllShows()
                         from myShow in myShows.Where(x => x.ShowId == show.ShowId)
                         where showIds.Contains(show.ShowId)
                         select new { Show = show, MyShow = myShow }).OrderBy(x => x.Show.ShowDate).ToList();

            var showList = new List<KeyValuePair<IShow, IMyShow>>();

            shows.ForEach(x =>{ showList.Add(new KeyValuePair<IShow, IMyShow>(x.Show, x.MyShow)); });

            return showList;
        }

        public IMyShow GetMyShow(Guid showId, Guid userId)
        {
            return _repo.FindAll().Where(x => x.ShowId == showId && x.UserId == userId).SingleOrDefault();
        }

        public void SaveCommit(IMyShow myShow, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(myShow, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IMyShow myShow, out bool success)
        {
            Checks.Argument.IsNotNull(myShow, "myShow");

            success = false;

            if (null == _repo.FindByMyShowId(myShow.MyShowId))
            {
                try
                {
                    _repo.Add(myShow);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IMyShow myShow)
        {
            Checks.Argument.IsNotNull(myShow, "myShow");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(myShow);
                u.Commit();
            }
        }

        public void Delete(IMyShow myShow)
        {
            Checks.Argument.IsNotNull(myShow, "myShow");

            _repo.Remove(myShow);
        }
    }
}
