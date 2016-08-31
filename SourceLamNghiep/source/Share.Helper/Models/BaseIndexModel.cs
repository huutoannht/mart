using System;
using System.Collections.Generic;
using Data.DataContract;

namespace Share.Helper.Models
{
	[Serializable]
	public class BaseIndexModel<T>
	{
		public BaseIndexModel()
		{
			Pagination = new PaginationModel();
		}

		public List<T> Results = new List<T>();

		public PaginationModel Pagination { get; set; }

        public SortDirection? SortDirection { get; set; }

        public string SortBy { get; set; }

        public bool FilterVisible { get; set; }

        public bool EventFiredFromSortButton { get; set; }

	    public void InitSortInfo()
	    {
            if (!SortDirection.HasValue)
            {
                SortDirection = Data.DataContract.SortDirection.Asc;
            }
            else
            {
                if (EventFiredFromSortButton)
                {
                    SortDirection = SortDirection == Data.DataContract.SortDirection.Asc
                        ? Data.DataContract.SortDirection.Desc : Data.DataContract.SortDirection.Asc;
                }
            }

            EventFiredFromSortButton = false;
	    }
	}
}