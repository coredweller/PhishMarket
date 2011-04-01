using System.Collections.Generic;
using TheCore.Interfaces;
using TheCore.Guess;

namespace PhishPond.Concrete
{
    public partial class Set : ISet
    {
        #region ISet Members

        //NEED TO FIX THIS
        public IList<KeyValuePair<ISong, SongNote>> SongList { get; set; }
        
        //{ get { return Songs.Cast<KeyValuePair<ISong, SongNote>>().OrderBy(x => x.Key.Order).ToList(); } }

        #endregion

        //public bool AddSong(ISong song)
        //{
        //    try
        //    {
        //        SetSongs.Add((Song)song);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public IList<ISong> GetSongs()
        {
            IList<ISong> songs = new List<ISong>();

            foreach (var song in SongList)
            {
                songs.Add(song.Key);
            }

            return songs;
        }

        public bool ContainsSong(ISong song)
        {
            foreach (var s in SongList)
            {
                if (song.SongIsEqual(s.Key))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
