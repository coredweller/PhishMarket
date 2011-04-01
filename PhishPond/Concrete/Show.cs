using TheCore.Interfaces;
using TheCore.Guess;

namespace PhishPond.Concrete
{
    public partial class Show : IShow
    {
        #region IShow Members

        public ShowRank ShowRank { get { return (ShowRank)this.Rank; } }
        
        #endregion

        public string GetShowName()
        {
            return string.IsNullOrEmpty(ShowName) ?
                                    string.Format("{0} - {1} - {2}, {3}", ShowDate.Value.ToString("MM/dd/yyyy"), VenueName, City, State) :
                                        string.Format("{0} - {1} - {2}, {3}", ShowName, ShowDate.Value.ToString("MM/dd/yyyy"), VenueName, State);
        }

    }
}
