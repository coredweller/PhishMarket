using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class SongService
    {
        ISongRepository _repo;

        public SongService(ISongRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public List<string> GetAlbumsList()
        {
            List<string> albums = new List<string>();

            var albumList = _repo.FindAll().DistinctBy(x => x.Album).ToList();

            albumList.ForEach(x => albums.Add(x.Album));

            albums.Sort();

            return albums;
        }

        public System.Web.UI.WebControls.ListItem[] GetAlbums()
        {
            var albumList = _repo.FindAll().DistinctBy(x => x.Album).OrderBy(x => x.Album).ToList();

            var albums = new System.Web.UI.WebControls.ListItem[albumList.Count()];

            albums = (from album in albumList select new System.Web.UI.WebControls.ListItem(album.Album, album.Album)).ToArray();

            return albums;
        }

        public IQueryable<ISong> GetSongsByAlbum(string album)
        {
            return _repo.FindAll().Where(x => x.Album == album).OrderBy(x => x.Order);
        }

        public IQueryable<ISong> GetSongsByFirstLetter(string letter)
        {
            return _repo.FindAll().Where(x => x.SongName.StartsWith(letter)).OrderBy(x => x.SongName);
        }

        public IQueryable<ISong> GetAllSongs()
        {
            return _repo.FindAll();
        }

        public ISong GetSong(Guid id)
        {
            return _repo.FindBySongId(id);
        }

        public ISong GetSong(string name)
        {
            return _repo.FindBySongName(name);
        }

        public void SaveCommit(ISong song, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(song, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(ISong song, out bool success)
        {
            Checks.Argument.IsNotNull(song, "song");

            success = false;

            if (null == _repo.FindBySongId(song.SongId))
            {
                try
                {
                    _repo.Add(song);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void DeleteCommit(ISong song)
        {
            Checks.Argument.IsNotNull(song, "song");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(song);
                u.Commit();
            }
        }

        public void Delete(ISong song)
        {
            Checks.Argument.IsNotNull(song, "song");

            _repo.Remove(song);
        }
    }
}
