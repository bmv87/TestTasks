using Sberbank.DB;
using Sberbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;


namespace Sberbank.Controllers {

	public class DataApiController : ApiController {


		[HttpPost]
		[ActionName("GetReport1")]
		[Route("api/DataApi/GetReport1")]
		public IHttpActionResult GetReport1([FromBody]Options options) {
			List<Report> list = new List<Report>();
			using (var dataContext = new ADODBContext()) {
				list = dataContext.GetReportList(options);
			}
			return Ok(list);
		}

		[HttpPost]
		[ActionName("GetReport2")]
		[Route("api/DataApi/GetReport2")]
		public IHttpActionResult GetReport2([FromBody]Options options) {
			List<Report> list;
			using (var dataContext = new EFDBContext()) {
				list = dataContext.Reports.Where(item => item.Date >= options.DateBegin
											 && item.Date <= options.DateEnd
											 && (item.Param1 == options.Param1
											 || item.Param2 == item.Param2
											 || item.Param3 == options.Param3))
											   .Select(rep => new Report {
												   Key = rep.Id,
												   Field1 = rep.Field1,
												   Field2 = rep.Field2,
												   Field3 = rep.Field3,
												   Field4 = rep.Field4,
												   Field5 = rep.Field5
											   }).ToList();
			}
			return Ok(list);
		}

		[HttpPost]
		[ActionName("GetReport3")]
		[Route("api/DataApi/GetReport3")]
		public IHttpActionResult GetReport3() {
			List<Report> list;
			using (var dataContext = new EFDBContext()) {

				list = dataContext.Reports
							.Select(rep => new Report {
								Key = rep.Id,
								Field1 = rep.Field1,
								Field2 = rep.Field2,
								Field3 = rep.Field3,
								Field4 = rep.Field4,
								Field5 = rep.Field5
							}).ToList();
			}
			return Ok(list);
		}
	}
}