using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Infrastructure;
using TheCore.Guess;

namespace TheCore.Services
{
    public class TopicService
    {
        ITopicRepository _repo;

        public TopicService(ITopicRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IList<ITopic> GetAllTopics()
        {
            return _repo.FindAll();
        }

        public ITopic GetTopic(Guid id)
        {
            return _repo.FindByTopicId(id);
        }

        public ITopic GetTopicByShow(Guid showId)
        {
            return _repo.FindByShowId(showId);
        }

        public ITopic GetTopicByTour(Guid tourId)
        {
            return _repo.FindByTopicId(tourId);
        }

        public ITopic GetTopic(string name)
        {
            return _repo.FindByTopicName(name);
        }

        public void SaveCommit(ITopic topic, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(topic, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(ITopic topic, out bool success)
        {
            Checks.Argument.IsNotNull(topic, "topic");

            success = false;

            if (null == _repo.FindByTopicId(topic.TopicId))
            {
                try
                {
                    _repo.Add(topic);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete(ITopic topic)
        {
            Checks.Argument.IsNotNull(topic, "topic");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(topic);
                u.Commit();
            }
        }
    }
}
