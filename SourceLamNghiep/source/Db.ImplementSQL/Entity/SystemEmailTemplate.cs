using System;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class SystemEmailTemplate : BaseEntity
    {
        public SystemEmailTemplateType Type { get; set; }

        public string Language { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
