using System;

namespace TheCore.Guess
{
    public interface ITopic
    {
        Guid TopicId { get; }
        string TopicName { get; }
        short Type { get; }
        DateTime CreatedDate { get; set; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        Guid? ShowId { get; set; }
        Guid? TourId { get; set; }
        bool Deleted { get; set; }
    }

    public enum TopicType
    {
        None = 0,
        Tour = 1,
        Show = 2
    }
}
