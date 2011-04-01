using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public partial class Video : IVideo
    {
        #region IVideo Members

        public VideoType VideoType { get { return (VideoType)this.Type; } }

        #endregion
    }
}
