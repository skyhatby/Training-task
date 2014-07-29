using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WLN.Test.Project.Logic.Membership.Identity;
using WLN.Test.Project.Logic.Membership.Intarfaces;
using WLN.Test.Project.Web.Models;

namespace WLN.Test.Project.Web.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AccountController : Controller
    {
        private IMembershipService _membershipService;

        public AccountController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _membershipService.GetUserByName(model.UserName);

                if (user != null && user.VerifyPassword(model.Password))
                {
                    var ui = new UserInfo { UserId = user.Id };
                    var t = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddHours(1),
                        model.RememberMe, ui.ToString());
                    var s = FormsAuthentication.Encrypt(t);
                    var c = new HttpCookie("asdf", s);
                    Response.Cookies.Add(c);
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _membershipService.RegisterUser(model.UserName, model.Password);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}