using System.Linq;
using TheCore.Interfaces;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace PhishPond.Concrete
{
    public partial class GuessWholeShow : IGuessWholeShow
    {
        public WholeShowSongList SongList { get; internal set; }

        //public GuessWholeShow(ITopic topic)
        //{
        //    Topic = topic;
        //    SongList = new WholeShowSongList();
        //    CreatedDate = DateTime.Now;
        //}

        //public GuessWholeShow(ITopic topic, int songTotal)
        //{
        //    Topic = topic;
        //    SongList = new WholeShowSongList(songTotal);
        //    CreatedDate = DateTime.Now;
        //}

        public bool Contains(ISong song)
        {
            if (SongList.ContainsSong(song))
            {
                return true;
            }

            return false;
        }

        public IWholeShowScore GetScore(IGuessWholeShow master)
        {
            double correct = 100;
            double incorrect = -100;
            double correctSpot = 500;

            IWholeShowScore score = new WholeShowScore();

            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            var masterSet = (Set)setService.GetSet(master.SetId);

            foreach (var setSong in this.Set.SetSongs.OrderBy(x => x.Order))
            {
                int count = masterSet.SetSongs.Where(x => x.SongId == setSong.SongId).Count();

                //If they are just straight up wrong then add it to here
                if (count == 0)
                    score.AddIncorrectSong(setSong.Song, incorrect);
                else
                {
                    int masterCount = masterSet.SetSongs.Where(x => x.SongId == setSong.SongId).Count();
                    int correctCount = score.Correct.Where(x => x.Key.SongId == setSong.SongId).Count();

                    if (masterCount > correctCount)
                    {
                        if (masterSet.SetSongs.Count >= setSong.Order)
                        {
                            if (setSong.Order == masterSet.SetSongs.Where(x => x.Order == setSong.Order).Single().Order)
                            {
                                //GIVE LOTS OF EXTRA POINTS CUZ THEY GOT IT IN THE SAME SLOT
                                score.AddCorrectSong(setSong.Song, correctSpot);
                            }
                            else
                            {
                                //GiVE NORMAL AMT OF POINTS CUZ THEY GOT WRONG SLOT
                                score.AddCorrectSong(setSong.Song, correct);
                            }
                        }
                        else
                        {
                            //GiVE NORMAL AMT OF POINTS CUZ THEY GOT WRONG SLOT
                            score.AddCorrectSong(setSong.Song, correct);
                        }
                    }
                    else
                    {
                        //If they guessed the song more times than is in the master setlist then they got it wrong
                        score.AddIncorrectSong(setSong.Song, incorrect);
                    }
                }
            }

            return score;
        }
    }
}
