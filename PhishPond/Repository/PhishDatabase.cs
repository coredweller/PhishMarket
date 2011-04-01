using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using PhishPond.Concrete;
using System.Data.Linq;
using TheCore.Interfaces;
using TheCore.Repository;
using TheCore.Guess;

namespace PhishPond.Repository.LinqToSql
{
    public partial class PhishDatabase : IPhishDatabase
    {
        public PhishDatabase(IConnectionString connectionString) : this(connectionString.Value) { }

        
        public virtual IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return GetTable<TEntity>();
        }

        public IQueryable<IPhoto> PhishPhotoDataSource
        {
            get { return GetQueryable<Photo>().Cast<IPhoto>(); }
        }

        public IQueryable<IPoster> PhishPosterDataSource
        {
            get { return GetQueryable<Poster>().Cast<IPoster>(); }
        }

        public IQueryable<ISet> PhishSetDataSource
        {
            get { return GetQueryable<Set>().Cast<ISet>(); }
        }

        public IQueryable<IShow> PhishShowDataSource
        {
            get { return GetQueryable<Show>().Cast<IShow>(); }
        }

        public IQueryable<ISong> PhishSongDataSource
        {
            get { return GetQueryable<Song>().Cast<ISong>(); }
        }

        public IQueryable<ITicketStub> PhishTicketStubDataSource
        {
            get { return GetQueryable<TicketStub>().Cast<ITicketStub>(); }
        }

        public IQueryable<ITour> PhishTourDataSource
        {
            get { return GetQueryable<Tour>().Cast<ITour>(); }
        }

        public IQueryable<IVideo> PhishVideoDataSource
        {
            get { return GetQueryable<Video>().Cast<IVideo>(); }
        }

        public IQueryable<IPost> PostDataSource
        {
            get { return GetQueryable<Post>().Cast<IPost>(); }
        }

        public IQueryable<ITopic> TopicDataSource
        {
            get { return GetQueryable<Topic>().Cast<ITopic>(); }
        }

        public IQueryable<ISetSong> SetSongDataSource
        {
            get { return GetQueryable<SetSong>().Cast<ISetSong>(); }
        }

        public IQueryable<IGuessWholeShow> GuessWholeShowDataSource
        {
            get { return GetQueryable<GuessWholeShow>().Cast<IGuessWholeShow>(); }
        }

        public IQueryable<IProfile> ProfileDataSource
        {
            get { return GetQueryable<Profile>().Cast<IProfile>(); }
        }

        public IQueryable<IFavoriteVersion> FavoriteVersionDataSource
        {
            get { return GetQueryable<FavoriteVersion>().Cast<IFavoriteVersion>(); }
        }

        public IEnumerable<IGetFavoriteVersionResult> GetFavoriteVersions(Guid userId, string album)
        {
            return profileGetFavoriteVersions(userId, album).Cast<IGetFavoriteVersionResult>();
        }

        public IEnumerable<IGetAllVersions> GetAllVersions(Guid songId)
        {
            return profileGetAllVersions(songId).Cast<IGetAllVersions>();
        }

        public IQueryable<IArt> ArtDataSource
        {
            get { return GetQueryable<Art>().Cast<IArt>(); }
        }

        public IQueryable<IMyShow> MyShowDataSource
        {
            get { return GetQueryable<MyShow>().Cast<IMyShow>(); }
        }

        public IQueryable<IMyShowPoster> MyShowPosterDataSource
        {
            get { return GetQueryable<MyShowPoster>().Cast<IMyShowPoster>(); }
        }

        public IQueryable<IMyShowTicketStub> MyShowTicketStubDataSource
        {
            get { return GetQueryable<MyShowTicketStub>().Cast<IMyShowTicketStub>(); }
        }

        public IQueryable<IMyShowArt> MyShowArtDataSource
        {
            get { return GetQueryable<MyShowArt>().Cast<IMyShowArt>(); }
        }

        public IQueryable<IAnalysis> AnalysisDataSource
        {
            get { return GetQueryable<Analysis>().Cast<IAnalysis>(); }
        }

        public IQueryable<IWantedList> WantedListDataSource
        {
            get { return GetQueryable<WantedList>().Cast<IWantedList>(); }
        }

















        [DebuggerStepThrough]
        public virtual ITable GetEditable<TEntity>() where TEntity : class {
            return GetTable<TEntity>();
        }
        
        [DebuggerStepThrough]
        public virtual IList<TEntity> InsertChangeSet<TEntity>() where TEntity : class {
            var items = GetChangeSet().Inserts.Where( x => x.GetType() == typeof(TEntity) );
            return items.Cast<TEntity>().ToList();
        }
        
        [DebuggerStepThrough]
        public virtual IList<TEntity> UpdateChangeSet<TEntity>() where TEntity : class {
            var items = GetChangeSet().Updates.Where( x => x.GetType() == typeof(TEntity) );
            return items.Cast<TEntity>().ToList();
        }
        
        [DebuggerStepThrough]
        public virtual IList<TEntity> DeleteChangeSet<TEntity>() where TEntity : class {
            var items = GetChangeSet().Deletes.Where( x => x.GetType() == typeof(TEntity) );
            return items.Cast<TEntity>().ToList();
        }

        [DebuggerStepThrough]
        public void Insert<TEntity>( TEntity instance ) where TEntity : class {
            GetEditable<TEntity>().InsertOnSubmit( instance );
        }

        [DebuggerStepThrough]
        public void InsertAll<TEntity>( IEnumerable<TEntity> instances ) where TEntity : class {
            GetEditable<TEntity>().InsertAllOnSubmit( instances );
        }

        [DebuggerStepThrough]
        public void Delete<TEntity>( TEntity instance ) where TEntity : class {
            GetEditable<TEntity>().DeleteOnSubmit( instance );
        }

        [DebuggerStepThrough]
        public void DeleteAll<TEntity>( IEnumerable<TEntity> instances ) where TEntity : class {
            GetEditable<TEntity>().DeleteAllOnSubmit( instances );
        }

        protected new void Dispose( bool disposing ) {

            if (base.Connection != null)
                if (base.Connection.State != System.Data.ConnectionState.Closed)
                {
                    base.Connection.Close();
                    base.Connection.Dispose();
                }

            base.Dispose();            

        }

       
    }
}