using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using TheCore.Repository;

namespace Yaf.Repository.LinqToSql
{
    public interface IYafDatabase : IDisposable
    {
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
        void Delete<TEntity>(TEntity instance) where TEntity : class;
        void DeleteAll<TEntity>(IEnumerable<TEntity> instances) where TEntity : class;
        IList<TEntity> DeleteChangeSet<TEntity>() where TEntity : class;
        ITable GetEditable<TEntity>() where TEntity : class;
        //IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
        void Insert<TEntity>(TEntity instance) where TEntity : class;
        void InsertAll<TEntity>(IEnumerable<TEntity> instances) where TEntity : class;
        IList<TEntity> InsertChangeSet<TEntity>() where TEntity : class;
        IEnumerable<IYafGetRecentPostsResult> GetRecentPosts { get; }
        IEnumerable<IYafGetRecentTopicsResult> GetRecentTopics { get; }
        IYafGetUserResult GetUserIdFromYafId(int yafUserId);
    }
}
