using SiteMonitor.DB;
using SiteMonitor.Models;
using SiteMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteMonitor.Controllers {
	[Authorize]
	public class AdminController : Controller {

		readonly MonitoringService _monitoringService;
		public AdminController() {
			_monitoringService = new MonitoringService();
		}
		// GET: Admin
		public ActionResult Index() {
			return View();
		}

		// GET: Admin/GetSitesList
		public ActionResult GetSitesList() {
			try {
				var list = Task.Run(async () => { return await _monitoringService.GetSiteInfo(); }).Result;
				ViewBag.IsAdminPanel = true;
				return PartialView("Table", list);
			}
			catch (Exception ex) {
				return PartialView("Error", ex.Message);
			}
		}

		[HttpGet]
		public ActionResult Create() {
			return PartialView("Form", new SiteInfo());
		}

		// POST: Admin/Create
		[HttpPost]
		public ActionResult Create(SiteInfo siteInfo) {
			try {
				var list = Task.Run(async () => { return await _monitoringService.AddSite(siteInfo); }).Result;
				ViewBag.IsAdminPanel = true;
				return PartialView("Table", list);
			}
			catch (Exception ex) {
				return PartialView("Error", ex.Message);
			}
		}
		

		// GET: Admin/Delete/5
		public ActionResult Delete(int id) {
			try {
				var list = Task.Run(async () => { return await _monitoringService.DeleteSite(id); }).Result;
				ViewBag.IsAdminPanel = true;
				return PartialView("Table", list);
			}
			catch (Exception ex) {
				return PartialView("Error", ex.Message);
			}
		}
	}
}
