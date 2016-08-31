using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataContract;
using Data.DataContract.DataLogDC;
using ML.Common;
using Models.DataLog;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class CustomerLogController : AuthorisedController
    {
        public CustomerLogController() : base(BePage.CustomerLog) { }

        [View]
        public ActionResult Index(DataLogIndexModel model)
        {
            if (!model.SortDirection.HasValue)
            {
                model.SortDirection = SortDirection.Desc;
            }

            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = "LogDate";
            }
            if (model.TextSearch!=null||model.FromDate!=null||model.ToDate!=null)
            {
                var filter = new FindRequest
                {
                    Table = TableLog.Customer,
                    TextSearch = model.TextSearch,
                    FromDate = DateTimeHelper.ConvertToUtcNull(model.FromDate, CurrentUserTimeZoneId).ToStartDateTimeNull(),
                    ToDate = DateTimeHelper.ConvertToUtcNull(model.ToDate, CurrentUserTimeZoneId).ToEndDateTimeNull(),
                    SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) }),
                    PageOption = new PageOption { PageSize = model.Pagination.PageSize, PageNumber = model.Pagination.CurrentPageIndex }
                };

                var response = ServiceHelper.DataLog.ExecuteDispose(s => s.FindDataLogs(filter));
                model.Results = response.Results.MapList<DataLogModel>();
                model.Pagination.TotalRecords = response.TotalRecords;
                model.Pagination.ShowPaging = false;
                model.FilterVisible = true;
            }
            else
            {
                model.Results = new List<DataLogModel>();
                model.Pagination.TotalRecords = 0;
                model.Pagination.ShowPaging = false;
                model.FilterVisible = true;
            }
           

            return View(model);
        }
    }
}