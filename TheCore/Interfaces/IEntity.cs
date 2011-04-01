using System;

namespace TheCore.Interfaces
{
    public interface IEntity
    {
        DateTime CreatedDate { get; set; }

        DateTime? UpdatedDate { get; set; }

        DateTime? DeletedDate { get; set; }

        bool Deleted { get; set; }
    }
}
