using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Extensions.Options;
using Sberbank.Models;
using System.Data;

namespace Sberbank.DB {
	public class ADODBContext : IDisposable {

		SQLiteFactory _factory;

		public ADODBContext() {
			_factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");


		}

		public string ConnectionString {
			get {
				return ConfigurationManager.ConnectionStrings[1].ConnectionString;
			}
		}

		public List<Report> GetReportList(Sberbank.Models.Options options) {
			List<Report> list = new List<Report>();
			using (SQLiteConnection connection = (SQLiteConnection)_factory.CreateConnection()) {
				connection.ConnectionString = ConnectionString;
				connection.Open();
				using (SQLiteCommand command = new SQLiteCommand(connection)) {
					command.CommandText = @"SELECT Id, Field1, Field2, Field3, Field4, Field5 
					FROM Reports
					WHERE (Date BETWEEN @DateBegin AND @DateEnd) 
					AND (Param1 = @Param1
					OR Param2 = @Param2
					OR Param3 = @Param3)";
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("DateBegin", options.DateBegin);
					command.Parameters.AddWithValue("DateEnd", options.DateEnd);
					command.Parameters.AddWithValue("Param1", options.Param1);
					command.Parameters.AddWithValue("Param2", options.Param2);
					command.Parameters.AddWithValue("Param3", options.Param3);
					SQLiteDataAdapter da = new SQLiteDataAdapter(command);
					DataSet ds = new DataSet();
					da.Fill(ds);
					DataTable dt = ds.Tables[0];
					foreach (DataRow row in dt.Rows) {
						// получаем все ячейки строки
						var item = row.ItemArray;
						var rep = new Report() {
							Key = int.Parse(item[0] + ""),
							Field1 = item[1] + "",
							Field2 = item[2] + "",
							Field3 = item[3] + "",
							Field4 = item[4] + "",
							Field5 = item[5] + "",
						};
						list.Add(rep);
					}
				}
			}
			return list;
		}

		public void Dispose() {
			_factory = null;
		}
	}
}