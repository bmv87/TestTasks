using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sberbank.Models {
	public class Report {
		[Display(Name = "Ключ 1")]
		public int Key { get; set; }
		[Display(Name = "Поле 1")]
		public string Field1 { get; set; }
		[Display(Name = "Поле 2")]
		public string Field2 { get; set; }
		[Display(Name = "Поле 3")]
		public string Field3 { get; set; }
		[Display(Name = "Поле 4")]
		public string Field4 { get; set; }
		[Display(Name = "Поле 5")]
		public string Field5 { get; set; }
		
	}
}