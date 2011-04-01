using System;
using TheCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TheCore.Repository
{
    public interface ITicketStubRepository
    {
        void Add(ITicketStub entity);
        ITicketStub FindByTicketStubId(Guid id);
        void Remove(ITicketStub entity);
        IQueryable<ITicketStub> FindAll();
        IQueryable<ITicketStub> FindByUserId(Guid userId);
        IQueryable<ITicketStub> FindByUserIdAndTourId(Guid userId, Guid tourId);
    }
}
