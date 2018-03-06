using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace SiteMonitor.ViewHelpers {
	public static class Controls {

		public static IHtmlString ImageActionLink(this AjaxHelper ajaxHelper, string linkText, string title, string actionName, string controllerName, AjaxOptions ajaxOptions, string linkClass, string iconClass, object routeValues = null,  object htmlAttributes = null) {
			
			var builderI = new TagBuilder("i");
			builderI.MergeAttribute("class", iconClass);
			string iTag = builderI.ToString(TagRenderMode.Normal);

			string spanTag = "";
			if (!string.IsNullOrEmpty(linkText)) {
				var builderSpan = new TagBuilder("span") { InnerHtml = " " + linkText };
				spanTag = builderSpan.ToString(TagRenderMode.Normal);
			}

			//Create the "a" tag that wraps
			var builderA = new TagBuilder("a");

			var requestContext = HttpContext.Current.Request.RequestContext;
			var uh = new UrlHelper(requestContext);
			if (string.IsNullOrWhiteSpace(controllerName)) {
				builderA.MergeAttribute("href", uh.Action(actionName, routeValues));
			}
			else {
				builderA.MergeAttribute("href", uh.Action(actionName, controllerName, routeValues));
			}
			builderA.MergeAttribute("title", title);
			builderA.MergeAttribute("class", linkClass);
			builderA.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
			builderA.MergeAttributes((ajaxOptions).ToUnobtrusiveHtmlAttributes());
			builderA.InnerHtml = iTag + spanTag;

			return new MvcHtmlString(builderA.ToString(TagRenderMode.Normal));
		}
	}
}