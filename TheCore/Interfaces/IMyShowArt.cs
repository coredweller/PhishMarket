using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheCore.Interfaces
{
    public interface IMyShowArt : IEntity
    {
        Guid MyShowArtId { get; set; }
        Guid MyShowId { get; set; }

        Guid ArtId { get; set; }
    }
}
