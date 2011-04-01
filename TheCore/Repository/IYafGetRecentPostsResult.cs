using System;
namespace TheCore.Repository
{
    public interface IYafGetRecentPostsResult
    {
        string BlogPostID { get; set; }
        string DeleteReason { get; set; }
        DateTime? Edited { get; set; }
        string EditReason { get; set; }
        int Flags { get; set; }
        int Indent { get; set; }
        string IP { get; set; }
        bool? IsApproved { get; set; }
        bool? IsDeleted { get; set; }
        bool IsModeratorChanged { get; set; }
        string Message { get; set; }
        int MessageID { get; set; }
        int Position { get; set; }
        DateTime Posted { get; set; }
        int? ReplyTo { get; set; }
        int TopicID { get; set; }
        int UserID { get; set; }
        string UserName { get; set; }
    }
}
