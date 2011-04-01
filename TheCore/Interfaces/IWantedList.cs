using System;

namespace TheCore.Interfaces
{
    public interface IWantedList : IEntity
    {
        Guid WantedId { get; set; }
        DateTime? FulfilledDate { get; set; }
        bool Archive { get; set; }
        Guid UserId { get; set; }
        Guid SongId { get; set; }
        Guid? SetSongId { get; set; }
        int Rank { get; set; }
    }
}
