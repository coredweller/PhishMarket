using System.Collections.Generic;
using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public class WholeShowScore : IWholeShowScore
    {
        public IList<KeyValuePair<ISong, double>> Correct { get; set; }

        public IList<KeyValuePair<ISong, double>> Incorrect { get; set; }

        public WholeShowScore()
        {
            Correct = new List<KeyValuePair<ISong, double>>();
            Incorrect = new List<KeyValuePair<ISong, double>>();
        }

        public void AddCorrectSong(ISong song, double score)
        {
            Correct.Add(new KeyValuePair<ISong, double>(song, score));
        }

        public void AddIncorrectSong(ISong song, double score)
        {
            Incorrect.Add(new KeyValuePair<ISong, double>(song, score));
        }

        public double GetScore()
        {
            double score = 0;

            foreach (var s in this.Correct)
            {
                score += s.Value;
            }

            foreach (var s in this.Incorrect)
            {
                score += s.Value;
            }

            return score;
        }
    }
}
