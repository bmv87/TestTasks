using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace SiteMonitor {
	public class BundleConfig {
		// Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
				"~/Scripts/jquery.unobtrusive-ajax.js"));
			bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
				"~/Scripts/jquery.signalR-2.2.2.js"));
			//bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
			//	"~/Scripts/knockout-{version}.js",
			//	"~/Scripts/knockout.validation.js"));

			bundles.Add(new ScriptBundle("~/bundles/app").Include(
						"~/Scripts/app/app.js"
				));

			//bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
			//	"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
				//"~/Scripts/jquery-3.0.0.slim.js",
				"~/Scripts/popper.js",
				"~/Scripts/popper-utils.js",
				"~/Scripts/bootstrap.js"
			));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				 "~/Content/bootstrap.css",
				 "~/Content/bootstrap-reboot.css",
				 "~/Content/bootstrap-grid.css",
				 "~/Content/fontawesome-all.css",
				 "~/Content/app/app.css"));
		}
	}
}
