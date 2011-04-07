using StructureMap.Configuration.DSL;
using PhishPond.Repository;
using PhishPond.Repository.LinqToSql;
using TheCore.Repository;

namespace PhishPond
{
    public class PhishPondRegistry : Registry
    {

        public PhishPondRegistry()
        {
            For<IConnectionString>()
                .Singleton()
                .Use<ConnectionString>()
                .WithCtorArg("connectionStringKey").EqualTo("PhishPond");

            For<TheCore.Infrastructure.IUnitOfWork>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<PhishPond.Repository.LinqToSql.UnitOfWork>();
            SelectConstructor<PhishPond.Repository.LinqToSql.UnitOfWork>(() => new PhishPond.Repository.LinqToSql.UnitOfWork((IPhishDatabaseFactory)null));

            For<ILogWriter>()
                .HybridHttpOrThreadLocalScoped()
                .Use<DebuggerWriter>();
            SelectConstructor<DebuggerWriter>(() => new DebuggerWriter());

            For<IPhishDatabase>()
                .HybridHttpOrThreadLocalScoped()
                .Use<PhishDatabase>()
                .Ctor<IConnectionString>("connectionString").IsTheDefault();
            SelectConstructor<PhishDatabase>(() => new PhishDatabase((IConnectionString)null));

            For<IPhishDatabaseFactory>()
                .HybridHttpOrThreadLocalScoped()
                .Use<PhishDatabaseFactory>();

            For<ITourRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<TourRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<TourRepository>(() => new TourRepository((IPhishDatabaseFactory)null, (IShowRepository)null));

            For<IShowRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ShowRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<ShowRepository>(() => new ShowRepository((IPhishDatabaseFactory)null));

            For<ISetRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<SetRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<SetRepository>(() => new SetRepository((IPhishDatabaseFactory)null));

            For<ISongRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<SongRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<SongRepository>(() => new SongRepository((IPhishDatabaseFactory)null));

            For<ITicketStubRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<TicketStubRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<TicketStubRepository>(() => new TicketStubRepository((IPhishDatabaseFactory)null, (IShowRepository)null));

            For<IPosterRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<PosterRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<PosterRepository>(() => new PosterRepository((IPhishDatabaseFactory)null, (IShowRepository)null));

            For<IPhotoRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<PhotoRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<PhotoRepository>(() => new PhotoRepository((IPhishDatabaseFactory)null));

            For<IVideoRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<VideoRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<VideoRepository>(() => new VideoRepository((IPhishDatabaseFactory)null));

            For<IPostRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<PostRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<PostRepository>(() => new PostRepository((IPhishDatabaseFactory)null));

            For<ITopicRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<TopicRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<TopicRepository>(() => new TopicRepository((IPhishDatabaseFactory)null));

            For<ISetSongRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<SetSongRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<SetSongRepository>(() => new SetSongRepository((IPhishDatabaseFactory)null));

            For<IGuessWholeShowRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<GuessWholeShowRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<GuessWholeShowRepository>(() => new GuessWholeShowRepository((IPhishDatabaseFactory)null));

            For<IProfileRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ProfileRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<ProfileRepository>(() => new ProfileRepository((IPhishDatabaseFactory)null));

            For<IFavoriteVersionRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<FavoriteVersionRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<FavoriteVersionRepository>(() => new FavoriteVersionRepository((IPhishDatabaseFactory)null));

            //For<ITempImageStorage>()
            //    .HybridHttpOrThreadLocalScoped()
            //    .Use<TempImageStorage>()
            //    .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            //SelectConstructor<TempImageStorage>(() => new TempImageStorage((IPhishDatabaseFactory)null));

            For<IArtRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ArtRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<ArtRepository>(() => new ArtRepository((IPhishDatabaseFactory)null, (IShowRepository)null));

            For<IMyShowRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<MyShowRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<MyShowRepository>(() => new MyShowRepository((IPhishDatabaseFactory)null));

            For<IMyShowPosterRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<MyShowPosterRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<MyShowPosterRepository>(() => new MyShowPosterRepository((IPhishDatabaseFactory)null));

            For<IMyShowTicketStubRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<MyShowTicketStubRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<MyShowTicketStubRepository>(() => new MyShowTicketStubRepository((IPhishDatabaseFactory)null));

            For<IMyShowArtRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<MyShowArtRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<MyShowArtRepository>(() => new MyShowArtRepository((IPhishDatabaseFactory)null));

            For<IAnalysisRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<AnalysisRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<AnalysisRepository>(() => new AnalysisRepository((IPhishDatabaseFactory)null));

            For<IWantedListRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<WantedListRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<WantedListRepository>(() => new WantedListRepository((IPhishDatabaseFactory)null));

            For<IPhishMarketUserRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<PhishMarketUserRepository>()
                .Ctor<IPhishDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<PhishMarketUserRepository>(() => new PhishMarketUserRepository((IPhishDatabaseFactory)null));
        }
    }
}
