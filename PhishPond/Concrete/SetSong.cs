using System.Collections.Generic;
using TheCore.Interfaces;
using System.Text;
using System;

namespace PhishPond.Concrete
{
    public partial class SetSong : ISetSong
    {

        public static bool ContainsSong(IList<SetSong> songList, ISong song)
        {
            foreach (var s in songList)
            {
                if (s.SongId == song.SongId)
                {
                    return true;
                }
            }

            return false;
        }

        public static SetSong FromSong(Song song)
        {
            return new SetSong
            {
                Abbreviation = song.Abbreviation,
                Album = song.Album,
                Cover = song.Cover,
                Order = song.Order,
                SongName = song.SongName,
                SongId = song.SongId
            };
        }

        public string GetSongName(double? length, DateTime? showDate, string city, string state)
        {
            if (length == null && showDate == null && city == null && state == null)
                return string.Empty;

            StringBuilder s = new StringBuilder();
            string showDateStr = showDate.Value.ToString("MM/dd/yyyy");

            if (length != null)
                s.Append(length.Value);

            if (showDate != null)
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(showDateStr);
            }

            if (!string.IsNullOrEmpty(city))
            {
                if (s.Length > 0)
                    s.Append(" - ");

                s.Append(city);

                if (!string.IsNullOrEmpty(state))
                {
                    s.Append(", " + state);
                }
            }

            return s.ToString();
        }
    }
}
