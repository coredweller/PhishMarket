using System;
using System.Linq;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Interfaces;
using TheCore.Infrastructure;
using System.Web.UI.WebControls;

namespace TheCore.Services
{
    public class AlbumService
    {

        IAlbumRepository _repo;

        public AlbumService(IAlbumRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IAlbum> GetAllAlbums()
        {
            return _repo.FindAll();
        }

        public ListItem[] GetAllAlbumsForDropDown()
        {
            var albums = _repo.FindAll().OrderBy(x => x.YearReleased);

            var list = new ListItem[albums.Count()];
            var count = 0;

            foreach (var album in albums)
            {
                list[count] = new ListItem(album.AlbumName, album.AlbumId.ToString());
                count++;
            }

            return list;
        }

        public IAlbum GetAlbum(Guid albumId)
        {
            return _repo.FindByAlbumId(albumId);
        }
        
        public void SaveCommit(IAlbum album, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(album, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IAlbum album, out bool success)
        {
            Checks.Argument.IsNotNull(album, "album");

            success = false;

            if (null == _repo.FindByAlbumId(album.AlbumId))
            {
                try
                {
                    _repo.Add(album);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IAlbum album)
        {
            Checks.Argument.IsNotNull(album, "album");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(album);
                u.Commit();
            }
        }

        public void Delete(IAlbum album)
        {
            Checks.Argument.IsNotNull(album, "album");

            _repo.Remove(album);
        }
    }
}
