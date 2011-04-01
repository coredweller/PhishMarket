using System;

namespace TheCore.Interfaces
{
    public interface IProfile : IEntity
    {
        Guid ProfileId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string FavoriteAlbum { get; set; }
        Guid? FavoriteLiveShow { get; set; }
        Guid? FavoriteStudioSong { get; set; }
        Guid? FavoriteLiveSong { get; set; }
        Guid? FavoriteTour { get; set; }
        string LoveAboutPhish { get; set; }
        string AboutYou { get; set; }
        Guid UserId { get; set; }

    }
}
