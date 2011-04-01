using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Guess;
using PhishPond.Concrete;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class TopicRepository : BaseRepository<ITopic, Topic>, ITopicRepository
    {
        LogWriter writer = new LogWriter();
        public TopicRepository(IPhishDatabase database) : base(database) { }

        public TopicRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<ITopic> GetAll()
        {
            return Database.TopicDataSource.Where(x => x.Deleted == false);
        }

        public IList<ITopic> FindAll()
        {
            return GetAll().OrderBy(t => t.StartDate).ToList();
        }

        public ITopic FindByTopicId(Guid id)
        {
            return GetAll().SingleOrDefault(t => t.TopicId == id);
        }

        public ITopic FindByShowId(Guid showId)
        {
            return GetAll().SingleOrDefault(t => t.ShowId == showId);
        }

        public ITopic FindByTourId(Guid tourId) 
        {
            return GetAll().SingleOrDefault(t => t.TourId == tourId);
        }

        public ITopic FindByTopicName(string topicName)
        {
            return GetAll().SingleOrDefault(t => t.TopicName.ToLower() == topicName.ToLower());
        }

        public override void Add(ITopic entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(t => t.TopicId == entity.TopicId))
            {
                writer.WriteLine("A Topic with an id={0}".FormatWith(entity.TopicId));
                throw new AlreadyExistsException("A Topic with an id={0}".FormatWith(entity.TopicId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(ITopic entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
