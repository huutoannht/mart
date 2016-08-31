using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ML.Common;

namespace Data.DataContract
{
    [DataContract]
    [Serializable]
    public class SortOption
    {
        public SortOption(params SortItem[] items)
        {
            Items = new List<SortItem>(items);
        }

        [DataMember]
        public List<SortItem> Items { get; set; }

        public string ItemsBySqlString
        {
            get
            {
                return string.Join(", ", Items.Where(s => !string.IsNullOrEmpty(s.FieldName) && s.Direction != SortDirection.None)
                    .Select(s => string.Format("{0} {1}", s.FieldName, s.Direction)));
            }
        }

        public void AddSort(string fieldName, SortDirection direction)
        {
            Items.RemoveAll(i => i.FieldName.EqualsString(fieldName));
            Items.Add(new SortItem { FieldName = fieldName, Direction = direction });
        }
    }

    [DataContract]
    [Serializable]
    public class SortItem
    {
        public SortItem()
        {
        }

        public SortItem(string fieldName, SortDirection direction)
            : this()
        {
            FieldName = fieldName;
            Direction = direction;
        }

        [DataMember]
        public string FieldName { get; set; }

        [DataMember]
        public SortDirection Direction { get; set; }
    }
}
