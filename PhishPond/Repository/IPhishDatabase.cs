using System;
using System.Linq;
using System.Data.Linq;
using PhishPond.Concrete;
using System.Collections.Generic;
using TheCore.Interfaces;
using TheCore.Repository;
using TheCore.Guess;

namespace PhishPond.Repository.LinqToSql
{
    public interface IPhishDatabase : IDisposable
    {
        void Delete<TEntity>(TEntity instance) where TEntity : class;
        void DeleteAll<TEntity>(IEnumerable<TEntity> instances) where TEntity : class;
        IList<TEntity> DeleteChangeSet<TEntity>() where TEntity : class;
        ITable GetEditable<TEntity>() where TEntity : class;
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
        void Insert<TEntity>(TEntity instance) where TEntity : class;
        void InsertAll<TEntity>(IEnumerable<TEntity> instances) where TEntity : class;
        IList<TEntity> InsertChangeSet<TEntity>() where TEntity : class;
        IQueryable<IPhoto> PhishPhotoDataSource { get; }
        IQueryable<IPoster> PhishPosterDataSource { get; }
        IQueryable<ISet> PhishSetDataSource { get; }
        IQueryable<IShow> PhishShowDataSource { get; }
        IQueryable<ISong> PhishSongDataSource { get; }
        IQueryable<ITicketStub> PhishTicketStubDataSource { get; }
        IQueryable<ITour> PhishTourDataSource { get; }
        IQueryable<IVideo> PhishVideoDataSource { get; }
        IQueryable<IPost> PostDataSource { get; }
        IQueryable<ITopic> TopicDataSource { get; }
        IQueryable<ISetSong> SetSongDataSource { get; }
        IQueryable<IGuessWholeShow> GuessWholeShowDataSource { get; }
        IQueryable<IProfile> ProfileDataSource { get; }
        IQueryable<IFavoriteVersion> FavoriteVersionDataSource { get; }
        IEnumerable<IGetFavoriteVersionResult> GetFavoriteVersions(Guid userId, string album);
        IEnumerable<IGetAllVersions> GetAllVersions(Guid songId);
        IQueryable<IArt> ArtDataSource { get; }
        IQueryable<IMyShow> MyShowDataSource { get; }
        IQueryable<IMyShowPoster> MyShowPosterDataSource { get; }
        IQueryable<IMyShowTicketStub> MyShowTicketStubDataSource { get; }
        IQueryable<IMyShowArt> MyShowArtDataSource { get; }
        IQueryable<IAnalysis> AnalysisDataSource { get; }
        IQueryable<IWantedList> WantedListDataSource { get; }
        IQueryable<IUser> UserDataSource { get; }
        Table<Photo> Photos { get; }
        Table<Poster> Posters { get; }
        Table<Set> Sets { get; }
        Table<Show> Shows { get; }
        Table<Song> Songs { get; }
        Table<TicketStub> TicketStubs { get; }
        Table<Tour> Tours { get; }
        Table<Post> Posts { get; }
        IList<TEntity> UpdateChangeSet<TEntity>() where TEntity : class;
        Table<Video> Videos { get; }
        Table<SetSong> SetSongs { get; }
        Table<Topic> Topics { get; }
        Table<GuessWholeShow> GuessWholeShows { get; }
        Table<Profile> Profiles { get; }
        Table<FavoriteVersion> FavoriteVersions { get; }
        void SubmitChanges();
    }
}
