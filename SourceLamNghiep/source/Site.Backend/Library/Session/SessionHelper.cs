using System.Linq;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Data.DataContract.RoleDC;
using Share.Helper.Cache;
using Share.Messages;
using ML.Common;
using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Site.Backend.Library.Session
{
    public class SessionHelper : ISessionHelper
    {
        public string CurrentTimeZone
        {
            get
            {
                if (Get(SessionKey.CurrentTimeZone) == null)
                {
                    return null;
                }

                return Get(SessionKey.CurrentTimeZone).ToStr();
            }
            set { Set(SessionKey.CurrentTimeZone, value); }
        }

        public Guid CurrentUserId
        {
            get
            {
                var userId = Get(SessionKey.CurrentUserId);

                if (userId == null || (Guid)userId == Guid.Empty)
                {
                    FormsAuthentication.SignOut();

                    throw new Exception(BackendMessage.YouLostSession);
                }

                return (Guid)userId;
            }
            set { Set(SessionKey.CurrentUserId, value); }
        }

        public BeUser CurrentUser { get { return _cacheHelper.GetBeUser(CurrentUserId); } }

        public Role CurrentRole { 
            get 
            {
                return _cacheHelper.GetRole(CurrentUser.RoleId.ToGuid()) ;
            } 
        }

        public void Abandon()
        {
            Session.Clear();
            Session.Abandon();
        }

        #region permissions

        public bool CanView(BePage page)
        {
            if (CurrentUser.Type == BeUserType.Super) return true;
            if (CurrentRole.Permissions == null) return false;
            if (IsExceptionPage(page)) return true;

            var pageId = (short)page;
            var permission = CurrentRole.Permissions.FirstOrDefault(p => p.PageId == pageId);
            return permission != null && permission.CanView;
        }

        public bool CanAdd(BePage page)
        {
            if (CurrentUser.Type == BeUserType.Super) return true;
            if (CurrentRole.Permissions == null) return false;
            if (IsExceptionPage(page)) return true;

            var pageId = (short)page;
            var permission = CurrentRole.Permissions.FirstOrDefault(p => p.PageId == pageId);
            return permission != null && permission.CanAdd;
        }

        public bool CanUpdate(BePage page)
        {
            if (CurrentUser.Type == BeUserType.Super) return true;
            if (CurrentRole.Permissions == null) return false;
            if (IsExceptionPage(page)) return true;

            var pageId = (short)page;
            var permission = CurrentRole.Permissions.FirstOrDefault(p => p.PageId == pageId);
            return permission != null && permission.CanUpdate;
        }

        public bool CanDelete(BePage page)
        {
            if (CurrentUser.Type == BeUserType.Super) return true;
            if (CurrentRole.Permissions == null) return false;
            if (IsExceptionPage(page)) return true;

            var pageId = (short)page;
            var permission = CurrentRole.Permissions.FirstOrDefault(p => p.PageId == pageId);
            return permission != null && permission.CanDelete;
        }

        public bool CanImport(BePage page)
        {
            if (CurrentUser.Type == BeUserType.Super) return true;
            if (CurrentRole.Permissions == null) return false;
            if (IsExceptionPage(page)) return true;

            var pageId = (short)page;
            var permission = CurrentRole.Permissions.FirstOrDefault(p => p.PageId == pageId);
            return permission != null && permission.CanImportExcel;
        }

        public bool CanExport(BePage page)
        {
            if (CurrentUser.Type == BeUserType.Super) return true;
            if (CurrentRole.Permissions == null) return false;
            if (IsExceptionPage(page)) return true;

            var pageId = (short)page;
            var permission = CurrentRole.Permissions.FirstOrDefault(p => p.PageId == pageId);
            return permission != null && permission.CanExportExcel;
        }

        private bool IsExceptionPage(BePage page)
        {
            return page == BePage.Home || page == BePage.Profile;
        }

        #endregion

        #region Constructor

        private readonly ICacheHelper _cacheHelper;

        public SessionHelper(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }

        #endregion

        #region Processing

        private HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public object Get(SessionKey key)
        {
            return Session[key.ToString()];
        }

        public void Set(SessionKey key, object source)
        {
            Session[key.ToString()] = source;
        }

        #endregion
        
    }
}