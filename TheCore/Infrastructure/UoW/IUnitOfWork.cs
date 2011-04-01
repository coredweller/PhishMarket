
namespace TheCore.Infrastructure
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
