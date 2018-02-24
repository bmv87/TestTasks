using Microsoft.EntityFrameworkCore;
using Sberbank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sberbank.DB {
	public class EFDBContext : DbContext {
		private static bool _created = false;
		public EFDBContext() {
			if (!_created) {
				_created = true;
				//	Database.EnsureDeleted();
				//	Database.EnsureCreated();
			}
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder) {
			optionbuilder.UseSqlite(@"Data Source=C:\Projects\TestTasks\Sberbank\App_Data\sber1.db");
		}

		public DbSet<ReportInfo> Reports { get; set; }
	}
}