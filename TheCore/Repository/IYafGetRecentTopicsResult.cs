using System;
namespace TheCore.Repository
{
    public interface IYafGetRecentTopicsResult
    {
        int Flags { get; set; }
        int ForumID { get; set; }
        bool? IsDeleted { get; set; }
        int? LastMessageID { get; set; }
        DateTime? LastPosted { get; set; }
        int? LastUserID { get; set; }
        string LastUserName { get; set; }
        int NumPosts { get; set; }
        int? PollID { get; set; }
        DateTime Posted { get; set; }
        short Priority { get; set; }
        string Topic { get; set; }
        int TopicID { get; set; }
        int? TopicMovedID { get; set; }
        int UserID { get; set; }
        string UserName { get; set; }
        int Views { get; set; }
    }
}
