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
using WLN.Test.Project.Logic.Membership;
using WLN.Test.Project.Model.Exceptions;

namespace WLN.Test.Project.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
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
                try
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
                        if (String.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
                catch (ServiceException ex)
                {
                    ModelState.AddModelError("", ex.Message);
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
                try
                {
                    if (model.SetAdmin)
                    {
                        _membershipService.RegisterUser(model.UserName, model.Password, "Administrator");
                    }
                    else _membershipService.RegisterUser(model.UserName, model.Password);
                }
                catch (MembershipServiceException ex)
                {
                    if (ex.Error == MembershipError.UserIsAlreadyRegistered)
                    {
                        ModelState.AddModelError("", "Such user already exists");
                        return View(model);
                    }
                    if (ex.Error == MembershipError.UnknownError)
                    {
                        ModelState.AddModelError("", "Unknown Error");
                        return View(model);
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            var authCookie = new HttpCookie("asdf");
            authCookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Response.Cookies.Add(authCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}