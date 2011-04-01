using StructureMap.Configuration.DSL;
using TheCore.Configuration;

namespace TheCore
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<IAppConfigManager>()
                .Singleton()
                .Use<AppConfigManager>();
        }
    }
}
