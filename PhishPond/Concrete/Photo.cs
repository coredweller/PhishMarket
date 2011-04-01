using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public partial class Photo : IPhoto
    {
        #region IPhoto Members

        public PhotoType PhotoType { get { return (PhotoType)this.Type; } }

        public ISong Song { get; internal set; }

        #endregion
    }
}
