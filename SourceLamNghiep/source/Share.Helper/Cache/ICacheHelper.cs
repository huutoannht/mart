using System;
using System.Collections.Generic;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Data.DataContract.BeUserDC;
using Data.DataContract.CategoryDC;
using Data.DataContract.CustomerDC;
using Data.DataContract.ProductDC;
using Data.DataContract.RoleDC;
using Data.DataContract.SystemEmailTemplateDC;
using Data.DataContract.HtmlContentDC;
using Share.Helper.Models;

namespace Share.Helper.Cache
{
    public interface ICacheHelper
    {
        string PrefixKey { get; }

        #region BeUser

        List<BeUser> GetAllBeUsers();

        BeUser GetBeUser(Guid id);

        void ClearGetBeUser(Guid id);

        #endregion

        #region BeRole

        Role GetRole(Guid id, bool throwErrorIfNotFound = true);

        void ClearGetRole(Guid id);

        List<Role> GetRoles();

        void ClearGetRoles();

        List<RolePermission> GetRolePermissions(Guid roleId);

        void ClearGetRolePermissions(Guid roleId);

        #endregion

        #region AppSettings

        List<AppSetting> GetAppSettings(AppSettingType settingType);

        void ClearGetAppSettings(AppSettingType settingType);

        #endregion

        #region System Email Template

        List<SystemEmailTemplate> GetSystemEmailTemplates();

        void ClearGetSystemEmailTemplates();

        #endregion

        #region Html contents

        List<HtmlContent> GetHtmlContents();

        void ClearGetHtmlContents();

        #endregion

        #region Customers

        Customer GetCustomer(Guid userId);

        void ClearGetCustomer(Guid userId);

        #endregion

        #region token

        Guid GetToken(Guid id);

        void SetToken(Guid id, Guid value);

        void ClearGetToken(Guid id);

        #endregion

        #region category

        List<Category> GetCategories();

        List<Category> GetCategories(CategoryType categoryType);

        Category GetCategory(Guid id);

        void ClearGetCategories();

        List<Category> GetCategoryTreeList();

        List<Category> GetCategoryTreeList(CategoryType categoryType);

        #endregion

        #region chat

        List<ChatUserModel> GetChatAdminGroup();

        void SetChatAdminGroup(List<ChatUserModel> value);

        List<ChatUserModel> GetChatUserGroup();

        void SetChatUserGroup(List<ChatUserModel> value);

        #endregion

        #region product

        List<Product> GetProducts();

        void ClearGetProducts();

        #endregion
    }
}
