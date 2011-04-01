using System;

namespace TheCore.Repository
{
    public interface IGetAllVersions
    {
        Guid SetSongId { get; set; }
        double? SetSongLength { get; set; }
        short? SetSongOrder { get; set; }
        Guid SetId { get; set; }
        short? SetNumber { get; set; }
        bool Encore { get; set; }
        int? ShowOrder { get; set; }
        DateTime? ShowDate { get; set; }
        string ShowName { get; set; }
        Guid ShowId { get; set; }
        string City { get; set; }
        string State { get; set; }
        Guid? TourId { get; set; }
        string VenueName { get; set; }
    }
}
