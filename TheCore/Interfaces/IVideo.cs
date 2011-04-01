using System;

namespace TheCore.Interfaces
{
    public interface IVideo : IEntity
    {
        Guid VideoId { get; }

        string Source { get; }
        string Quality { get; }

        double Length { get; }

        //To keep track of what type of video it is
        short Type { get; }

        /* Long run I want to concatenate videos into a streaming collection and play it as an entire show
         * so people can see the show in a video to give them a feeling of how it was to be there.  
         * Also, the more input the closer the more full the experience can be. */

        string Notes { get; }

        string FileLocation { get; }

        string FileName { get; }

        //Way to identify the video
        string Name { get; }

    }

    public enum VideoType
    {
        NotSet = 0,

        SingleSong = 1,
        MultipleSongs = 2,

        Crowd = 3,
        Other = 4
    }
}
