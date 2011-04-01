using System.Collections.Generic;
using System.Linq;
using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public partial class Tour : ITour
    {
        #region ITour Members

        IList<IShow> ITour.Shows
        {
            get { return this.Shows.Cast<IShow>().OrderBy(show => show.Order).ToList(); }
        }
        
        #endregion

    }
}
