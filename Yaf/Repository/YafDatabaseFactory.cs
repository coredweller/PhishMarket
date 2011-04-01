using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCore;
using TheCore.Helpers;
using System.Data.Linq;

namespace Yaf.Repository.LinqToSql
{
    public class YafDatabaseFactory : DisposableResource, IYafDatabaseFactory
    {
        private readonly string _connectionString;
        private readonly ILogWriter _logWriter;
        public IYafDatabase _database;

        public YafDatabaseFactory(IConnectionString connectionString, ILogWriter logWriter)
        {
            Checks.Argument.IsNotNull(connectionString, "connectionString");

            _connectionString = connectionString.Value;
            _logWriter = logWriter;
        }


        #region IYafDatabaseFactory Members

        public IYafDatabase Get()
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

                _database = new YafDatabase(_connectionString) 
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
