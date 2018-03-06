using SiteMonitor.DB;
using SiteMonitor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace SiteMonitor.Services {
	public class MonitoringService : IDisposable {
		private MonitorDBContext _dbContext;
		
		public MonitoringService() {
			_dbContext = new MonitorDBContext();
		}

		public async Task<List<SiteInfo>> GetSiteInfo() {
			var list = await _dbContext.Sites.ToListAsync<SiteInfo>();
			return list;
		}

		public async Task<List<SiteInfo>> CheckSites() {
			var list = await _dbContext.Sites.ToListAsync<SiteInfo>();
			Ping ping = new Ping();
			PingReply pingReply = null;
			return await Task.Run(() => {
				foreach (var site in list) {
					pingReply = ping.Send(site.Url);
					site.Address = pingReply.Address.ToString(); //IP
					site.Status = pingReply.Status.ToString(); //Статус
					site.RoundtripTime = pingReply.RoundtripTime; //Время ответа
					_dbContext.Entry(site).State = EntityState.Modified;
				}
				_dbContext.SaveChanges();
				return list;
			});
		}

		public async Task<List<SiteInfo>> AddSite(SiteInfo siteInfo) {
			_dbContext.Sites.Add(siteInfo);
			await _dbContext.SaveChangesAsync();
			return await _dbContext.Sites.ToListAsync();
		}
		public async Task<List<SiteInfo>> DeleteSite(int id) {
			var si = _dbContext.Sites.Find(id);
			_dbContext.Sites.Remove(si);
			await _dbContext.SaveChangesAsync();
			return await _dbContext.Sites.ToListAsync();
		}

		public void Dispose() {
			_dbContext.Dispose();
			_dbContext = null;
		}
	}
}