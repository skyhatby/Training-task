using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLN.Test.Project.Logic.FileSystem.Interfaces;

namespace WLN.Test.Project.Web.Controllers
{
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