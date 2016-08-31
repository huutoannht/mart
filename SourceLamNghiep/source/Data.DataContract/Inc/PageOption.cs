using System;
using System.Runtime.Serialization;

namespace Data.DataContract
{
    [DataContract]
    [Serializable]
    public class PageOption
    {
        private int _pageNumber = 1;

        public PageOption()
        {
            PageSize = int.MaxValue;
        }

        public PageOption(int pageNumer, int pageSize)
            : this()
        {
            PageNumber = pageNumer;
            PageSize = pageSize;
        }

        /// <summary>
        /// Default equals int.MaxValue
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value > 1 ? value : 1; }
        }

        public int PageStartIndex
        {
            get { return (PageNumber <= 1 || PageSize == int.MaxValue) ? 0 : (PageNumber - 1) * PageSize; }
        }

        public bool IsValid
        {
            get { return PageSize != int.MaxValue; }
        }
    }
}
