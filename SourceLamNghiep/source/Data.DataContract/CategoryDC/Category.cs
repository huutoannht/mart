using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Data.DataContract.ProductDC;

namespace Data.DataContract.CategoryDC
{
    [Serializable]
    [DataContract]
    public class Category : BaseDC
    {
        public Category()
        {
            Childs = new List<Category>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid? ParentId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public int SortOrder { get; set; }

        [DataMember]
        public CategoryType CategoryType { get; set; }

        //ref

        [DataMember]
        public int LevelPadding { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public int OrderNumber { get; set; }

        [DataMember]
        public string LevelTreeView { get; set; }

        [DataMember]
        public string ParentLevelTreeView { get; set; }

        [DataMember]
        public List<Category> Childs { get; set; }

        [DataMember]
        public List<Product> Products { get; set; }

        [DataMember]
        public int ProductCount { get; set; }

        [DataMember]
        public List<CategoryImage> Images { get; set; }
    }
}
