using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using Penna.Core.Utilities.Enums;
using Penna.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class ExplorerController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectService _projectService;

        public ExplorerController(IHttpContextAccessor httpContextAccessor, IProjectService projectService)
        {
            _httpContextAccessor = httpContextAccessor;
            _projectService = projectService;
        }

        //
        // GET: /Explorer/
        public IActionResult Index(string path)
        {
            //var projectFilesRequested = Request.Headers["Referer"].ToString().Contains("Project");

            TempData["active"] = (SD.BlockId > 0) ? "BlockDocuments" : "ProjectDocuments";
            Toolbar.Title = (SD.BlockId > 0) ? "Blok Dosyaları Listesi" : "Proje Dosyaları Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", (SD.BlockId > 0) ? "Blok Dosyaları Listesi" : "Proje Dosyaları Listesi" };
            Toolbar.Urls = new[] { "/", "#" };

            // ProjectDocuments
            var folderPath = SD.RootPath + "\\UploadedDocuments\\" + $"Tenant-{SD.TenantId}\\" + $"Project-{SD.ProjectId}\\";
            if (SD.BlockId > 0)
            {
                folderPath = folderPath + $"Block-{SD.BlockId}\\";
            }
            // or folderPath = "FullPath of the folder on server"             
            var realPath = folderPath + path;

            if (System.IO.File.Exists(realPath)) // Get File Download *************
            {
                var fileBytes = System.IO.File.ReadAllBytes(realPath);

                //http://stackoverflow.com/questions/1176022/unknown-file-type-mime
                return File(fileBytes, "application/force-download");
            }
            else if (Directory.Exists(realPath)) // Get Directory List *************
            {
                List<DirModel> dirListModel = MapDirs(realPath);

                List<FileModel> fileListModel = MapFiles(realPath);

                ExplorerModel explorerModel = new ExplorerModel(dirListModel, fileListModel);

                //For using browser ability to correctly browsing the folders,
                //Every path needs to end with slash
                if (realPath.Last() != '/' && realPath.Last() != '\\')
                { explorerModel.URL = "/Explorer/" + path + "/"; explorerModel.RealUrl = path + "/"; }
                else
                { explorerModel.URL = "/Explorer/" + path; explorerModel.RealUrl = path ?? "/"; }

                var request = _httpContextAccessor.HttpContext.Request;

                UriBuilder uriBuilder = new UriBuilder
                { Path = request.Path.ToString() };

                //Gettin the current directory name using page URL.
                explorerModel.FolderName = WebUtility.UrlDecode(uriBuilder.Uri.Segments.Last());

                //Making a URL to going up one level. 
                Uri uri = new Uri(uriBuilder.Uri.AbsoluteUri.Remove
                                    (uriBuilder.Uri.AbsoluteUri.Length - uriBuilder.Uri.Segments.Last().Length));

                explorerModel.ParentFolderName = uri.AbsolutePath;

                return View(explorerModel);
            }
            else
            {
                return Content(path + " is not a valid file or directory.");
            }
        }

        [HttpGet("{controller}/{action}/{*path}")]
        public IActionResult Project(string path)
        {
            //var projectFilesRequested = Request.Headers["Referer"].ToString().Contains("Project");

            TempData["active"] = (SD.BlockId > 0) ? "BlockDocuments" : "ProjectDocuments";
            Toolbar.Title = (SD.BlockId > 0) ? "Blok Dosyaları Listesi" : "Proje Dosyaları Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", (SD.BlockId > 0) ? "Blok Dosyaları Listesi" : "Proje Dosyaları Listesi" };
            Toolbar.Urls = new[] { "/", "#" };

            // ProjectDocuments
            var folderPath = SD.RootPath + "\\UploadedDocuments\\" + $"Tenant-{SD.TenantId}\\" + $"Project-{SD.ProjectId}\\";
            // or folderPath = "FullPath of the folder on server" 
            var realPath = folderPath + path;

            if (System.IO.File.Exists(realPath)) // Get File Download *************
            {
                var fileBytes = System.IO.File.ReadAllBytes(realPath);

                //http://stackoverflow.com/questions/1176022/unknown-file-type-mime
                return File(fileBytes, "application/force-download");
            }
            else if (Directory.Exists(realPath)) // Get Directory List *************
            {
                List<DirModel> dirListModel = MapDirs(realPath);

                List<FileModel> fileListModel = MapFiles(realPath);

                ExplorerModel explorerModel = new ExplorerModel(dirListModel, fileListModel);

                //For using browser ability to correctly browsing the folders,
                //Every path needs to end with slash
                if (realPath.Last() != '/' && realPath.Last() != '\\')
                { explorerModel.URL = "/Explorer/Project/" + path + "/"; explorerModel.RealUrl = path + "/"; }
                else
                { explorerModel.URL = "/Explorer/Project/" + path; explorerModel.RealUrl = path ?? "/"; }

                var request = _httpContextAccessor.HttpContext.Request;

                UriBuilder uriBuilder = new UriBuilder
                { Path = request.Path.ToString() };

                //Gettin the current directory name using page URL.
                explorerModel.FolderName = WebUtility.UrlDecode(uriBuilder.Uri.Segments.Last());

                //Making a URL to going up one level. 
                Uri uri = new Uri(uriBuilder.Uri.AbsoluteUri.Remove
                                    (uriBuilder.Uri.AbsoluteUri.Length - uriBuilder.Uri.Segments.Last().Length));

                explorerModel.ParentFolderName = uri.AbsolutePath;

                return View("Index", explorerModel);
            }
            else
            {
                return Content(path + " is not a valid file or directory.");
            }
        }

        [HttpGet("{controller}/{action}/{*path}")]
        public IActionResult Block(string path)
        {
            //var projectFilesRequested = Request.Headers["Referer"].ToString().Contains("Project");

            TempData["active"] = (SD.BlockId > 0) ? "BlockDocuments" : "ProjectDocuments";
            Toolbar.Title = (SD.BlockId > 0) ? "Blok Dosyaları Listesi" : "Proje Dosyaları Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", (SD.BlockId > 0) ? "Blok Dosyaları Listesi" : "Proje Dosyaları Listesi" };
            Toolbar.Urls = new[] { "/", "#" };

            // ProjectDocuments
            var folderPath = SD.RootPath + "\\UploadedDocuments\\" + $"Tenant-{SD.TenantId}\\" + $"Project-{SD.ProjectId}\\";
            if (SD.BlockId > 0)
            {
                folderPath = folderPath + $"Block-{SD.BlockId}\\";
            }
            // or folderPath = "FullPath of the folder on server" 
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var realPath = folderPath + path;

            if (System.IO.File.Exists(realPath)) // Get File Download *************
            {
                var fileBytes = System.IO.File.ReadAllBytes(realPath);

                //http://stackoverflow.com/questions/1176022/unknown-file-type-mime
                return File(fileBytes, "application/force-download");
            }
            else if (Directory.Exists(realPath)) // Get Directory List *************
            {
                List<DirModel> dirListModel = MapDirs(realPath);

                List<FileModel> fileListModel = MapFiles(realPath);

                ExplorerModel explorerModel = new ExplorerModel(dirListModel, fileListModel);

                //For using browser ability to correctly browsing the folders,
                //Every path needs to end with slash
                if (realPath.Last() != '/' && realPath.Last() != '\\')
                { explorerModel.URL = "/Explorer/Block/" + path + "/"; explorerModel.RealUrl = path + "/"; }
                else
                { explorerModel.URL = "/Explorer/Block/" + path; explorerModel.RealUrl = path ?? "/"; }

                var request = _httpContextAccessor.HttpContext.Request;

                UriBuilder uriBuilder = new UriBuilder
                { Path = request.Path.ToString() };

                //Gettin the current directory name using page URL.
                explorerModel.FolderName = WebUtility.UrlDecode(uriBuilder.Uri.Segments.Last());

                //Making a URL to going up one level. 
                Uri uri = new Uri(uriBuilder.Uri.AbsoluteUri.Remove
                                    (uriBuilder.Uri.AbsoluteUri.Length - uriBuilder.Uri.Segments.Last().Length));

                explorerModel.ParentFolderName = uri.AbsolutePath;

                return View("Index", explorerModel);
            }
            else
            {
                return Content(path + " is not a valid file or directory.");
            }
        }

        private List<DirModel> MapDirs(String realPath)
        {
            List<DirModel> dirListModel = new List<DirModel>();

            IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
            foreach (string dir in dirList)
            {
                DirectoryInfo d = new DirectoryInfo(dir);

                DirModel dirModel = new DirModel
                {
                    DirName = Path.GetFileName(dir),
                    DirAccessed = d.LastAccessTime
                };

                dirListModel.Add(dirModel);
            }

            return dirListModel;
        }

        private List<FileModel> MapFiles(String realPath)
        {
            List<FileModel> fileListModel = new List<FileModel>();

            IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
            foreach (string file in fileList)
            {
                FileInfo f = new FileInfo(file);

                FileModel fileModel = new FileModel();

                if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
                    && f.Extension.ToLower() != "asp" && f.Extension.ToLower() != "exe")
                {
                    fileModel.FileName = Path.GetFileName(file);
                    fileModel.FileAccessed = f.LastAccessTime;
                    fileModel.FileSizeText = (f.Length < 1024) ? f.Length.ToString() + " B" : f.Length / 1024 + " KB";

                    fileListModel.Add(fileModel);
                }
            }

            return fileListModel;
        }

        [HttpPost]
        public IActionResult Upload(UploadModelDto uploadModel)
        {
            /// Burada ilk önce whatsup ile mesajı gönder, 
            /// mesaj gitmez ise tekrar mesaj yazma imkanımı olsun
            /// yada dosyalarıda mı yüklemesin 
            var folderPath = SD.RootPath + "\\UploadedDocuments\\" + $"Tenant-{SD.TenantId}\\" + $"Project-{SD.ProjectId}\\";
            if (SD.BlockId > 0)
            {
                folderPath = folderPath + $"Block-{SD.BlockId}\\";
            }
            string path = string.Empty;
            string uploadPath = string.Empty;

            // Mimari Upload
            if(uploadModel.Mimari != null)
            {
                try
                {
                    uploadPath = folderPath + "Mimari\\";
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    path = Path.Combine(uploadPath, uploadModel.Mimari.FileName);
                    if (System.IO.File.Exists(path))
                    { ModelState.AddModelError("", $"{uploadModel.Mimari.FileName}, Bu dosya yüklüdür."); }
                    else
                    {
                        var stream = new FileStream(path, FileMode.Create);
                        uploadModel.Mimari.CopyToAsync(stream).Wait();
                        stream.Close();
                        stream.Dispose();
                    }
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Mimari dosya yüklenirken bir hata oldu. Dosya yüklenemedi." });
                }
            }

            // Statik Upload
            if (uploadModel.Statik != null)
            {
                try
                {
                    uploadPath = folderPath + "Statik\\";
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    path = Path.Combine(uploadPath, uploadModel.Statik.FileName);
                    if (System.IO.File.Exists(path))
                    { ModelState.AddModelError("", $"{uploadModel.Statik.FileName}, Bu dosya yüklüdür."); }
                    else
                    {
                        var stream = new FileStream(path, FileMode.Create);
                        uploadModel.Statik.CopyToAsync(stream).Wait();
                        stream.Close();
                        stream.Dispose();
                    }
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Statik dosya yüklenirken bir hata oldu. Dosya yüklenemedi." });
                }
            }

            // Mekanik Upload
            if (uploadModel.Mekanik != null)
            {
                try
                {
                    uploadPath = folderPath + "Mekanik\\";
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    path = Path.Combine(uploadPath, uploadModel.Mekanik.FileName);
                    if (System.IO.File.Exists(path))
                    { ModelState.AddModelError("", $"{uploadModel.Mekanik.FileName}, Bu dosya yüklüdür."); }
                    else
                    {
                        var stream = new FileStream(path, FileMode.Create);
                        uploadModel.Mekanik.CopyToAsync(stream).Wait();
                        stream.Close();
                        stream.Dispose();
                    }
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Mekanik dosya yüklenirken bir hata oldu. Dosya yüklenemedi." });
                }
            }

            // Elektrik Upload
            if (uploadModel.Elektrik != null)
            {
                try
                {
                    uploadPath = folderPath + "Elektrik\\";
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    path = Path.Combine(uploadPath, uploadModel.Elektrik.FileName);
                    if (System.IO.File.Exists(path))
                    { ModelState.AddModelError("", $"{uploadModel.Elektrik.FileName}, Bu dosya yüklüdür."); }
                    else
                    {
                        var stream = new FileStream(path, FileMode.Create);
                        uploadModel.Elektrik.CopyToAsync(stream).Wait();
                        stream.Close();
                        stream.Dispose();
                    }
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Elektrik dosya yüklenirken bir hata oldu. Dosya yüklenemedi." });
                }
            }

            // Peyzaj Upload
            if (uploadModel.Peyzaj != null)
            {
                try
                {
                    if (uploadModel.Peyzaj != null)
                    {
                        uploadPath = folderPath + "Peyzaj\\";
                        if (!Directory.Exists(uploadPath))
                            Directory.CreateDirectory(uploadPath);
                        path = Path.Combine(uploadPath, uploadModel.Peyzaj.FileName);
                        if (System.IO.File.Exists(path))
                        { ModelState.AddModelError("", $"{uploadModel.Peyzaj.FileName}, Bu dosya yüklüdür."); }
                        else
                        {
                            var stream = new FileStream(path, FileMode.Create);
                            uploadModel.Peyzaj.CopyToAsync(stream).Wait();
                            stream.Close();
                            stream.Dispose();
                        }
                    }
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Peyzaj dosya yüklenirken bir hata oldu. Dosya yüklenemedi." });
                }
            }

            return Json(new { success = true }); // Redirect(uploadModel.URL);
        }


        [HttpPost]
        public IActionResult DeleteFile(string realUrl, string fileName)
        {
            var folderPath = SD.RootPath + "\\UploadedDocuments\\";
            if (realUrl.Length > 0 && (realUrl.First() == '\\' || realUrl.First() == '/'))
                realUrl = realUrl.Remove(0, realUrl.First().ToString().Length);
            realUrl = WebUtility.UrlDecode(realUrl);
            string path = Path.Combine(folderPath, realUrl, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult CreateFolder(string realUrl, string folderName)
        {
            var folderPath = SD.RootPath + "\\UploadedDocuments\\";
            if (realUrl.Length > 0 && (realUrl.First() == '\\' || realUrl.First() == '/'))
                realUrl = realUrl.Remove(0, realUrl.First().ToString().Length);
            realUrl = WebUtility.UrlDecode(realUrl);
            string path = Path.Combine(folderPath, realUrl, folderName);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult DeleteFolder(string realUrl, string folderName)
        {
            var folderPath = SD.RootPath + "\\UploadedDocuments\\";
            if (realUrl.Length > 0 && (realUrl.First() == '\\' || realUrl.First() == '/'))
                realUrl = realUrl.Remove(0, realUrl.First().ToString().Length);
            realUrl = WebUtility.UrlDecode(realUrl);
            string path = Path.Combine(folderPath, realUrl, folderName);
            if (System.IO.Directory.Exists(path))
            {
                if (System.IO.Directory.EnumerateFileSystemEntries(path).Count() == 0)
                {
                    System.IO.Directory.Delete(path);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Dizin boş değil!" });
                }
            }

            return Json(new { success = false, message = "Dizin bulunamadı!" });
        }

        [HttpGet]
        public async Task<IActionResult> ProjectActiveCheck()
        {
            var project = await _projectService.GetByIdAsync(SD.ProjectId);
            return Json(new { success = project != null && project.Status == ProjectStatusEnum.Active });
        }
    }
}
