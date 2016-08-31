using System;
using StructureMap;

namespace Share.DI
{
    public class Configuration
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
            initialize.AddRegistry<CommonRegistry>();

            Db.ImplementSQL.Configuration.Instance.Configure(initialize);

            initialize.Scan(x =>
            {
                x.Assembly("Service.ContractImplement");
                x.WithDefaultConventions();
            });
        }
    }
}
