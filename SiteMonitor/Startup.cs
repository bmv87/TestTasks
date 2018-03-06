using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SiteMonitor.Models;

[assembly: OwinStartup(typeof(SiteMonitor.Startup))]

namespace SiteMonitor {
	public class Startup {
		public OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

		public void Configuration(IAppBuilder app) {
			// Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888
			app.MapSignalR();
			// Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
			app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

			// Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход,
			app.UseCookieAuthentication(new CookieAuthenticationOptions {
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
				Provider = new CookieAuthenticationProvider {
					// Позволяет приложению проверять метку безопасности при входе пользователя.
					// Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.  
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
						validateInterval: TimeSpan.FromMinutes(20),
						regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
				},
				ExpireTimeSpan = TimeSpan.FromMinutes(20)

			});
							
		}
	}
}
