using System;

namespace TheCore.Repository
{
    public interface IGetFavoriteVersionResult
    {
        Guid SongId { get; set; }
        string SongName { get; set; }
        short? Order { get; set; }
        string Album { get; set; }
        double? Length { get; set; }
        Guid? FavoriteVersionId { get; set; }
        Guid? UserId { get; set; }
        Guid? SetSongId { get; set; }
        double? SetSongLength { get; set; }
        short? SetSongOrder { get; set; }
        Guid? SetId { get; set; }
        short? SetNumber { get; set; }
        bool? Encore { get; set; }
        string City { get; set; }
        int? ShowOrder { get; set; }
        DateTime? ShowDate { get; set; }
        string ShowName { get; set; }
        Guid? ShowId { get; set; }
        string State { get; set; }
        Guid? TourId { get; set; }
        string VenueName { get; set; }

    }
}
