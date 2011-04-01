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
    public class FavoriteVersionSongList
    {
        public List<FavoriteSetSong> SongList { get; set; }

        

        public FavoriteVersionSongList()
        {
            SongList = new List<FavoriteSetSong>();
        }

        public void AddFavoriteSongPair(FavoriteVersion fave, SetSong setSong, Show show)
        {
            SongList.Add(new FavoriteSetSong { Favorite = fave,  LiveSong = setSong, LiveShow = show });
        }

        public static List<FavoriteSetSong> GenerateFavoriteVersionListByAlbum(string album)
        {
            var songService = new SongService(Ioc.GetInstance<ISongRepository>());
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
            var favoriteVersionService = new FavoriteVersionService(Ioc.GetInstance<IFavoriteVersionRepository>());
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var setService = new SetService(Ioc.GetInstance<ISetRepository>());

            FavoriteVersionSongList songList = new FavoriteVersionSongList();

            foreach (var song in songService.GetSongsByAlbum(album))
            {
                
                var versions = favoriteVersionService.GetAllFavoriteVersions().Where(s => s.SongId == song.SongId).GroupBy(g => g.SetSongId).ToList();

                if (versions == null || versions.Count() <= 0)
                {
                    songList.AddFavoriteSongPair(null, SetSong.FromSong((Song)song), null);
                    continue;
                }

                if (versions.Count() == 1)
                {
                    var version = versions[0].First();

                    var setSong = setSongService.GetSetSong(version.SetSongId.Value);
                    var set = setService.GetSet(setSong.SetId.Value);
                    var show = showService.GetShow(set.ShowId.Value);

                    songList.AddFavoriteSongPair((FavoriteVersion)version, (SetSong)setSong, (Show)show);
                }
                else
                {
                    int count = 0;

                    Guid? setSongId = null;
                    FavoriteVersion fave = null;
                    SetSong setSong = null;
                    IShow show = null;

                    foreach (var version in versions)
                    {
                        if (version.Count() > count)
                        {
                            fave = (FavoriteVersion)version.First();
                            setSongId = version.First().SetSongId;
                        }
                    }

                    if(setSongId != null)
                    {
                        setSong = (SetSong)setSongService.GetSetSong(setSongId.Value);
                        var set = setService.GetSet(setSong.SetId.Value);
                        show = showService.GetShow(set.ShowId.Value);
                        
                    }

                    songList.AddFavoriteSongPair(fave, setSong ?? SetSong.FromSong((Song)song) , (Show)show);
                }
            }

            return songList.SongList;
        }
    }
}
