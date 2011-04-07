using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheCore.Interfaces
{
    public interface IUser
    {
        Guid UserId { get; set; }
        string UserName { get; set; }
    }
}
