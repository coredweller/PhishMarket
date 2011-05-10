using System;

namespace TheCore.Interfaces
{
    public interface IPoster : IEntity
    {
        Guid PosterId { get; set; }
        string Creator { get; set; }
        string Notes { get; set; }
        Guid PhotoId { get; set; }

        //For example number/total 1;23/500 which is #123 out of 500 total pieces of released art
        int? Total { get; set; }
        int? Number { get; set; }

        double? Width { get; set; }
        double? Length { get; set; }
        string Technique { get; set; }
        short? Status { get; set; }
        DateTime? ReleaseDate { get; set; }
        string Title { get; set; }
        Guid? UserId { get; set; }
        Guid? ShowId { get; set; }
    }

    public enum PosterStatus
    {
        Official = 1,
        Unofficial = 2,
        Charity = 3,
        Phan = 4,
        Other = 9
    }
}
