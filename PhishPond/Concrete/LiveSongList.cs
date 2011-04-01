using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhishPond.Concrete
{
    public class LiveSongInfo
    {
        public string SongName { get; set; }
        public string FavoriteShowInfo { get; set; }

        public double HighestRating { get; set; }
        public string HighestRatedShowInfo { get; set; }

        public LiveSongInfo(string songName, double highestRating)
        {
            SongName = songName;
            HighestRating = highestRating;
        }
    }
}
