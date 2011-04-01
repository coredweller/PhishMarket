using System;

namespace TheCore.Interfaces
{
    public interface IPost : IEntity
    {
        Guid PostId { get; }
        string Title { get; }
        string TitleUrl { get; }
        DateTime PostedDate { get; }
        string PostedBy { get; }
        string Entry { get; }
    }
}
