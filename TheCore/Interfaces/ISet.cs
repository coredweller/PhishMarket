using System;
using System.Collections.Generic;
using TheCore.Guess;

namespace TheCore.Interfaces
{
    public interface ISet : IEntity
    {
        Guid SetId { get; set; }

        short? SetNumber { get;  }

        bool Encore { get;  }

        string Notes { get;  }

        IList<KeyValuePair<ISong, SongNote>> SongList { get; }

        bool ContainsSong(ISong song);

        bool Official { get; set; }


        Guid? ShowId { get; set; }


        //bool AddSong(ISong song);
    }
}
