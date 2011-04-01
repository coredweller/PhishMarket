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

        string Notes { get; set; }

        DateTime? NotesUpdatedDate { get; set; }
    }
}
