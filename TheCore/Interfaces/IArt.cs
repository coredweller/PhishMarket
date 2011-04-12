using System;

namespace TheCore.Interfaces
{
    public interface IArt : IEntity
    {
        Guid ArtId { get; set; }
        string Creator { get; set; }
        string Notes { get; set; }
        Guid? ShowId { get; set; }
        Guid? UserId { get; set; }
        Guid PhotoId { get; set; }
    }
}
