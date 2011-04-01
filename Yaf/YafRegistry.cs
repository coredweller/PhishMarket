using StructureMap.Configuration.DSL;
using TheCore.Repository;
using Yaf.Repository.LinqToSql;

namespace Yaf
{
    public class YafRegistry : Registry
    {
        public YafRegistry()
        {
            For<IConnectionString>()
                .Singleton()
                .Use<ConnectionString>()
                .WithCtorArg("connectionStringKey").EqualTo("yafnet");

            For<ILogWriter>()
                .HybridHttpOrThreadLocalScoped()
                .Use<DebuggerWriter>();
            SelectConstructor<DebuggerWriter>(() => new DebuggerWriter());

            For<IYafRepository>()
                .HybridHttpOrThreadLocalScoped()
                .Use<YafRepository>()
                .Ctor<IYafDatabaseFactory>("factory").IsTheDefault();

            SelectConstructor<YafRepository>(() => new YafRepository((IYafDatabaseFactory)null));

            For<IYafDatabase>()
                .HybridHttpOrThreadLocalScoped()
                .Use<YafDatabase>()
                .Ctor<IConnectionString>("connectionString").IsTheDefault();
            SelectConstructor<YafDatabase>(() => new YafDatabase((IConnectionString)null));

            For<IYafDatabaseFactory>()
                .HybridHttpOrThreadLocalScoped()
                .Use<YafDatabaseFactory>();
        }
    }
}
