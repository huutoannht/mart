using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public string Code { get; set; }

        public string ImagePath { get; set; }

        public int SortOrder { get; set; }

        public CategoryType CategoryType { get; set; }

        //Ref

        public List<CategoryImage> Images { get; set; }
    }
}
