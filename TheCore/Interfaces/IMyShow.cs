using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheCore.Interfaces
{
    public interface IMyShow : IEntity
    {
        Guid MyShowId { get; set; }

        Guid ShowId { get; set; }

        Guid UserId { get; set; }

        int? Rating { get; set; }
        int? EnergyRating { get; set; }
        int? FlowRating { get; set; }
        int? SegueRating { get; set; }
        int? Type1JamRating { get; set; }
        int? Type2JamRating { get; set; }
        int? BustoutRating { get; set; }

        string Notes { get; set; }

        DateTime? NotesUpdatedDate { get; set; }
    }
}
