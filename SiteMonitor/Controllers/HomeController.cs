using SiteMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteMonitor.Controllers
{
    public class HomeController : Controller
    {
		readonly MonitoringService _monitoringService;
		public HomeController() {
			_monitoringService = new MonitoringService();
		}
        // GET: Home
        public ActionResult Index()
        {
			return View();
        }

		public ActionResult GetSitesList() {
			try {
				var list = Task.Run(async () => { return await _monitoringService.GetSiteInfo(); }).Result;
				 
				ViewBag.IsAdminPanel = false;
				return PartialView("Table", list);
			}
			catch (Exception ex) {
				return PartialView("Error", ex.Message);
			}
		}
	}
}