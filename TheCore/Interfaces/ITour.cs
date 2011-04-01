using System;
using System.Collections.Generic;

namespace TheCore.Interfaces
{
    public interface ITour : IEntity
    {
        Guid TourId { get; set; }

        string TourName { get; }

        IList<IShow> Shows { get; }

        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }

        

        bool Official { get; set; }

        
    }

}
