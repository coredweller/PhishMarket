using System;

namespace TheCore.Interfaces
{
    public interface IAlbum
    {
        Guid AlbumId { get; set; }

        int YearReleased { get; set; }
        string AlbumName { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
