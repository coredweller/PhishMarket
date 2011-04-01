using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class MyShowPosterService
    {
        IMyShowPosterRepository _repo;

        public MyShowPosterService(IMyShowPosterRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IMyShowPoster> GetAllMyShowPosters()
        {
            return _repo.FindAll();
        }

        public IMyShowPoster GetMyShowPoster(Guid myShowPosterId)
        {
            return _repo.FindByMyShowPosterId(myShowPosterId);
        }

        public IQueryable<IMyShowPoster> GetMyShowPosterByMyShow(Guid myShowId)
        {
            return _repo.FindAll().Where(x => x.MyShowId == myShowId);
        }

        public IMyShowPoster GetMyShowPosterByMyShowAndPosterId(Guid myShowId, Guid posterId)
        {
            return _repo.FindAll().Where(x => x.MyShowId == myShowId && x.PosterId == posterId).SingleOrDefault();
        }

        public IList<IMyShowPoster> GetMyShowPosterByTourAndUser(Guid tourId, Guid userId)
        {
            ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var shows = showService.GetOfficialShows(tourId);

            var showIds = (from s in shows select s.ShowId).ToList();

            var myShows = (from p in myShowService.GetMyShowsForUser(userId)
                           where showIds.Contains(p.ShowId)
                           select p).ToList();

            var myShowPosters = new List<IMyShowPoster>();

            myShows.ForEach(x => 
            {
                myShowPosters.AddRange(GetMyShowPosterByMyShow(x.MyShowId));
            });

            return myShowPosters;
        }

        public KeyValuePair<IMyShowPoster, IPoster> GetAnyMyShowPosterForUser(Guid userId)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());

            var myShows = myShowService.GetMyShowsForUser(userId);

            var myShowPoster = GetAllMyShowPosters().Where(x => myShows.Any(y => y.MyShowId == x.MyShowId)).OrderByDescending(z => z.CreatedDate).FirstOrDefault();

            IPoster poster = null;

            if (myShowPoster != null)
            {
                poster = posterService.GetPoster(myShowPoster.PosterId);
            }

            return new KeyValuePair<IMyShowPoster, IPoster>(myShowPoster, poster);
        }

        public void SaveCommit(IMyShowPoster myShow, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(myShow, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IMyShowPoster myShowPoster, out bool success)
        {
            Checks.Argument.IsNotNull(myShowPoster, "myShow");

            success = false;

            if (null == _repo.FindByMyShowPosterId(myShowPoster.MyShowPosterId))
            {
                try
                {
                    _repo.Add(myShowPoster);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IMyShowPoster myShowPoster)
        {
            Checks.Argument.IsNotNull(myShowPoster, "myShowPoster");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(myShowPoster);
                u.Commit();
            }
        }

        public void Delete(IMyShowPoster myShowPoster)
        {
            Checks.Argument.IsNotNull(myShowPoster, "myShowPoster");

            _repo.Remove(myShowPoster);
        }
    }
}
