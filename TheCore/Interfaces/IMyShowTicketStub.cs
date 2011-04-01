using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheCore.Interfaces
{
    public interface IMyShowTicketStub : IEntity
    {
        Guid MyShowTicketStubId { get; set; }
        Guid MyShowId { get; set; }
        Guid TicketStubId { get; set; }
    }
}
