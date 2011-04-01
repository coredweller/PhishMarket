using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Helpers;
using TheCore.Interfaces;
using PhishPond.Concrete;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class TicketStubRepository : BaseRepository<ITicketStub,TicketStub>, ITicketStubRepository
    {
        LogWriter writer = new LogWriter();
        private IShowRepository _showRepo;

        public TicketStubRepository(IPhishDatabase database, IShowRepository showRepo) : base(database) { _showRepo = showRepo; }
        public TicketStubRepository(IPhishDatabaseFactory factory, IShowRepository showRepo) : base(factory) { _showRepo = showRepo; }

        private IQueryable<ITicketStub> GetAll()
        {
            return Database.PhishTicketStubDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<ITicketStub> FindAll()
        {
            return GetAll().OrderBy(stub => stub.TicketStubId);
        }

        public ITicketStub FindByTicketStubId(Guid id)
        {
            return GetAll().SingleOrDefault(ticketStub => ticketStub.TicketStubId == id);
        }

        public IQueryable<ITicketStub> FindByUserId(Guid userId)
        {
            return GetAll().Where(x => x.UserId == userId);
        }

        public IQueryable<ITicketStub> FindByUserIdAndTourId(Guid userId, Guid tourId)
        {
            var shows = _showRepo.FindByTourId(tourId);

            var showIds = (from s in shows select s.ShowId).ToList();

            return (from ts in FindByUserId(userId)
                   where showIds.Contains(ts.ShowId.Value)
                   select ts);
        }

        public override void Add(ITicketStub entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            if (GetAll().Any(ticketStub => ticketStub.TicketStubId == entity.TicketStubId))
            {
                writer.WriteLine("A Topic with an id={0}".FormatWith(entity.TicketStubId));
                throw new AlreadyExistsException("A Topic with an id={0}".FormatWith(entity.TicketStubId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ITicketStub entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
