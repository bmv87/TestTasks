using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sberbank.Entity {
	public class ReportInfo {
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Field1 { get; set; }
		public string Field2 { get; set; }
		public string Field3 { get; set; }
		public string Field4 { get; set; }
		public string Field5 { get; set; }
		public bool Param1 { get; set; }
		public bool Param2 { get; set; }
		public bool Param3 { get; set; }
	}
}