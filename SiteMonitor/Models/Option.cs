using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteMonitor.Models {
	public class Option {
		public int Id { get; set; }
		[Display(Name = "Интервал (мин)")]
		public int JobInterval { get; set; }
	}
}