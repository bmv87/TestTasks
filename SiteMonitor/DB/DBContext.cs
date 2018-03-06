using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SiteMonitor.Models;

namespace SiteMonitor.DB {
	public class MonitorDBContext : DbContext {
		public DbSet<Option> Options { get; set; }
		public DbSet<SiteInfo> Sites { get; set; }
	}
}