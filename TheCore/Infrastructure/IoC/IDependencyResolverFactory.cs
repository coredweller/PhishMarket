
namespace TheCore.Infrastructure
{
    using System;

    public interface IDependencyResolverFactory
    {
        IDependencyResolver CreateInstance();
    }
}