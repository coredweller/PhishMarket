using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheCore.Interfaces
{
    public interface IMyShowPoster : IEntity
    {
        Guid MyShowPosterId { get; set; }

        Guid MyShowId { get; set; }

        Guid PosterId { get; set; }
    }
}
