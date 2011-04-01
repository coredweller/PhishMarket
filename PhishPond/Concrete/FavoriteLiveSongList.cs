using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public class FavoriteLiveSongList
    {
        public SetSong LiveSong { get; set; }
        public List<Show> FavoriteLiveShows { get; set; }

        public double HighestRating { get; set; }
        public List<Show> HighestRatedShows { get; set; }

        private SetSongService setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
        private SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());
        private ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());

        public FavoriteLiveSongList()
        {
            LiveSong = new SetSong();
            FavoriteLiveShows = new List<Show>();

            HighestRating = 0;
            HighestRatedShows = new List<Show>();
        }

        public FavoriteLiveSongList(SetSong setSong, Show show)
        {
            LiveSong = new SetSong();
            FavoriteLiveShows = new List<Show>();
            HighestRatedShows = new List<Show>();

            FavoriteLiveShows.Add(show);
            LiveSong = setSong;
        }

        public FavoriteLiveSongList(SetSong setSong)
        {
            LiveSong = new SetSong();
            FavoriteLiveShows = new List<Show>();
            HighestRatedShows = new List<Show>();

            LiveSong = setSong;
        }

        public FavoriteLiveSongList(SetSong setSong, int highestRank, List<Show> highestRankedShows)
        {
            LiveSong = setSong;
            HighestRating = highestRank;
            HighestRatedShows = highestRankedShows;

            FavoriteLiveShows = new List<Show>();
        }

        public FavoriteLiveSongList(SetSong setSong, List<Show> shows)
        {
            LiveSong = setSong;
            FavoriteLiveShows = shows;
            HighestRatedShows = new List<Show>();
        }

        public void AddFavorite(SetSong song, Show show)
        {
            LiveSong = song;
            FavoriteLiveShows.Add(show);
        }

        public void ClearShows()
        {
            FavoriteLiveShows = new List<Show>();
        }

        public static List<LiveSongInfo> GetRepeaterUsableList(List<FavoriteLiveSongList> songList)
        {
            var safeList = new List<LiveSongInfo>();

            foreach (var song in songList)
            {
                var liveSongInfo = new LiveSongInfo(song.LiveSong.SongName, song.HighestRating);

                if (song.FavoriteLiveShows.Count > 0)
                {
                    foreach (var show in song.FavoriteLiveShows)
                    {
                        liveSongInfo.FavoriteShowInfo += song.LiveSong.GetSongName(song.LiveSong.Length, show.ShowDate, show.City, show.State) + " ";
                    }
                }

                if (song.HighestRatedShows != null && song.HighestRatedShows.Count > 0)
                {
                    foreach (var show in song.HighestRatedShows)
                    {
                        liveSongInfo.HighestRatedShowInfo += song.LiveSong.GetSongName(song.LiveSong.Length, show.ShowDate, show.City, show.State) + " ";
                    }
                }

                safeList.Add(liveSongInfo);
            }

            return safeList;
        }

        public List<FavoriteLiveSongList> GenerateFavoriteLiveSongList(IQueryable<ISong> songs)
        {
            var songService = new SongService(Ioc.GetInstance<ISongRepository>());
            var favoriteVersionService = new FavoriteVersionService(Ioc.GetInstance<IFavoriteVersionRepository>());

            List<FavoriteLiveSongList> songLists = new List<FavoriteLiveSongList>();

            foreach (var song in songs)
            {
                var versions = favoriteVersionService.GetAllFavoriteVersions().Where(s => s.SongId == song.SongId).GroupBy(g => g.SetSongId).ToList();

                //If there aren't any favorites chosen for that song then just add the song to be displayed and continue
                if (versions == null || versions.Count() <= 0)
                {
                    var fave = GetAnalysisPart((Song)song);
                    if (fave != null)
                    {
                        songLists.Add(fave);
                    }
                    else
                    {
                        songLists.Add(new FavoriteLiveSongList(SetSong.FromSong((Song)song)));
                    }

                    continue;
                }

                //If there is only 1 favorite version then just use the first one in the collection
                if (versions.Count() == 1)
                {
                    var version = versions[0].First();
                    var setSongId = version.SetSongId.Value;
                    var setSong = (SetSong)setSongService.GetSetSong(setSongId);
                    var show = GetShowFromSetSong(setSongId);

                    var fave = GetAnalysisPart(setSong);
                    if (fave != null)
                    {
                        fave.FavoriteLiveShows.Add(show);
                    }
                    else
                    {
                        fave = new FavoriteLiveSongList(setSong, show);
                    }

                    songLists.Add(fave);
                }
                //There is a lot to check
                else
                {
                    int count = 0;

                    Guid? setSongId = null;

                    FavoriteLiveSongList songList = new FavoriteLiveSongList();

                    foreach (var version in versions)
                    {
                        //If this version has more votes then it needs to be added
                        if (version.Count() >= count)
                        {
                            if (version.Count() > count && count > 0)
                            {
                                //If its not the first time in the loop and this version is the most voted on then clear whatever is in there
                                songList.ClearShows();
                            }

                            //Change the count so that next time it will be right
                            count = version.Count();

                            setSongId = version.First().SetSongId;
                            var setSong = (SetSong)setSongService.GetSetSong(setSongId.Value);
                            var show = GetShowFromSetSong(setSongId.Value);
                            songList.AddFavorite(setSong, show);
                        }
                    }

                    var fave = GetAnalysisPart(setSongId);

                    if (fave != null)
                    {
                        songList.HighestRating = fave.HighestRating;
                        songList.HighestRatedShows = fave.HighestRatedShows;
                    }

                    songLists.Add(songList);
                }
            }

            return songLists;
        }

        private FavoriteLiveSongList GetAnalysisPart(Guid? setSongId)
        {
            if (!setSongId.HasValue)
                return null;

            return GetAnalysisPart((SetSong)setSongService.GetSetSong(setSongId.Value));
        }

        private FavoriteLiveSongList GetAnalysisPart(SetSong setSong)
        {
            var songService = new SongService(Ioc.GetInstance<ISongRepository>());

            return GetAnalysisPart((Song)songService.GetSong(setSong.SongId.Value));
        }

        private FavoriteLiveSongList GetAnalysisPart(Song song)
        {
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
            var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());
            var songService = new SongService(Ioc.GetInstance<ISongRepository>());

            //Get all Analysis for that Song but in groups of SetSong
            var analysis = analysisService.GetAnalysisBySong(song.SongId).GroupBy(x => x.SetSongId);

            double highestRating = 0;
            double? rating = 0;
            List<Guid> setSongIds = new List<Guid>();

            //If there are no analysis then there is nothing to see here
            if (analysis.Count() == 0)
                return null;

            var fave = new FavoriteLiveSongList(SetSong.FromSong(song));

            //If there are 1 or more analysis then we need to find out which is the highest ranked
            foreach (var a in analysis)
            {
                rating = a.Average(x => x.Rating);
                var setSongId = a.First().SetSongId;

                if (rating.HasValue && rating.Value > highestRating)
                {
                    highestRating = rating.Value;
                    fave.HighestRatedShows = new List<Show>();

                    var setSong = (SetSong)setSongService.GetSetSong(setSongId);
                    var show = GetShowFromSetSong(setSongId);
                    fave.HighestRatedShows.Add(show);
                }
                else if (rating.HasValue && rating.Value == highestRating)
                {
                    var setSong = (SetSong)setSongService.GetSetSong(setSongId);
                    var show = GetShowFromSetSong(setSongId);

                    fave.HighestRatedShows.Add(show);
                }
            }

            fave.HighestRating = highestRating;

            return fave;
        }

        private Show GetShowFromSetSong(Guid setSongId)
        {
            var setSong = (SetSong)setSongService.GetSetSong(setSongId);
            var set = setService.GetSet(setSong.SetId.Value);
            var show = showService.GetShow(set.ShowId.Value);

            return (Show)show;
        }
    }
}