using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SiteMonitor.Models;

namespace SiteMonitor.Hubs {
	public class NotificationHub : Hub {
		public void SendStatus(List<SiteInfo> list) {
			Clients.All.onSiteStatus(list);
		}

		public override Task OnConnected() {
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled) {
			return base.OnDisconnected(stopCalled);
		}

		public override Task OnReconnected() {
			return base.OnReconnected();
		}
	}
}