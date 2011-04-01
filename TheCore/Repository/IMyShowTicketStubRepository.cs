using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IMyShowTicketStubRepository
    {
        void Add(IMyShowTicketStub entity);
        void Remove(IMyShowTicketStub entity);
        IMyShowTicketStub FindByMyShowTicketStubId(Guid myShowTicketStubId);
        IQueryable<IMyShowTicketStub> FindAll();
    }
}
