using System;
using System.Runtime.Serialization;

namespace Share.Helper.Models
{
    [Serializable]
    [DataContract]
    public class PaginationModel
    {
        public PaginationModel()
        {
	        ShowPaging = true;
            PageSize = SiteSettings.DefaultPageSize;
            CurrentPageIndex = 1;
        }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int CurrentPageIndex { get; set; }

        [DataMember]
        public int PageCount { get; private set; }

        [DataMember]
        public int DisplayedPageStartIndex { get; private set; }

        [DataMember]
        public int DisplayedPageEndIndex { get; private set; }

        [DataMember]
        public string PageClickUrl { private get; set; }

        [DataMember]
		public bool ShowPaging { get; set; }

        [DataMember]
		public string PaginationId { get; set; }

        
        /// <summary>
        /// Format xxxx({0}). PageClickFunction ~ xxx, param {0} is page index and it must be require.
        ///  </summary>
        [DataMember]
        public string PageClickFunction { get; set; }

        /// <summary>
        /// Format xxxx(0}). PageSizeClickFunction ~ xxx, param {0} is HTMLSelectElement.
        ///  </summary>
        [DataMember]
        public string PageSizeClickFunction { get; set; }

        public void CalculatePaging()
        {
            PageCount = (int)(Math.Ceiling(TotalRecords / (double)PageSize));
            CurrentPageIndex = CurrentPageIndex <= 0 ? 1 : (CurrentPageIndex > PageCount ? PageCount : CurrentPageIndex);

            DisplayedPageStartIndex = (CurrentPageIndex - SiteSettings.DefaultPageCount / 2) < 1
                                        ? 1
                                        : CurrentPageIndex - SiteSettings.DefaultPageCount / 2;

            DisplayedPageEndIndex = (CurrentPageIndex + SiteSettings.DefaultPageCount / 2) > PageCount
                                        ? PageCount
                                        : CurrentPageIndex + SiteSettings.DefaultPageCount / 2;
        }

        public string GetExecutePageClick(int pageIndex, bool alwaysSetEmptyFunction = false)
        {
            if (!alwaysSetEmptyFunction)
            {
                pageIndex = pageIndex <= 0 ? 1 : (pageIndex > PageCount ? PageCount : pageIndex);

                if (!string.IsNullOrEmpty(PageClickFunction))
                {
                    return string.Format("onclick={0}", string.Format(PageClickFunction, pageIndex));
                }

                if (!string.IsNullOrEmpty(PageClickUrl))
                {
                    return string.Format("class=cssLink onclick=void(0) link={0}",
                        String.Format("{0}{1}{2}={3}", PageClickUrl, (PageClickUrl.IndexOf("?") >= 0 ? "&" : "?"), SiteSettings.DefaultPageQueryParameter, pageIndex));
                }
            }

            return "onclick=void(0)";
        }
    }
}