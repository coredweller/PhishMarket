using System.Collections.Generic;
using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public class SetBasedScore
    {
        public double Score
        {
            get
            {
                double score = 0;

                foreach (KeyValuePair<ISet, double> set in Sets)
                {
                    score += set.Value;
                }

                return score;
            }

            internal set { Score = value; }
        }

        public IList<KeyValuePair<ISet, double>> Sets { get; internal set; }

        public SetBasedScore()
        {
            Sets = new List<KeyValuePair<ISet, double>>();
        }

        public void AddSet(ISet set, double score)
        {
            Sets.Add(new KeyValuePair<ISet, double>(set, score));
        }
    }
}
