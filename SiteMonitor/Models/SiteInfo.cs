using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace SiteMonitor.Models {
	public class SiteInfo {

		public int Id { get; set; }
		[Display(Name = "Наименование")]
		public string Name { get; set; }
		[Display(Name = "Хост")]
		public string Url { get; set; }
		[Display(Name = "Адрес")]
		public string Address { get;  set; }
		[Display(Name = "Статус")]
		public string Status { get;  set; }
		[Display(Name = "Время ответа")]
		public long RoundtripTime { get;  set; }
	}
}