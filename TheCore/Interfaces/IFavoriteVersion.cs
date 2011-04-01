using System;

namespace TheCore.Interfaces
{
    public interface IFavoriteVersion : IEntity
    {
        Guid FavoriteVersionId { get; set; }
        Guid? SetSongId { get; set; }
        Guid SongId { get; set; }
        Guid UserId { get; set; }
    }
}
