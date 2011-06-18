using System;
using TheCore.Guess;

namespace TheCore.Interfaces
{
    public interface IShow : IEntity
    {
        Guid ShowId { get; set; }
        string ShowName { get; }
        string VenueName { get; }
        string City { get; }
        string State { get; }
        string Country { get; }
        int? Order { get; }
        decimal? TicketPrice { get; }
        string Notes { get; }
        int? Rank { get; }
        ShowRank ShowRank { get; }
        DateTime? ShowDate { get; }
        bool Official { get; set; }
        Guid? UserId { get; set; }
        Guid? TourId { get; set; }
        string VenueNotes { get; set; }

        string GetShowName();
    }

    //Use this to represent the star system.
    public enum ShowRank
    {
        None = 0,
        Excellent = 1,
        Great = 2,
        Good = 3,
        Average = 4,
        BelowAverage = 5,
    }
}
