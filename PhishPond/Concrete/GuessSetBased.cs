using System;
using System.Collections.Generic;
using TheCore.Interfaces;
using TheCore.Guess;

namespace PhishPond.Concrete
{
    public class GuessSetBased
    {
        public DateTime CreatedDate { get; internal set; }

        public SetBasedSongList SongList { get; internal set; }

        public ITopic Topic { get; internal set; }

        private const double RightSongRightSpot = 2;
        private const double RightSongSameSet = 1;
        private const double IntoCorrect = .5;

        private const double WrongSong = -.5;
        private const double IntoWrong = -.25;

        public GuessSetBased(ITopic topic)
        {
            Topic = topic;
            CreatedDate = DateTime.Now;
            SongList = new SetBasedSongList();
        }

        public SetBasedScore GetScore(GuessSetBased master)
        {
            SetBasedScore score = new SetBasedScore();

            int setIndex = 0;
            int songIndex = 0;

            ISet retSet;
            double scoreCount = 0;
            
            foreach (var set in master.SongList.Sets)
            {
                retSet = new Set();
                scoreCount = 0;

                setIndex = master.SongList.Sets.IndexOf(set);
                
                foreach (var song in set.Key.SongList)
                {
                   songIndex = set.Key.SongList.IndexOf(song);

                   var songPair = new KeyValuePair<ISong, SongNote>(song.Key, song.Value);

                   retSet.SongList.Add(songPair);

                   if (SongList.Sets[setIndex].Key.SongList[songIndex].Key.SongIsEqual(song.Key))
                   {
                       scoreCount += RightSongRightSpot;

                       var songNote = SongList.Sets[setIndex].Key.SongList[songIndex].Value;

                       if (songNote == SongNote.Into && songNote == song.Value)
                       {
                           scoreCount += IntoCorrect;
                       }
                       else if(songNote == SongNote.Into && songNote != song.Value)
                       {
                           scoreCount -= IntoWrong;
                       }
                   }
                   else if (SongList.Sets[setIndex].Key.ContainsSong(song.Key))
                   {
                       scoreCount += RightSongSameSet;
                   }
                   else
                   {
                       scoreCount -= WrongSong;
                   }
                }

                score.AddSet(retSet, scoreCount);
            }

            return score;
        }
    }
}