using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore.Interfaces;
using PhishPond.Concrete;
using PhishPond.Repository.LinqToSql;
using TheCore.Helpers;
using TheCore.Repository;
using TheCore.Exceptions;

namespace PhishPond.Repository
{
    public class MyShowArtRepository : BaseRepository<IMyShowArt, MyShowArt>, IMyShowArtRepository
    {
        LogWriter writer = new LogWriter();
        public MyShowArtRepository(IPhishDatabase database) : base(database) { }

        public MyShowArtRepository(IPhishDatabaseFactory factory) : base(factory) { }

        private IQueryable<IMyShowArt> GetAll()
        {
            return Database.MyShowArtDataSource.Where(x => x.Deleted == false);
        }

        public IQueryable<IMyShowArt> FindAll()
        {
            return GetAll().OrderBy(s => s.MyShowId);
        }

        public IMyShowArt FindByMyShowArtId(Guid myShowArtId)
        {
            return GetAll().SingleOrDefault(myShow => myShow.MyShowArtId == myShowArtId);
        }

        public override void Add(IMyShowArt entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            entity.CreatedDate = DateTime.Now;

            if (GetAll().Any(myShow => myShow.MyShowArtId == entity.MyShowArtId))
            {
                writer.WriteLine("A MyShowArt with an id={0}".FormatWith(entity.MyShowArtId));
                throw new AlreadyExistsException("A MyShowArt with an id={0}".FormatWith(entity.MyShowArtId));
            }
            else
            {
                base.Add(entity);
            }
        }

        public override void Remove(IMyShowArt entity)
        {
            Checks.Argument.IsNotNull(entity, "entity");

            base.Remove(entity);
        }
    
    }
}
