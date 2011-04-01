using System;
using System.Collections.Generic;
using TheCore.Guess;

namespace TheCore.Repository
{
    public interface ITopicRepository
    {
        void Add(ITopic entity);
        ITopic FindByTopicId(Guid id);
        ITopic FindByTopicName(string topicName);
        void Remove(ITopic entity);

        IList<ITopic> FindAll();

        ITopic FindByTourId(Guid tourId);

        ITopic FindByShowId(Guid showId);
    }
}
