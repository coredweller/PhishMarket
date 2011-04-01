using System;

namespace TheCore.Interfaces
{
    public interface ISong : IEntity
    {
        Guid SongId { get; set; }
        string SongName { get; set; }
        string Notes { get; }
        string SpecialAppearances { get; }
        string Album { get; }
        short? Order { get; }
        double? Length { get; set; }
        short? JamStyle { get; }
        bool Cover { get; }
        string Abbreviation { get; set; }

        bool SongIsEqual(ISong song);
    }
}

public enum JamType
{
    None = 0,
    Type1 = 1,
    Type2 = 2,
    NotSelected = 13
}

