using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sberbank.Models {
	public class Options {
		[Display(Name = "Дата 1")]
		public DateTime DateBegin { get; set; }
		[Display(Name = "Дата 2")]
		public DateTime DateEnd { get; set; }
		[Display(Name = "Параметр 1")]
		public bool Param1 { get; set; }
		[Display(Name = "Параметр 2")]
		public bool Param2 { get; set; }
		[Display(Name = "Параметр 3")]
		public bool Param3 { get; set; }
	}
}