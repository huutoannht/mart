using Site.Backend.Library.Session;
using Data.DataContract;
using Share.Helper;
using Share.Helper.Cache;
using Share.Helper.Client;
using ML.Common.Log;
using System;
using System.Web;
using System.Web.Security;

namespace Site.Backend.Library.Helper.Implements
{
	public class AuthenHelper : IAuthenHelper
	{
		private readonly ILogger _log = LogManager.GetLogger();

		private readonly HttpContext _httpContext;

		private readonly ISessionHelper _sessionHelper;

		private readonly ICryptoHelper _cryptoHelper;

		private readonly IServiceHelper _serviceHelper;

		private readonly ICacheHelper _cacheHelper;

		public AuthenHelper(ISessionHelper sessionHelper, ICryptoHelper cryptoHelper, IServiceHelper serviceHelper, ICacheHelper cacheHelper)
		{
			_httpContext = HttpContext.Current;
			_sessionHelper = sessionHelper;
			_cryptoHelper = cryptoHelper;
			_serviceHelper = serviceHelper;
			_cacheHelper = cacheHelper;
		}

		public bool IsAuthenticated
		{
			get { return _httpContext.User.Identity.IsAuthenticated; }
		}

		public bool SessionIsAlive
		{
			get { return _sessionHelper.CurrentUserId != Guid.Empty; }
		}

		public void Logout()
		{
			FormsAuthentication.SignOut();

		    var userId = _sessionHelper.Get(SessionKey.CurrentUserId) as Guid?;

            if (userId != null && userId.Value != Guid.Empty)
		    {
                _cacheHelper.ClearGetBeUser(userId.Value);
		    }

			_sessionHelper.Abandon();

			if (_httpContext != null && _httpContext.Session != null)
			{
				_httpContext.Session.Clear();
				_httpContext.Session.Abandon();
			}
		}

        public bool CanAccessArea(BeUserType beUserType)
        {
            return beUserType == _sessionHelper.CurrentUser.Type;
        }
	}	
}