using System;
using TheCore.Guess;

namespace TheCore.Interfaces
{
    public interface IShow : IEntity
    {
        Guid ShowId { get; set; }

        ////Give users the ability to name a show how they want
        string ShowName { get; }
        string VenueName { get; }
        string City { get; }
        string State { get; }
        string Country { get; }
        int? Order { get; }

        //ITour Tour { get; set; }

        decimal? TicketPrice { get; }
        string Notes { get; }

        int? Rank { get; }

        ShowRank ShowRank { get; }

        DateTime? ShowDate { get; }


        bool Official { get; set; }

        Guid? UserId { get; set; }

        Guid? TourId { get; set; }

        string GetShowName();

        string VenueNotes { get; set; }

        string PhishNetUrl { get; set; }

        int? PhishNetShowId { get; set; }

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
