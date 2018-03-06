using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace SiteMonitor.Jobs {
	public class SchedulerService {

		public static async void Start() {
			IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
			await scheduler.Start();

			IJobDetail job = JobBuilder.Create<Monitor>().Build();

			ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
				.WithIdentity("monitorTrigger", "mGroup")     // идентифицируем триггер с именем и группой
				.StartNow()                            // запуск сразу после начала выполнения
				.WithSimpleSchedule(x => x            // настраиваем выполнение действия
					.WithIntervalInMinutes(GetJobInterval())          // через 1 минуту
					.RepeatForever())                   // бесконечное повторение
				.Build();                               // создаем триггер
			
			await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
		}

		private static int GetJobInterval() {
			try {
				var interval = System.Configuration.ConfigurationManager.AppSettings["MonitorInterval"];
				return Convert.ToInt32(interval);
			}
			catch  {
				return 5;
			}
			
		}

	}
}