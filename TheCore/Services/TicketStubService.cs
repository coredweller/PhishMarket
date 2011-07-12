using System;
using System.Collections.Generic;
using TheCore.Interfaces;
using TheCore.Infrastructure;
using TheCore.Helpers;
using TheCore.Repository;
using System.Linq;

namespace TheCore.Services
{
    public class TicketStubService
    {
        ITicketStubRepository _repo;

        public TicketStubService(ITicketStubRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<ITicketStub> GetAllTicketStubs()
        {
            return _repo.FindAll();
        }

        public ITicketStub GetTicketStub(Guid id)
        {
            return _repo.FindByTicketStubId(id);
        }

        public IQueryable<ITicketStub> GetTicketStubsByUserId(Guid userId)
        {
            return _repo.FindByUserId(userId);
        }

        public IQueryable<ITicketStub> GetTicketStubsByYear(int year)
        {
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var shows = showService.GetShowsByYear(year);
            var showIds = shows.Select(x => x.ShowId).ToList();

            var showStubs = (from t in GetAllTicketStubs()
                    join s in shows on t.ShowId equals s.ShowId
                    where showIds.Contains(t.ShowId.Value)
                       select new ShowTicketStub(s,t));

            return showStubs.OrderBy(x => x.Show.ShowDate.Value).Select(y => y.TicketStub);
        }

        public IQueryable<ITicketStub> GetByShow(Guid showId)
        {
            return _repo.FindAll().Where(x => x.ShowId == showId).OrderBy(y => y.Original == true);
        }

        public IQueryable<ITicketStub> GetByUserAndTour(Guid userId, Guid tourId)
        {
            return _repo.FindByUserIdAndTourId(userId, tourId);
        }

        public void SaveCommit(ITicketStub ticketStub, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(ticketStub, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(ITicketStub ticketStub, out bool success)
        {
            Checks.Argument.IsNotNull(ticketStub, "ticketStub");

            success = false;

            if (null == _repo.FindByTicketStubId(ticketStub.TicketStubId))
            {
                try
                {
                    _repo.Add(ticketStub);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete(ITicketStub ticketStub)
        {
            Checks.Argument.IsNotNull(ticketStub, "ticketStub");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(ticketStub);
                u.Commit();
            }
        }

    }

    public class ShowTicketStub
    {
        public IShow Show { get; set; }
        public ITicketStub TicketStub { get; set; }

        public ShowTicketStub(IShow show, ITicketStub ticketStub)
        {
            Show = show;
            TicketStub = ticketStub;
        }
    }
}
