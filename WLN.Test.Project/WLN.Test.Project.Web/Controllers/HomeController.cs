using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WLN.Test.Project.Logic.FileSystem.Interfaces;
using WLN.Test.Project.Logic.Membership.Identity;
using WLN.Test.Project.Logic.Membership.Intarfaces;
using WLN.Test.Project.Logic.FileSystem;
using WLN.Test.Project.Web.Models;

namespace WLN.Test.Project.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private IFileSystemService _fileSystemService;

        public HomeController(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
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

        public ActionResult DeleteFolder(string path)
        {
            _fileSystemService.DeleteDirectory(path);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFile(string path)
        {
            _fileSystemService.DeleteFile(path);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateFileOrFolder(string path)
        {
            var model = new CreateViewModel() { Name = path };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFileOrFolder(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File)
                    {
                        _fileSystemService.CreateFile(model.Name);
                    }
                    else
                    {
                        if (model.Directory)
                        {
                            _fileSystemService.CreateDirectory(model.Name);
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (FileSystemServiceException ex)
                {
                    if (ex.Error == FileSystemError.IncorrectPath)
                        ModelState.AddModelError("","Incorrect Path");
                    if (ex.Error == FileSystemError.YouHaventAccessToTheResource)
                        ModelState.AddModelError("", "You Havent Access To The Resource");
                }
            }
            return View(model);
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
    }
}