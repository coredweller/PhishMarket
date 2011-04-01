using System.Collections.Generic;

namespace TheCore.Interfaces
{
    public interface IWholeShowScore
    {
        IList<KeyValuePair<ISong, double>> Correct { get; set; }

        IList<KeyValuePair<ISong, double>> Incorrect { get; set; }

        void AddCorrectSong(ISong song, double score);

        void AddIncorrectSong(ISong song, double score);

        double GetScore();
    }
}
