using System.Collections.Generic;
using TheCore.Interfaces;

namespace TheCore.Guess
{
    public class SetBasedSongList
    {
        public SetBasedSongList()
        {
            Sets = new List<KeyValuePair<ISet, SetName>>();
        }

        public SetBasedSongList(IList<KeyValuePair<ISet, SetName>> sets)
        {
            Sets = sets;
        }

        public IList<KeyValuePair<ISet, SetName>> Sets { get; internal set; }

        public void AddSet(ISet set, SetName name)
        {
            Sets.Add(new KeyValuePair<ISet, SetName>(set, name));
        }
    }
}
