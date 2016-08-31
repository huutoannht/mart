using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models.Role;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;
using Data.DataContract;
using Data.DataContract.RoleDC;
using Share.Helper.Extension;
using Share.Messages;
using ML.Common;

namespace Site.Backend.Controllers
{
    public class RoleController : AuthorisedController
    {
        public RoleController() : base(BePage.Roles) { }

        [View]
        public ActionResult Index(RoleIndexModel model)
        {
            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = "Name";
            }

            var filter = new FindRequest
            {
                Name = model.Name,
                SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) }),
                PageOption = new PageOption { PageSize = model.Pagination.PageSize, PageNumber = model.Pagination.CurrentPageIndex }
            };

            var response = ServiceHelper.Role.ExecuteDispose(s => s.FindRoles(filter));
            model.Results = response.Results.MapList<RoleModel>();
            model.Pagination.TotalRecords = response.TotalRecords;

            return View(model);
        }

        [View]
        public ActionResult AddNew()
        {
            var model = new RoleModel
            {
                Permissions = LoadRolePermissions(Guid.Empty)
            };

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_roleInfo", model)
            });
        }

        [View]
        public ActionResult Edit(Guid id)
        {
            var model = ServiceHelper.Role.ExecuteDispose(s => s.GetRole(id)).Map<Role, RoleModel>();

            model.Permissions = LoadRolePermissions(id);

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_roleInfo", model)
            });
        }

        public ActionResult Save(RoleModel model)
        {
            if (model.IsNew && !CanAdd)
            {
                return GetAddDeniedResult();
            }

            if (!model.IsNew && !CanUpdate)
            {
                return GetUpdateDeniedResult();
            }

            if (!ModelState.IsValid) return JsonObject(false, GetModelStateErrors());

            var role = model.Map<RoleModel, Role>();
            role.Name = role.Name.ToStr().Trim();

            if (role.IsNew)
            {
                role.InitId();
            }

            var result = ServiceHelper.Role.ExecuteDispose(s => s.SaveRole(new SaveRequest
            {
                Entity = role
            }));

            if (!result.Success)
            {
                var msg = result.Messages.FirstOrDefault();
                return JsonObject(false, string.IsNullOrWhiteSpace(msg)
                    ? BackendMessage.ErrorOccure : msg.GetServiceMessageRes());
            }

            SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);

            return JsonObject(true, string.Empty);
        }

        [Delete]
        public ActionResult Delete(Guid id)
        {
            var response = ServiceHelper.Role.ExecuteDispose(s => s.DeleteRole(id));
            if (response.Success)
            {
                SendNotification(NotifyType.Success, BackendMessage.DeleteSuccessfull);
                return JsonObject(true, string.Empty);
            }

            var msg = response.Messages.FirstOrDefault();
            return JsonObject(false, string.IsNullOrWhiteSpace(msg)
                ? BackendMessage.ErrorOccure : msg.GetServiceMessageRes());
        }

        #region private

        private List<RolePermissionModel> LoadRolePermissions(Guid roleId)
        {
            var listPermissions = ServiceHelper.Role.ExecuteDispose(s => s.GetPermissions(roleId));

            var pages = typeof(BePage).EnumToList();

            var list = (from page in pages
                        join permission in listPermissions on page.Value equals
                            permission.PageId into p
                        from per in p.DefaultIfEmpty()
                        where 
                        //page.Value != (short)BePage.Roles
                        //&& page.Value != (short)BePage.Appsettings
                        //&& page.Value != (short)BePage.BackendUsers
                        page.Value != (short)BePage.Home
                        && page.Value != (short)BePage.Profile
                        select new RolePermissionModel
                        {
                            Id = per != null ? per.Id : Guid.Empty,
                            RoleId = roleId,
                            PageId = page.Value,
                            CanView = per != null && per.CanView,
                            CanAdd = per != null && per.CanAdd,
                            CanUpdate = per != null && per.CanUpdate,
                            CanDelete = per != null && per.CanDelete,
                            CanPrint = per != null && per.CanPrint,
                            CanExportExcel = per != null && per.CanExportExcel,
                            CanImportExcel = per != null && per.CanImportExcel,
                            PageName = page.Name
                        }).OrderBy(p => p.PageName).ToList();

            return list;
        }
       
        #endregion
    }
}