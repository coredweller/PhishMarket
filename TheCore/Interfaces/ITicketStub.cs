using System;

namespace TheCore.Interfaces
{
    public interface ITicketStub : IEntity
    {
        Guid TicketStubId { get; set; }
        string Notes { get; set; }
        Guid? PhotoId { get; set; }
        Guid? ShowId { get; set; }

        //If this is a TM/LN ticket or a PBTM tix
        bool Original { get; set; }
        Guid? UserId { get; set; }
    }
}
