using Sberbank.DB;
using Sberbank.Entity;
using Sberbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sberbank.Controllers {
	public class HomeController : Controller {
		// GET: Home
		public ActionResult Index() {
			var model = new Options();
			return View("~/Views/Home/Index.cshtml", model);
		}

		public ActionResult CreateReport() {
			using (var dataContext = new EFDBContext()) {
				for (int i = 0; i < 15; i++) {
					var n = 0;
					var rep = new ReportInfo() {
						Date = DateTime.Now.AddMonths(i),
						Field1 = "Поле" + (n + 1).ToString() + "-" + i,
						Field2 = "Поле" + (n + 1).ToString() + "-" + i,
						Field3 = "Поле" + (n + 1).ToString() + "-" + i,
						Field4 = "Поле" + (n + 1).ToString() + "-" + i,
						Field5 = "Поле" + (n + 1).ToString() + "-" + i,
					};
					if (i % 3 == 0) {
						rep.Param1 = false;
						rep.Param2 = false;
						rep.Param3 = true;
					}else if (i % 2 == 0) {
						rep.Param1 = false;
						rep.Param2 = true;
						rep.Param3 = false;
					}
					else {
						rep.Param1 = false;
						rep.Param2 = false;
						rep.Param3 = true;
					}
					dataContext.Reports.Add(rep);
				}
				dataContext.SaveChanges();
			}
			return Content("OK");
		}
	}
}