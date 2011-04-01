using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Infrastructure;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Interfaces;

namespace TheCore.Services
{
    public class GuessWholeShowService
    {

        IGuessWholeShowRepository _repo;

        public GuessWholeShowService(IGuessWholeShowRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IList<IGuessWholeShow> GetAllGuessWholeShows()
        {
            return _repo.FindAll();
        }

        public IGuessWholeShow GetGuessWholeShow(Guid id)
        {
            return _repo.FindByGuessWholeShowId(id);
        }

        public IGuessWholeShow GetGuessWholeShowBySetId(Guid setId)
        {
            return _repo.FindBySetId(setId);
        }

        public IList<IGuessWholeShow> GetGuessWholeShowByTopicId(Guid topicId)
        {
            return _repo.FindByTopicId(topicId);
        }

        public IGuessWholeShow GetOfficialGuessByTopic(Guid topicId)
        {
            return _repo.FindByTopicId(topicId).Where(x => x.Official == true).First();
        }

        public IList<IGuessWholeShow> GetGuessWholeShowByTopicIdAndUserId(Guid topicId, Guid userId)
        {
            return _repo.FindByTopicIdAndUserId(topicId, userId);
        }

        public void SaveCommit(IGuessWholeShow guessWholeShow, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(guessWholeShow, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(IGuessWholeShow guessWholeShow, out bool success)
        {
            Checks.Argument.IsNotNull(guessWholeShow, "guessWholeShow");

            success = false;

            if (null == _repo.FindByGuessWholeShowId(guessWholeShow.GuessWholeShowId))
            {
                try
                {
                    _repo.Add(guessWholeShow);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete( IGuessWholeShow guessWholeShow )
        {
            Checks.Argument.IsNotNull(guessWholeShow, "guessWholeShow");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(guessWholeShow);
                u.Commit();
            }
        }
    }
}
