using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;
using System.Linq;

namespace TheCore.Services
{
    public class SetService
    {
        ISetRepository _repo;

        public SetService(ISetRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<ISet> GetAllSets()
        {
            return _repo.FindAll();
        }

        public ISet GetSet(Guid setId)
        {
            return _repo.FindBySetId(setId);
        }

        public IQueryable<ISet> GetSetsForShow(Guid showId)
        {
            return _repo.FindByShowId(showId);
        }

        public void SaveCommit(ISet set, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(set, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(ISet set, out bool success)
        {
            Checks.Argument.IsNotNull(set, "set");

            success = false;

            if (null == _repo.FindBySetId(set.SetId))
            {
                try
                {
                    _repo.Add(set);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(ISet set)
        {
            Checks.Argument.IsNotNull(set, "set");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(set);
                u.Commit();
            }
        }

        public void Delete(ISet set)
        {
            Checks.Argument.IsNotNull(set, "set");

            _repo.Remove(set);
        }
    }
}
