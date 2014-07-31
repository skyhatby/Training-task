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
            try
            {
                var drives = _fileSystemService.GetDrives();
                ViewBag.CurrentDirectory = path;
                return View(drives);
            }
            catch (FileSystemServiceException ex)
            {
                if (ex.Error == FileSystemError.YouHaventAccessToTheResource)
                    return PartialView("Error", "You Haven't Access To The Resource");
                if (ex.Error == FileSystemError.UnknownError)
                    return View("Error", "Unknown Error");
            }
            return null;
        }

        public PartialViewResult GetListOfDirectoriesPartial(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                try
                {
                    var model = _fileSystemService.GetAllFoldersFromDirectory(path);
                    var parentFolderAddress = _fileSystemService.GetDirectoryByPath(path).Parent;
                    ViewBag.Parent = parentFolderAddress != null ? parentFolderAddress.FullName : "";
                    return PartialView(model);
                }
                catch (FileSystemServiceException ex)
                {
                    if (ex.Error == FileSystemError.YouHaventAccessToTheResource)
                        return PartialView("Error", "You Haven't Access To The Resource");
                    if (ex.Error == FileSystemError.DirectoryDoesntExist)
                        return PartialView("Error", "Directory Doesn't Exist");
                    if (ex.Error == FileSystemError.IncorrectPath)
                        return PartialView("Error", "Incorrect Path");
                }

            }
            return null;
        }

        public PartialViewResult GetListOfFilesPartial(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                try
                {
                    return PartialView(_fileSystemService.GetAllFilesFromDirectory(path));
                }
                catch (FileSystemServiceException)
                {
                    return null;
                }
            }
            return null;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteFolder(string path)
        {
            try
            {
                _fileSystemService.DeleteDirectory(path);
            }
            catch (FileSystemServiceException ex)
            {
                if (ex.Error == FileSystemError.YouHaventAccessToTheResource)
                    return PartialView("Error", "You Haven't Access To The Resource");
                if (ex.Error == FileSystemError.DirectoryDoesntExist)
                    return PartialView("Error", "Directory Doesn't Exist");
                if (ex.Error == FileSystemError.IncorrectPath)
                    return PartialView("Error", "Incorrect Path");

            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteFile(string path)
        {
            try
            {
                _fileSystemService.DeleteFile(path);
            }
            catch (FileSystemServiceException ex)
            {
                if (ex.Error == FileSystemError.YouHaventAccessToTheResource)
                    return PartialView("Error", "You Haven't Access To The Resource");
                if (ex.Error == FileSystemError.FileDoesntExists)
                    return PartialView("Error", "Directory Doesn't Exist");
                if (ex.Error == FileSystemError.IncorrectPath)
                    return PartialView("Error", "Incorrect Path");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateFileOrFolder(string path)
        {
            var model = new CreateViewModel() { Name = path };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateFileOrFolder(CreateViewModel model)
        {
            if (model.File || model.Directory)
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (model.File)
                        {
                            _fileSystemService.CreateFile(model.Name);
                        }
                        if (model.Directory)
                        {
                            _fileSystemService.CreateDirectory(model.Name);
                        }
                        return RedirectToAction("Index", new { path = model.Name });
                    }
                    catch (FileSystemServiceException ex)
                    {
                        if (ex.Error == FileSystemError.IncorrectPath)
                            ModelState.AddModelError("", "Incorrect Path");
                        if (ex.Error == FileSystemError.YouHaventAccessToTheResource)
                            ModelState.AddModelError("", "You Havent Access To The Resource");
                    }
                }
            ModelState.AddModelError("", "You must choose at least file or folder");
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