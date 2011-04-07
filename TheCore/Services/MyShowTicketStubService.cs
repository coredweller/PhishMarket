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
    public class MyShowTicketStubService
    {
        IMyShowTicketStubRepository _repo;

        public MyShowTicketStubService(IMyShowTicketStubRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IMyShowTicketStub> GetAllMyShowTicketStubs()
        {
            return _repo.FindAll();
        }

        public IMyShowTicketStub GetMyShowTicketStub(Guid myShowTicketStubId)
        {
            return _repo.FindByMyShowTicketStubId(myShowTicketStubId);
        }

        public IQueryable<IMyShowTicketStub> GetMyShowTicketStubByMyShow(Guid myShowId)
        {
            return _repo.FindAll().Where(x => x.MyShowId == myShowId);
        }

        public IMyShowTicketStub GetMyShowTicketStubByMyShowAndTicketStubId(Guid myShowId, Guid ticketStubId)
        {
            return _repo.FindAll().Where(x => x.MyShowId == myShowId && x.TicketStubId == ticketStubId).SingleOrDefault();
        }

        public IList<IMyShowTicketStub> GetMyShowTicketStubByTourAndUser(Guid tourId, Guid userId)
        {
            ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var shows = showService.GetOfficialShows(tourId);

            var showIds = (from s in shows select s.ShowId).ToList();

            var myShows = (from p in myShowService.GetMyShowsForUser(userId)
                           where showIds.Contains(p.ShowId)
                           select p).ToList();

            var myShowTicketStubs = new List<IMyShowTicketStub>();

            myShows.ForEach(x => 
            {
                myShowTicketStubs.AddRange(GetMyShowTicketStubByMyShow(x.MyShowId));
            });

            return myShowTicketStubs;
        }

        public MyShowThumbnail<IMyShowTicketStub> GetAnyMyShowTicketStubForUser(Guid userId)
        {
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());
            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var myShows = myShowService.GetMyShowsForUser(userId);

            var myShowTicketStubs = GetAllMyShowTicketStubs().Where(x => myShows.Any(y => y.MyShowId == x.MyShowId)).OrderByDescending(z => z.CreatedDate);

            foreach (var myShowTicketStub in myShowTicketStubs)
            {
                var ticketStub = ticketStubService.GetTicketStub(myShowTicketStub.TicketStubId);
                var photo = photoService.GetPhotoThumbnail(ticketStub.PhotoId.Value);
                if (photo.Thumbnail)
                    return new MyShowThumbnail<IMyShowTicketStub>(myShowTicketStub, photo);
            }

            return null;
        }

        public void SaveCommit(IMyShowTicketStub myShow, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(myShow, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IMyShowTicketStub myShowTicketStub, out bool success)
        {
            Checks.Argument.IsNotNull(myShowTicketStub, "myShow");

            success = false;

            if (null == _repo.FindByMyShowTicketStubId(myShowTicketStub.MyShowTicketStubId))
            {
                try
                {
                    _repo.Add(myShowTicketStub);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IMyShowTicketStub myShowTicketStub)
        {
            Checks.Argument.IsNotNull(myShowTicketStub, "myShowTicketStub");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(myShowTicketStub);
                u.Commit();
            }
        }

        public void Delete(IMyShowTicketStub myShowTicketStub)
        {
            Checks.Argument.IsNotNull(myShowTicketStub, "myShowTicketStub");

            _repo.Remove(myShowTicketStub);
        }
    }
}
