using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Interfaces;

namespace TheCore.Services
{
    public class MyShowThumbnail<T>
    {
        public T MyShowItem { get; set; }
        public IPhoto Thumbnail { get; set; }

        public MyShowThumbnail(T myShowItem, IPhoto thumbnail)
        {
            MyShowItem = myShowItem;
            Thumbnail = thumbnail;
        }
    }
}
