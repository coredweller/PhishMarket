using System;

namespace TheCore.Interfaces
{
    public interface IGuessWholeShow : IEntity
    {
        Guid GuessWholeShowId { get; set; }
        Guid TopicId { get; set; }
        Guid SetId { get; set; }
        Guid UserId { get; set; }

        bool Official { get; set; }

        IWholeShowScore GetScore(IGuessWholeShow master);
    }
}
