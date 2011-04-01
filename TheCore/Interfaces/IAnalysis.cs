using System;

namespace TheCore.Interfaces
{
    public interface IAnalysis : IEntity
    {
        Guid AnalysisId { get; set; }
        Guid SetSongId { get; set; }
        Guid UserId { get; set; }

        Guid? MyShowId { get; set; }
        int? Rating { get; set; }
        string Notes { get; set; }
    }
}
