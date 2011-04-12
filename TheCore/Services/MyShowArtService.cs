using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Infrastructure;
using TheCore.Interfaces;

namespace TheCore.Services
{
    public class MyShowArtService
    {
        IMyShowArtRepository _repo;

        public MyShowArtService(IMyShowArtRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IMyShowArt> GetAllMyShowArt()
        {
            return _repo.FindAll();
        }

        public IMyShowArt GetMyShowArt(Guid myShowArtId)
        {
            return _repo.FindByMyShowArtId(myShowArtId);
        }

        public IQueryable<IMyShowArt> GetMyShowArtByMyShow(Guid myShowId)
        {
            return _repo.FindAll().Where(x => x.MyShowId == myShowId);
        }

        public IMyShowArt GetMyShowArtByMyShowAndArtId(Guid myShowId, Guid artId)
        {
            return _repo.FindAll().Where(x => x.MyShowId == myShowId && x.ArtId == artId).SingleOrDefault();
        }

        public IList<IMyShowArt> GetMyShowArtByTourAndUser(Guid tourId, Guid userId)
        {
            ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var shows = showService.GetOfficialShows(tourId);

            var showIds = (from s in shows select s.ShowId).ToList();

            var myShows = (from p in myShowService.GetMyShowsForUser(userId)
                           where showIds.Contains(p.ShowId)
                           select p).ToList();

            var myShowArts = new List<IMyShowArt>();

            myShows.ForEach(x => 
            {
                myShowArts.AddRange(GetMyShowArtByMyShow(x.MyShowId));
            });

            return myShowArts;
        }

        public MyShowThumbnail<IMyShowArt> GetAnyMyShowArtForUser(Guid userId)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var artService = new ArtService(Ioc.GetInstance<IArtRepository>());
            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var myShows = myShowService.GetMyShowsForUser(userId);

            var myShowArts = GetAllMyShowArt().Where(x => myShows.Any(y => y.MyShowId == x.MyShowId)).OrderByDescending(z => z.CreatedDate);

            foreach (var myShowArt in myShowArts)
            {
                var art = artService.GetArt(myShowArt.ArtId);
                var photo = photoService.GetPhotoThumbnail(art.PhotoId);
                if (photo.Thumbnail)
                    return new MyShowThumbnail<IMyShowArt>(myShowArt, photo);
            }
            
            return null;
        }

        public void SaveCommit(IMyShowArt myShow, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(myShow, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IMyShowArt myShowArt, out bool success)
        {
            Checks.Argument.IsNotNull(myShowArt, "myShow");

            success = false;

            if (null == _repo.FindByMyShowArtId(myShowArt.MyShowArtId))
            {
                try
                {
                    _repo.Add(myShowArt);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IMyShowArt myShowArt)
        {
            Checks.Argument.IsNotNull(myShowArt, "myShowArt");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(myShowArt);
                u.Commit();
            }
        }

        public void Delete(IMyShowArt myShowArt)
        {
            Checks.Argument.IsNotNull(myShowArt, "myShowArt");

            _repo.Remove(myShowArt);
        }
    }
}
