using System;
using TheCore;
using TheCore.Helpers;
using TheCore.Infrastructure;

namespace PhishPond.Repository.LinqToSql
{
    public class UnitOfWork : DisposableResource, IUnitOfWork
    {
        private readonly IPhishDatabase _database;
        private bool _isDisposed;

        public UnitOfWork(IPhishDatabase database)
        {
            Checks.Argument.IsNotNull(database, "database");
            _database = database;
        }


        public UnitOfWork(IPhishDatabaseFactory factory)
            : this(factory.Get())
        {
        }

        public virtual void Commit()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            
            _database.SubmitChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
