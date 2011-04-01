using System.Collections.Generic;
using TheCore.Interfaces;
using TheCore.Guess;

namespace PhishPond.Concrete
{
    public class WholeShowSongList
    {
        public IDictionary<ISong,SongNote> SongList { get; set; }
        public ICollection<ISong> Songs { get { return SongList.Keys; } }

        public WholeShowSongList()
        {
            SongList = new Dictionary<ISong, SongNote>();
        }

        public WholeShowSongList(int count)
        {
            SongList = new Dictionary<ISong, SongNote>(count);
        }

        public bool ContainsSong(ISong song)
        {
            return Songs.Contains(song);
        }

        public ISong ContainsSongReturned(ISong song)
        {
            ISong retSong = null;

            foreach (ISong s in SongList.Keys)
            {
                if (s.SongId == song.SongId)
                {
                    retSong = s;
                }
            }

            return retSong;
        }

        //public static WholeShowSongList GetSongList(ISet set)
        //{
        //    //set.songl
        //}

        public void AddSong(ISong song, SongNote note)
        {
            SongList.Add(new KeyValuePair<ISong, SongNote>(song, note));
        }
    }
}
