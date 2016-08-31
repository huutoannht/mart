using System;
using System.Linq;
using SharedCache.WinServiceCommon;
using Share.Helper.Client;
using ML.Common;
using ML.Utils.Cache;

namespace Share.Helper.Cache
{
	public partial class CacheHelper : ICacheHelper
	{
		#region Constructor

        private const int Cache1Day = 1440;

        private const int Cache2Day = 2880;

		private readonly ICache _cache;
		private readonly IServiceHelper _serviceHelper;

		public CacheHelper(ICache cache, IServiceHelper serviceHelper)
		{
			PrefixKey = std.AppSettings["CachePrefix"];

			_cache = cache;
			_serviceHelper = serviceHelper;
		}

		#endregion

		public string PrefixKey { get; private set; }
        
	    #region Processing

		private object GetOrSet<TService>(string key, TService service, Func<TService, object> funcService)
		{
			return GetOrSet(key, () => service.ExecuteDispose(funcService));
		}

		public T GetOrSet<T>(string key, Func<T> execSet = null, TimeSpan? timeExpired = null)
		{
		    try
		    {
		        var obj = _cache.Get(key);

		        if (obj is CacheException)
		        {
		            return default(T);
		        }

		        if (obj == null && execSet != null)
		        {
		            obj = execSet();
		            _cache.Add(key, obj, timeExpired != null ? timeExpired.Value.Minutes : Cache1Day);
		        }

		        return obj != null ? (T) obj : default(T);
		    }
		    catch
		    {
		        if (execSet != null)
		        {
                    var obj = execSet();
                    _cache.Add(key, obj, timeExpired != null ? timeExpired.Value.Minutes : Cache1Day);
                    return obj != null ? obj : default(T);
		        }
                return default(T);
		    }
		}
		
		private void Clear(CacheKey key, params object[] extendKeys)
		{
			_cache.Remove(FormatKey(key, extendKeys));
		}

		private string FormatKey(CacheKey key, params object[] extendKeys)
		{
			var extend = extendKeys.Where(k => k != null).Select(k => k.ToStr().ToLower().Replace("-", ""));
			var keyStr = string.Format("{0}_{1}_{2}", PrefixKey, key, string.Join("_", extend));
		    return keyStr;
		}

		#endregion

    }
}
