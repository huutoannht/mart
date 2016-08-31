using System.Linq;
using Site.Backend.Library.Helper;
using Site.Backend.Library.Session;
using Share.Helper;
using Share.Helper.Cache;
using Share.Helper.Client;
using ML.Common;
using StructureMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Site.Backend.Library
{
    public static class Common
    {
        #region Object

        public static T GetHelper<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }

        public static IServiceHelper ServiceHelper { get { return GetHelper<IServiceHelper>(); } }

        public static ISessionHelper SessionHelper { get { return GetHelper<ISessionHelper>(); } }

        public static ICacheHelper CacheHelper { get { return GetHelper<ICacheHelper>(); } }

        public static IAuthenHelper AuthenHelper { get { return GetHelper<IAuthenHelper>(); } }

        public static ICryptoHelper CryptoHelper { get { return GetHelper<ICryptoHelper>(); } }

        #endregion
    }
}