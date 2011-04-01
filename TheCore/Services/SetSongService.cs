using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;
using System.Linq;

namespace TheCore.Services
{
    public class SetSongService
    {
        ISetSongRepository _repo;

        public SetSongService(ISetSongRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<ISetSong> GetAllSetSongs()
        {
            return _repo.FindAll();
        }

        public ISetSong GetSetSong(Guid setSongId)
        {
            return _repo.FindBySetSongId(setSongId);
        }

        public IQueryable<ISetSong> GetSetSongsBySet(Guid setId)
        {
            return _repo.FindAll().Where(x => x.SetId == setId);
        }

        public IQueryable<ISetSong> GetSetSongBySong(Guid songId)
        {
            return _repo.FindAll().Where(x => x.SongId == songId);
        }

        public ISetSong GetSetSongBySongAndSet(Guid songId, Guid setId)
        {
            return _repo.FindBySongIdAndSetId(songId, setId);
        }

        public IList<ISetSong> GetSetSongsBySongIds(IList<ISong> songs)
        {
            var setSongs = new List<ISetSong>();

            _repo.FindAll().ToList().ForEach(x => { setSongs.AddRange(_repo.FindAll().Where(s => s.SongId == x.SongId)); });

            return setSongs;
        }

        public IQueryable<ISetSong> GetAllVersions(Guid songId)
        {
            return _repo.FindAllSetSongsBySongId(songId);
        }

        public void SaveCommit(ISetSong song, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(song, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(ISetSong song, out bool success)
        {
            Checks.Argument.IsNotNull(song, "song");

            success = false;

            if (null == _repo.FindBySetSongId(song.SetSongId))
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
        public void DeleteCommit(ISetSong song)
        {
            Checks.Argument.IsNotNull(song, "song");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(song);
                u.Commit();
            }
        }

        public void Delete(ISetSong song)
        {
            Checks.Argument.IsNotNull(song, "song");

            _repo.Remove(song);
        }
    }
}
