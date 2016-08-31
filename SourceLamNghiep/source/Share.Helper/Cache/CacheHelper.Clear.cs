using System;
using Data.DataContract;

namespace Share.Helper.Cache
{
	public partial class CacheHelper
	{
	    public void ClearGetCustomer(Guid userId)
	    {
            Clear(CacheKey.GetCustomer, userId);
	    }

	    public void ClearGetToken(Guid id)
	    {
            Clear(CacheKey.GetToken, id);
	    }

	    public void ClearGetCategories()
	    {
            Clear(CacheKey.GetCategories);
            Clear(CacheKey.GetCategoriesIncludeProducts);
            Clear(CacheKey.GetCategoryTreeList);
	    }

	    public void ClearGetProducts()
	    {
            Clear(CacheKey.GetProducts);
	    }

	    public void ClearGetBeUser(Guid id)
        {
            Clear(CacheKey.GetBeUser, id);
            Clear(CacheKey.GetAllBeUsers);
        }
	    public void ClearGetRole(Guid id)
	    {
	        ClearGetRoles();
            ClearGetRolePermissions(id);
            Clear(CacheKey.GetRole, id);
        }

	    public void ClearGetRoles()
	    {
            Clear(CacheKey.GetRoles);
	    }

	    public void ClearGetRolePermissions(Guid roleId)
	    {
            Clear(CacheKey.GetRolePermissions, roleId);
	    }

	    public void ClearGetAppSettings(AppSettingType settingType)
        {
            Clear(CacheKey.GetAppSettings, settingType);
        }

	    public void ClearGetSystemEmailTemplates()
	    {
            Clear(CacheKey.GetSystemEmailTemplates);
	    }

	    public void ClearGetHtmlContents()
	    {
            Clear(CacheKey.GetHtmlContents);
	    }
	}
}
