using System.Collections;
using System.Collections.Generic;
using ML.Common;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Data.DataContract;

namespace Share.Helper.Extension
{
	public static class UrlHelperExtensions
	{
        public static bool ImageIsExist(this UrlHelper urlHelper, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            var fullPath = urlHelper.RequestContext.HttpContext.Server.MapPath(Path.Combine(ConstKeys.DataImageFolder, fileName));

            return File.Exists(fullPath);
        }

        public static string ImageLink(this UrlHelper urlHelper, string fileName)
        {
            if (!urlHelper.ImageIsExist(fileName)) return string.Empty;
            var imagePath = Path.Combine(ConstKeys.DataImageFolder, fileName);
            return urlHelper.Content(imagePath);
        }

	    public static string ImageThumbLink(this UrlHelper urlHelper, string fileName, int size = 0)
	    {
            var thumbFileName = ConstKeys.ImageThumbPrefix + fileName;

            if (!urlHelper.ImageIsExist(thumbFileName))
	        {
                var imgSourcePath = urlHelper.RequestContext.HttpContext.Server.MapPath(Path.Combine(ConstKeys.DataImageFolder, fileName));
                var thumbImgSourcePath = urlHelper.RequestContext.HttpContext.Server.MapPath(Path.Combine(ConstKeys.DataImageFolder, thumbFileName));

	            if (size > 0)
	            {
	                SiteUtils.CreateThumbImage(imgSourcePath, thumbImgSourcePath, size);
	            }
	            else
	            {
                    SiteUtils.CreateThumbImage(imgSourcePath, thumbImgSourcePath);
	            }
	        }

	        return ImageLink(urlHelper, thumbFileName);
	    }

        public static string ImageThumbSmallLink(this UrlHelper urlHelper, string fileName, int size = 0)
        {
            var thumbFileName = ConstKeys.ImageThumbSmallPrefix + fileName;

            if (!urlHelper.ImageIsExist(thumbFileName))
            {
                var imgSourcePath = urlHelper.RequestContext.HttpContext.Server.MapPath(Path.Combine(ConstKeys.DataImageFolder, fileName));
                var thumbImgSourcePath = urlHelper.RequestContext.HttpContext.Server.MapPath(Path.Combine(ConstKeys.DataImageFolder, thumbFileName));
                SiteUtils.CreateThumbImage(imgSourcePath, thumbImgSourcePath, size > 0 ? size : 80);
            }

            return ImageLink(urlHelper, thumbFileName);
        }

	    public static string FacebookShare(this UrlHelper urlHelper, string action, string controller, object param)
	    {
	        if (string.IsNullOrWhiteSpace(controller))
	        {
                controller = urlHelper.RequestContext.RouteData.Values["controller"].ToStr();
	        }

            return urlHelper.FacebookShareUrl(urlHelper.Action(action, controller, param, urlHelper.RequestContext.HttpContext.Request.Url.Scheme));
	    }

        public static string FacebookShareUrl(this UrlHelper urlHelper, string url)
        {
            var script = "FB.ui({method: 'share',href: '" + url + "',}, function(response){})";
            return script;
        }

	    public static string FacebookShare(this UrlHelper urlHelper, string action, object param)
	    {
	        return FacebookShare(urlHelper, action, null, param);
	    }

        public static string TwitterShare(this UrlHelper urlHelper, string action, string controller, object param)
        {
            if (string.IsNullOrWhiteSpace(controller))
            {
                controller = urlHelper.RequestContext.RouteData.Values["controller"].ToStr();
            }

            return urlHelper.TwitterShareUrl(urlHelper.Action(action, controller, param, urlHelper.RequestContext.HttpContext.Request.Url.Scheme));
        }

        public static string TwitterShareUrl(this UrlHelper urlHelper, string url)
        {
            var script = "shareOnTwitter('" + url + "')";

            return script;
        }

        public static string TwitterShare(this UrlHelper urlHelper, string action, object param)
        {
            return TwitterShare(urlHelper, action, null, param);
        }

        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
	}
}