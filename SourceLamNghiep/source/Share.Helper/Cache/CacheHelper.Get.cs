using System;
using System.Collections.Generic;
using System.Linq;
using Data.DataContract.CategoryDC;
using Data.DataContract.CustomerDC;
using Data.DataContract.ProductDC;
using ML.Common;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Data.DataContract.BeUserDC;
using Data.DataContract.HtmlContentDC;
using Data.DataContract.RoleDC;
using Data.DataContract.SystemEmailTemplateDC;
using Share.Helper.Models;

namespace Share.Helper.Cache
{
    public partial class CacheHelper
    {
        public Customer GetCustomer(Guid userId)
        {
            var key = FormatKey(CacheKey.GetCustomer, userId);
            var value = GetOrSet(key, _serviceHelper.Customer, s => s.GetCustomer(userId));

            return value as Customer;
        }

        public Guid GetToken(Guid id)
        {
            var key = FormatKey(CacheKey.GetToken, id);
            var value = _cache.Get(key);
            if (value == null)
            {
                return Guid.Empty;
            }

            ClearGetToken(id);

            return value.ToGuid();
        }

        public void SetToken(Guid id, Guid value)
        {
            var key = FormatKey(CacheKey.GetToken, id);
            var valueOld = _cache.Get(key);
            if (valueOld != null)
            {
                ClearGetToken(id);
            }

            _cache.Add(key, value, Cache1Day);
        }

        public List<Category> GetCategories()
        {
            var key = FormatKey(CacheKey.GetCategories);

            var cacheValue = GetOrSet(key, _serviceHelper.Category, service => service.GetAllCategory());

            return (List<Category>)cacheValue ?? new List<Category>();
        }

        public List<Category> GetCategories(CategoryType categoryType)
        {
            return GetCategories().Where(i => i.CategoryType == categoryType).ToList();
        }

        public Category GetCategory(Guid id)
        {
            return GetCategories().FirstOrDefault(i => i.Id == id);
        }

        public List<Category> GetCategoryTreeList()
        {
            var key = FormatKey(CacheKey.GetCategoryTreeList);

            var cacheValue = GetOrSet(key, () => SiteUtils.BuildCategoryTreeFromList());

            return cacheValue ?? new List<Category>();
        }

        public List<Category> GetCategoryTreeList(CategoryType categoryType)
        {
            return GetCategoryTreeList().Where(i => i.CategoryType == categoryType).ToList();
        }

        public List<ChatUserModel> GetChatAdminGroup()
        {
            var key = FormatKey(CacheKey.GetChatAdminGroup);

            var obj = _cache.Get(key);
            if (obj == null) return new List<ChatUserModel>();

            return obj as List<ChatUserModel> ?? new List<ChatUserModel>();
        }

        public void SetChatAdminGroup(List<ChatUserModel> value)
        {
            var key = FormatKey(CacheKey.GetChatAdminGroup);
            var obj = _cache.Get(key);
            if (obj != null)
            {
                _cache.Remove(key);
            }

            _cache.Add(key, value, Cache1Day);
        }

        public List<ChatUserModel> GetChatUserGroup()
        {
            var key = FormatKey(CacheKey.GetChatUserGroup);

            var obj = _cache.Get(key);
            if (obj == null) return new List<ChatUserModel>();

            return obj as List<ChatUserModel> ?? new List<ChatUserModel>();
        }

        public void SetChatUserGroup(List<ChatUserModel> value)
        {
            var key = FormatKey(CacheKey.GetChatUserGroup);
            var obj = _cache.Get(key);
            if (obj != null)
            {
                _cache.Remove(key);
            }

            _cache.Add(key, value, Cache1Day);
        }

        public List<Product> GetProducts()
        {
            var key = FormatKey(CacheKey.GetProducts);

            var cacheValue = GetOrSet(key, _serviceHelper.Product, service => service.GetAll());

            return (List<Product>)cacheValue ?? new List<Product>();
        }

        public List<HtmlContent> GetHtmlContents()
        {
            var key = FormatKey(CacheKey.GetHtmlContents);

            var cacheValue = GetOrSet(key, _serviceHelper.HtmlContent, service => service.GetAll());

            return (List<HtmlContent>)cacheValue ?? new List<HtmlContent>();
        }

        public List<BeUser> GetAllBeUsers()
        {
            var key = FormatKey(CacheKey.GetAllBeUsers);

            var cacheValue = GetOrSet(key, _serviceHelper.BeUser, service => service.GetAll());

            return (List<BeUser>)cacheValue ?? new List<BeUser>();
        }

        public BeUser GetBeUser(Guid id)
        {
            var key = FormatKey(CacheKey.GetBeUser, id);
            var value = GetOrSet(key, _serviceHelper.BeUser, s => s.GetBeUser(id));

            return value as BeUser;
        }

        public Role GetRole(Guid id, bool throwErrorIfNotFound = true)
        {
            var key = FormatKey(CacheKey.GetRole, id);
            var value = GetOrSet(key, _serviceHelper.Role, s =>
            {
                var role = s.GetRole(id);

                role.Permissions = s.GetPermissions(id);

                return role;
            });

            if (value == null && throwErrorIfNotFound)
            {
                throw new Exception("Unable to find this role: " + id);
            }

            return value as Role;
        }

        public List<Role> GetRoles()
        {
            var key = FormatKey(CacheKey.GetRoles);

            var cacheValue = GetOrSet(key, _serviceHelper.Role, service => service.GetAll());

            return cacheValue as List<Role> ?? new List<Role>();
        }

        public List<RolePermission> GetRolePermissions(Guid roleId)
        {
            var key = FormatKey(CacheKey.GetRolePermissions, roleId);

            var cacheValue = GetOrSet(key, _serviceHelper.Role, service => service.GetPermissions(roleId));

            return cacheValue as List<RolePermission> ?? new List<RolePermission>();
        }

        public List<AppSetting> GetAppSettings(AppSettingType settingType)
        {
            var key = FormatKey(CacheKey.GetAppSettings, settingType);

            var appRequest = new Data.DataContract.AppSettingDC.FindRequest { SettingType = settingType };
            var cacheValue = GetOrSet(key, _serviceHelper.AppSetting, service => service.FindAppSettings(appRequest).Results);

            return cacheValue as List<AppSetting> ?? new List<AppSetting>();
        }

        public List<SystemEmailTemplate> GetSystemEmailTemplates()
        {
            var key = FormatKey(CacheKey.GetSystemEmailTemplates);

            var cacheValue = GetOrSet(key, _serviceHelper.SystemEmailTemplate, service => service.GetAll());

            return (List<SystemEmailTemplate>)cacheValue ?? new List<SystemEmailTemplate>();
        }
    }
}
