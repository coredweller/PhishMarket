using TheCore;
using TheCore.Helpers;
using System.Data.Linq;

namespace PhishPond.Repository.LinqToSql
{
    public class PhishDatabaseFactory : DisposableResource, IPhishDatabaseFactory
    {
        private readonly string _connectionString;
        private readonly ILogWriter _logWriter;
        public IPhishDatabase _database;

        public PhishDatabaseFactory(IConnectionString connectionString, ILogWriter logWriter)
        {
            Checks.Argument.IsNotNull(connectionString, "connectionString");

            _connectionString = connectionString.Value;
            _logWriter = logWriter;
        }


        #region IPhishDatabaseFactory Members

        public IPhishDatabase Get()
        {
            if (_database == null)
            {
                DataLoadOptions options = new DataLoadOptions();

                //options.LoadWith<Tour>(tour => tour.Shows);

                //options.LoadWith<Show>(show => show.Sets);

                //options.LoadWith<Set>(set => set.SetSongs);
                //options.LoadWith<Set>(set => set.Show);

                //options.LoadWith<SetSong>(setSong => setSong.Song);
                //options.LoadWith<SetSong>(setSong => setSong.Set);
                

                //options.LoadWith<Song>(song => song.set

                _database = new PhishDatabase(_connectionString) 
                    { 
                        LoadOptions = options, 
                        DeferredLoadingEnabled = true, 
                        Log = (_logWriter == null ? null : _logWriter.Get()) 
                    };
            }

            return _database;
        }

        #endregion
    }
}
