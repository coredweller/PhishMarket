using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Interfaces;
using PhishPond.Concrete;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class PostRepository : BaseRepository<IPost, Post>, IPostRepository
    {
        LogWriter writer = new LogWriter();
        public PostRepository(IPhishDatabase database) : base(database) { }

        public PostRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IPost> GetAll()
        {
            return Database.PostDataSource.Where(x => x.Deleted == false);
        }

        public IList<IPost> FindAll()
        {
            return GetAll().OrderByDescending(p => p.PostedDate).ToList();
        }

        public IPost FindByPostId(Guid id)
        {
            return GetAll().SingleOrDefault(p => p.PostId == id);
        }

        public IPost FindByTitle(string title)
        {
            return GetAll().SingleOrDefault(p => p.Title == title);
        }

        public override void Add(IPost entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(p => p.PostId == entity.PostId))
            {
                writer.WriteLine("A Post with an id={0}".FormatWith(entity.PostId));
                throw new AlreadyExistsException("A Post with an id={0}".FormatWith(entity.PostId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IPost entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
