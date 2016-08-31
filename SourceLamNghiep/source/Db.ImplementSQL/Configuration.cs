using System;
using Db.ImplementSQL;
using StructureMap;

namespace Db.ImplementSQL
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

        /// <summary>
        /// Configure repository for ISS
        /// </summary>
        /// <param name="initialize"></param>
        public void Configure(IInitializationExpression initialize)
        {
            AutoMapperConfiguration.Instance.Configure();

            initialize.Scan(x =>
            {
                x.Assembly("Db.Interfaces");
                x.Assembly("Db.ImplementSQL");

                x.WithDefaultConventions();
            });

        }
    }
}
