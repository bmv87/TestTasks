using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Quartz;
using SiteMonitor.Models;
using SiteMonitor.Hubs;
using SiteMonitor.Services;

namespace SiteMonitor.Jobs {
	public class Monitor : IJob {
		public async Task Execute(IJobExecutionContext context) {
			List<SiteInfo> list = new List<SiteInfo>();
			using(var service = new MonitoringService()) {
				list = await service.CheckSites();
			}
			//TO DO: some action
			var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
			// отправляем сообщение
			hubContext.Clients.All.displaySiteList(list);
		}
	}
}