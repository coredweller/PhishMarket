namespace TheCore.Repository
{
    using System;
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}
