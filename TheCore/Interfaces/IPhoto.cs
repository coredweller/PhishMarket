using System;

namespace TheCore.Interfaces
{
    public interface IPhoto : IEntity
    {
        Guid PhotoId { get; }

        string Source { get;  }

        short? Quality { get;  }

        bool Thumbnail { get; set; }


        string Notes { get;  }

        string FileLocation { get;  }

        string FileName { get;  }

        string NickName { get;  }

        short Type { get;  }

        byte[] Image { get; set; }

        string ContentType { get; set; }

        int ContentLength { get; set; }

        Guid UserId { get; set; }

        //If its taken during a song, but make it optional in case it is not during a song.
        Guid? SongId { get; set; }
        /* Long run I want to concatenate pictures into a collage and play the show along with them
         so people can see the show in pictures as the music is played to give them a feeling of how it
         was to be there.  Also, the more input the closer the feeling can be. */

        Guid? ShowId { get; set; }
    }

    public enum PhotoType
    {
        NotSet = 0,
        Picture = 1,
        Poster = 2,
        TicketStub = 3,
        Art = 4,

        Other = 5
    }

    public enum PhotoQuality
    {
        NotSet = 0,
        Perfect = 1,
        Great = 2,
        Good = 3,
        Average = 4,
        Bad = 5,
        Terrible = 6
    }
}
