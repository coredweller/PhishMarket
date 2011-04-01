using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhishPond.Repository.LinqToSql;
using TheCore.Interfaces;
using PhishPond.Concrete;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class MyShowTicketStubRepository : BaseRepository<IMyShowTicketStub, MyShowTicketStub>, IMyShowTicketStubRepository
    {
        LogWriter writer = new LogWriter();
        public MyShowTicketStubRepository(IPhishDatabase database) : base(database) { }

        public MyShowTicketStubRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IMyShowTicketStub> GetAll()
        {
            return Database.MyShowTicketStubDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IMyShowTicketStub> FindAll()
        {
            return GetAll().OrderBy(s => s.MyShowId);
        }

        public IMyShowTicketStub FindByMyShowTicketStubId(Guid myShowTicketStubId)
        {
            return GetAll().SingleOrDefault(myShow => myShow.MyShowTicketStubId == myShowTicketStubId);
        }

        public override void Add(IMyShowTicketStub entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(myShow => myShow.MyShowTicketStubId == entity.MyShowTicketStubId))
            {
                writer.WriteLine("A MyShowTicketStub with an id={0}".FormatWith(entity.MyShowTicketStubId));
                throw new AlreadyExistsException("A MyShowTicketStub with an id={0}".FormatWith(entity.MyShowTicketStubId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IMyShowTicketStub entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
