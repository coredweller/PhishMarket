using System;
using System.Linq;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class WantedListService
    {
        IWantedListRepository _repo;

        public WantedListService(IWantedListRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IWantedList> GetAllWantedLists()
        {
            return _repo.FindAll();
        }

        public IQueryable<IWantedList> GetAllActiveWantedLists()
        {
            return GetAllWantedLists().Where(x => x.Archive != true && x.SetSongId == null);
        }

        public IQueryable<IWantedList> GetByUserId(Guid userId)
        {
            return GetAllWantedLists().Where(x => x.UserId == userId && x.Archive == false).OrderBy(x => x.Rank);
        }

        public IQueryable<IWantedList> GetArchivedByUserId(Guid userId)
        {
            return GetAllWantedLists().Where(x => x.UserId == userId && x.Archive == true).OrderByDescending(x => x.FulfilledDate);
        }

        public bool SongAlreadyExistsForUser(Guid userId, Guid songId)
        {
            return GetAllWantedLists().Where(x => x.UserId == userId && x.SongId == songId && x.Archive == false).ToList().Count() > 0;
        }

        public void SaveCommit(IWantedList wantedList, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(wantedList, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IWantedList wantedList, out bool success)
        {
            Checks.Argument.IsNotNull(wantedList, "myShow");

            success = false;

            if (null == _repo.FindById(wantedList.WantedId).SingleOrDefault())
            {
                try
                {
                    _repo.Add(wantedList);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IWantedList wantedList)
        {
            Checks.Argument.IsNotNull(wantedList, "wantedList");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(wantedList);
                u.Commit();
            }
        }

        public void Delete(IWantedList wantedList)
        {
            Checks.Argument.IsNotNull(wantedList, "wantedList");

            _repo.Remove(wantedList);
        }
    }
}
