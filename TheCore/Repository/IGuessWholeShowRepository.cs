using System;
using System.Collections.Generic;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IGuessWholeShowRepository : IRepository<IGuessWholeShow>
    {
        IList<IGuessWholeShow> FindAll();

        IGuessWholeShow FindByGuessWholeShowId(Guid id);

        IGuessWholeShow FindBySetId(Guid setId);

        IList<IGuessWholeShow> FindByTopicId(Guid topicId);

        IList<IGuessWholeShow> FindByTopicIdAndUserId(Guid topicId, Guid userId);
    }
}
