using System;
using System.Collections.Generic;
using System.Linq;
using TheCore.Interfaces;
using TheCore.Helpers;
using PhishPond.Concrete;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository.LinqToSql
{
    public class GuessWholeShowRepository : BaseRepository<IGuessWholeShow, GuessWholeShow>, IGuessWholeShowRepository
    {
        LogWriter writer = new LogWriter();
        public GuessWholeShowRepository(IPhishDatabase database) : base(database) { }

        public GuessWholeShowRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IGuessWholeShow> GetAll()
        {
            return Database.GuessWholeShowDataSource;
        }

        public IList<IGuessWholeShow> FindAll()
        {
            return GetAll().OrderBy(t => t.TopicId).ToList();
        }

        public IGuessWholeShow FindByGuessWholeShowId(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.GuessWholeShowId == id);
        }

        public IGuessWholeShow FindBySetId(Guid setId)
        {
            return GetAll().SingleOrDefault(x => x.SetId == setId);
        }

        public IList<IGuessWholeShow> FindByTopicId(Guid topicId)
        {
            return GetAll().Where(x => x.TopicId == topicId).OrderBy(x => x.TopicId).ToList();
        }

        public IList<IGuessWholeShow> FindByTopicIdAndUserId(Guid topicId, Guid userId)
        {
            return GetAll().Where(x => x.TopicId == topicId && x.UserId == userId).ToList();
        }

        public override void Add(IGuessWholeShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(guessWholeShow => guessWholeShow.GuessWholeShowId == entity.GuessWholeShowId))
            {
                writer.WriteLine("A GuessWholeShow with an id={0}".FormatWith(entity.GuessWholeShowId));
                throw new AlreadyExistsException("A GuessWholeShow with an id={0}".FormatWith(entity.GuessWholeShowId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IGuessWholeShow entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    }
}
