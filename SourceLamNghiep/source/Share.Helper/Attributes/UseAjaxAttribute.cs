using System;
using System.Web.Mvc;

namespace Share.Helper.Attributes
{
	/// <summary>
	/// Use in the case: actions support ajax, non-ajax
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class UseAjaxAttribute : ActionMethodSelectorAttribute
	{
		public readonly bool IsAjax ;

		public UseAjaxAttribute(bool useAjax)
		{
			IsAjax = useAjax;
		}

		public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
		{
			var ajaxRequest = controllerContext.HttpContext.Request.IsAjaxRequest();

			return IsAjax == ajaxRequest;
		}
	}
}