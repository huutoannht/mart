using System;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class AppSetting : BaseEntity
    {
        public AppSettingType SettingType { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
