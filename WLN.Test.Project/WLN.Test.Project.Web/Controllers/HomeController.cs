using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WLN.Test.Project.Logic.FileSystem.Interfaces;
using WLN.Test.Project.Logic.Membership.Identity;
using WLN.Test.Project.Logic.Membership.Intarfaces;
using WLN.Test.Project.Web.Models;

namespace WLN.Test.Project.Web.Controllers
{
    public class HomeController : Controller
    {
        private IFileSystemService _fileSystemService;
        private IMembershipService _membershipService;

        public HomeController(IFileSystemService fileSystemService, IMembershipService membershipService)
        {
            _fileSystemService = fileSystemService;
            _membershipService = membershipService;
        }

        public ActionResult Index(string path = "")
        {
            var drives = _fileSystemService.GetDrives();
            ViewBag.CurrentDirectory = path;
            return View(drives);
        }

        public PartialViewResult GetListOfDirectoriesPartial(string path)
        {
            if (!String.IsNullOrEmpty(path)) 
            { 
            var model = _fileSystemService.GetAllFoldersFromDirectory(path);
            var parentFolderAddress = _fileSystemService.GetDirectoryByPath(path).Parent;
            ViewBag.Parent = parentFolderAddress != null ? parentFolderAddress.FullName : "";
            return PartialView(model);
            }
            return null;
        }

        public PartialViewResult GetListOfFilesPartial(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                return PartialView(_fileSystemService.GetAllFilesFromDirectory(path));
            }
            return null;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }
    }
}