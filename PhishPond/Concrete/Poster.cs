using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public partial class Poster : IPoster
    {

        #region IPoster Members

        public PosterStatus PosterStatus { get { return (PosterStatus)this.Status; } }

        #endregion
    }
}
