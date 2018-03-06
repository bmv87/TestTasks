using SiteMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;


namespace SiteMonitor.Controllers
{
	[Authorize]
	public class AccountController : Controller
    {
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		public AccountController() {
		}

		public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
			UserManager = userManager;
			SignInManager = signInManager;
		}

		public ApplicationSignInManager SignInManager {
			get {
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set {
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager {
			get {
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set {
				_userManager = value;
			}
		}

		private IAuthenticationManager AuthenticationManager {
			get {
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl) {
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
			if (!ModelState.IsValid) {
				return View(model);
			}

			// Сбои при входе не приводят к блокированию учетной записи
			// Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true
			var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
			switch (result) {
				case SignInStatus.Success:
					return RedirectToLocal(returnUrl);
				case SignInStatus.LockedOut:
					return RedirectToLocal(returnUrl);
				case SignInStatus.RequiresVerification:
					return RedirectToLocal(returnUrl);
				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Неудачная попытка входа.");
					return View(model);
			}
		}
		// GET: /Account/Register
		[AllowAnonymous]
		public ActionResult Register() {
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model) {
			if (ModelState.IsValid) {
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Hometown = model.Hometown };
				var result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded) {
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
					return RedirectToAction("Index", "Home");
				}
				AddErrors(result);
			}

			// Появление этого сообщения означает наличие ошибки; повторное отображение формы
			return View(model);
		}
		// POST: /Account/LogOff
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff() {
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home");
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (_userManager != null) {
					_userManager.Dispose();
					_userManager = null;
				}
				if (_signInManager != null) {
					_signInManager.Dispose();
					_signInManager = null;
				}
			}

			base.Dispose(disposing);
		}

		private ActionResult RedirectToLocal(string returnUrl) {
			if (Url.IsLocalUrl(returnUrl)) {
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		private void AddErrors(IdentityResult result) {
			foreach (var error in result.Errors) {
				ModelState.AddModelError("", error);
			}
		}
	}
}