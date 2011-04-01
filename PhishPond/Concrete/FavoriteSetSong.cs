using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhishPond.Concrete
{
    public class FavoriteSetSong
    {
        public FavoriteVersion Favorite { get; set; }
        public SetSong LiveSong { get; set; }
        public Show LiveShow { get; set; }
    }
}
