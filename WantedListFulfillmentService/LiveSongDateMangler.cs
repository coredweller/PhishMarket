using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhishPond.Concrete;
using TheCore.Interfaces;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;

namespace WantedListFulfillmentService
{
    public class LiveSongDateMangler
    {
        public DateTime ShowDate { get; set; }
        public SetSong LiveSong { get; set; }

        public LiveSongDateMangler()
        {
            ShowDate = DateTime.MinValue;
            LiveSong = null;
        }


        public void Process(IQueryable<IWantedList> activeWantedLists)
        {
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var setService = new SetService(Ioc.GetInstance<ISetRepository>());
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

            var liveSongManglers = (from show in showService.GetAllShows().OrderByDescending(x => x.ShowDate).Take(10)
                          from set in setService.GetSetsForShow(show.ShowId)
                          from setsong in setSongService.GetSetSongsBySet(set.SetId)
                          select new LiveSongDateMangler { LiveSong = (SetSong)setsong, ShowDate = show.ShowDate.Value });

            foreach (var wanted in activeWantedLists)
            {

                //bust it raw dawg
                ///LEFT OFF HERE
            }
        }
    }
}
