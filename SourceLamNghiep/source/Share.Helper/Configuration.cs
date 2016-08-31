using System;
using ML.Common;
using ML.Utils.Cache;
using Share.Helper.Cache;
using Share.Helper.Client;
using StructureMap;

namespace Share.Helper
{
    public sealed class Configuration
    {
        #region Singleton

        private readonly static Lazy<Configuration> LazyInstance = new Lazy<Configuration>(() => new Configuration());

        public static Configuration Instance
        {
            get { return LazyInstance.Value; }
        }

        #endregion

        public void Configure()
        {
            ObjectFactory.Initialize(Configure);
        }

        public void Configure(IInitializationExpression initialize)
        {
            initialize.For<ICryptoHelper>().Use<CryptoHelper>();

            //service
            if (std.GetAppSetting("ServiceHelperBy").ToLower() == "internal")
            {
                initialize.For<IServiceHelper>().Use<InternalServiceHelper>();
            }
            else
            {
                initialize.For<IServiceHelper>().Use<WCFServiceHelper>().SelectConstructor(() => new WCFServiceHelper());
            }

            //cache
            switch (std.GetAppSetting(ConstKeys.CacheProvider).ToLower())
            {
                case "sharedcache":
                    initialize.For<ICache>().Use<SharedCacheManager>();
                    break;

                case "memcached":
                    initialize.For<ICache>().Use<MemcachedManager>();
                    break;

                default:
                    initialize.For<ICache>().Use<AspCacheManager>();
                    break;

            }

            initialize.For<ICacheHelper>().Use<CacheHelper>();

        }
    }
}
