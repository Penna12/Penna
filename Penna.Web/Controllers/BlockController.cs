using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Extensions;
using Penna.Core.Utilities.Constants;
using Penna.Core.Utilities.Enums;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class BlockController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IBlockService _blockService;
        private readonly IBlockTeamService _blockTeamService;
        private readonly IBlockSubcontractorService _blockSubcontractorService;
        private readonly IBlockSubcontractorBusinessGroupService _blockSubcontractorBusinessGroupService;

        public BlockController(IHttpContextAccessor httpContextAccessor, IMapper mapper, IBlockService blockService,
            IBlockTeamService blockTeamService, 
            IBlockSubcontractorService blockSubcontractorService, 
            IBlockSubcontractorBusinessGroupService blockSubcontractorBusinessGroupService)
        {
            _mapper = mapper;
            _blockService = blockService;
            _httpContextAccessor = httpContextAccessor;
            _blockTeamService = blockTeamService;
            _blockSubcontractorService = blockSubcontractorService;
            _blockSubcontractorBusinessGroupService = blockSubcontractorBusinessGroupService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "BlockList";
            Toolbar.Title = "Blok Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Blok Listesi" };
            Toolbar.Urls = new[] { "/", "#" };
             
            BlockDto blockDto = new BlockDto();
            blockDto.BlockTypeEnumList = _blockService.GetBlockTypeListForDropDown();
            blockDto.CostCalculationEnumList = _blockService.GetCostCalculationListForDropDown();
            return View(blockDto);
        }

        [ServiceFilter(typeof(BlockSelectedCheckFilter))]
        public async Task<IActionResult> Documents(string path)
        {
            TempData["active"] = "BlockDocuments";
            Toolbar.Title = "Blok Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Blok Dosya Listesi" };
            Toolbar.Urls = new[] { "/", "#" };

            BlockDto blockDto = new BlockDto();
            blockDto.Block = await _blockService.SingleOrDefaultAsync(b => b.ProjectId == SD.ProjectId && b.Id == SD.BlockId, includeProperties: "Project,Apartments");
            ViewBag.Block = blockDto.Block;
            // ====================================================================================

            var folderPath = SD.RootPath + "\\Folder\\";
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

            //return View(blockDto);
        }

        [ServiceFilter(typeof(BlockSelectedCheckFilter))]
        public async Task<IActionResult> Teams()
        {
            TempData["active"] = "BlockTeams";
            Toolbar.Title = "Blok Ekibi Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Blok Ekip Listesi" };
            Toolbar.Urls = new[] { "/", "#" };

            BlockTeamDto blockTeamDto = new BlockTeamDto();
            blockTeamDto.Block = await _blockService.SingleOrDefaultAsync(b => b.ProjectId == SD.ProjectId && b.Id == SD.BlockId, includeProperties: "Project");
            blockTeamDto.UserList = _blockService.GetUserListForDropDown(SD.TenantId);
            blockTeamDto.UserPositionList = _blockService.GetUserPositionListForDropDown(SD.TenantId);
            return View(blockTeamDto);
        }

        [ServiceFilter(typeof(BlockSelectedCheckFilter))]
        public async Task<IActionResult> Subcontractors()
        {
            TempData["active"] = "BlockSubcontractors";
            Toolbar.Title = "Blok Taşeronları Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Taşeron Listesi" };
            Toolbar.Urls = new[] { "/", "#" };

            BlockSubcontractorDto blockSubcontractor = new BlockSubcontractorDto();
            blockSubcontractor.Block = await _blockService.SingleOrDefaultAsync(b => b.ProjectId == SD.ProjectId && b.Id == SD.BlockId, includeProperties: "Project");
            blockSubcontractor.SubcontractorList = _blockService.GetSubcontractorListForDropDown(SD.TenantId);
            return View(blockSubcontractor);
        }


        #region Calling Json data
        [HttpGet]
        public IActionResult GetEnumName(Enum name)
        {
            return Json(new { success = true, data = EnumExtensions.GetDisplayName(name) });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var list = await _blockService.Where(b => b.ProjectId == SD.ProjectId, includeProperties:"Project");
            return Json(new { success = list != null, data = list });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var block = await _blockService.GetByIdAsync(id);
            return Json(new { success = block != null, data = block });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Block block)
        {
            if (ModelState.IsValid)
            {
                block.ProjectId = SD.ProjectId;
                block.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                block.CreatedDate = DateTime.Now;
                await _blockService.AddAsync(block);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(Block block)
        {
            if (ModelState.IsValid)
            {
                block.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                block.UpdatedDate = DateTime.Now;
                _blockService.Update(block);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var block = await _blockService.GetByIdAsync(id);
            if (block == null)
            {
                return Json(new { success = false });
            }
            _blockService.Remove(block);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> PossibleNewBlockCreate()
        {
            var result = await _blockService.Where(b => b.ProjectId == SD.ProjectId &&  b.TypeId == BlockTypeEnum.Block, includeProperties:"Project");
            int createdBlockCount = result.Count();
            if (createdBlockCount > 0)
            {
                var project = result.First().Project;
                if ((project.BlockCount) <= createdBlockCount)
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = true });
        }
        #endregion Calling Json data

        #region Block Teams Json data
        [HttpGet]
        public async Task<IActionResult> GetUserData(string userId)
        {
            var list = await _blockService.GetAppUserByIdAsync(userId);
            return Json(new { success = list != null, data = list });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllBlockTeamsData()
        {
            var list = await _blockTeamService.GetBlockTeamListAsync(SD.BlockId);
            return Json(new { success = list != null, data = list });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam(BlockTeam blockTeam)
        {
            if (ModelState.IsValid)
            {
                blockTeam.BlockId = SD.BlockId;
                blockTeam.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                blockTeam.CreatedDate = DateTime.Now;
                await _blockTeamService.AddAsync(blockTeam);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var blockTeam = await _blockTeamService.GetByIdAsync(id);
            if (blockTeam == null)
            {
                return Json(new { success = false });
            }
            _blockTeamService.Remove(blockTeam);
            return Json(new { success = true });
        }

        #endregion Block Teams Json data

        #region Block Subcontractors Json data
        [HttpGet]
        public async Task<IActionResult> GetCurrentAccountData(int accountId)
        {
            var list = await _blockService.GetCurrentAccountByIdAsync(accountId);
            return Json(new { success = list != null, data = list });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlockSubcontractorsData()
        {
            var list = await _blockSubcontractorService.Where(b => b.BlockId == SD.BlockId, includeProperties: "Block,CurrentAccount,BlockSubcontractorBusinessGroups");
            return Json(new { success = list != null, data = list });
        }

        [HttpGet]
        public IActionResult GetBusinessGroupEnumList()
        {
            var list = EnumExtensions.GetAttributeList(typeof(BusinessGroupEnum));
            return Json(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubcontractor(BlockSubcontractorAddDto blockSubcontractorAddDto)
        {
            if (ModelState.IsValid)
            {
                BlockSubcontractor blockSubcontractor = new BlockSubcontractor();
                blockSubcontractor.BlockId = SD.BlockId;
                blockSubcontractor.CurrentAccountId = blockSubcontractorAddDto.CurrentAccountId;
                blockSubcontractor.CompanyRepresentative = blockSubcontractorAddDto.CompanyRepresentative;
                blockSubcontractor.Phone = blockSubcontractorAddDto.Phone;
                blockSubcontractor.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                blockSubcontractor.CreatedDate = DateTime.Now;
                await _blockSubcontractorService.AddAsync(blockSubcontractor);
                // BusinessGroupEnum[] BusinessGroupId  alt tablosuna kayıt yapılacak.
                foreach (var bsbg in blockSubcontractorAddDto.BusinessGroupId)
                {
                    BlockSubcontractorBusinessGroup blockSubcontractorBusinessGroup = new BlockSubcontractorBusinessGroup()
                    {
                        BlockSubcontractorId = blockSubcontractor.Id,
                        BusinessGroupId = bsbg
                    };
                    await _blockSubcontractorBusinessGroupService.AddAsync(blockSubcontractorBusinessGroup);
                }
                
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubcontractor(int id)
        {
            var blockSubcontractor = await _blockSubcontractorService.GetByIdAsync(id);
            if (blockSubcontractor == null)
            {
                return Json(new { success = false });
            }
            _blockSubcontractorService.Remove(blockSubcontractor);
            return Json(new { success = true });
        }

        #endregion Block Subcontractors Json data


        #region upload document, calling json data
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
        public IActionResult Upload(IFormFile file, string URL, string RealUrl)
        {
            if (file != null)
            {
                var folderPath = SD.RootPath + "\\Folder\\";

                if (RealUrl.Length > 0 && (RealUrl.First() == '\\' || RealUrl.First() == '/'))
                    RealUrl = RealUrl.Remove(0, RealUrl.First().ToString().Length);

                var realUrl = WebUtility.UrlDecode(RealUrl);
                var path = Path.Combine(folderPath, realUrl, file.FileName);
                if (System.IO.File.Exists(path))
                { return Content("Bu dosya yüklüdür."); }
                else
                {
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream).Wait();
                    stream.Close();
                    stream.Dispose();
                }
            }

            return Redirect(URL);
        }


        [HttpPost]
        public IActionResult DeleteFile(string realUrl, string fileName)
        {
            var folderPath = SD.RootPath + "\\Folder\\";
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
            var folderPath = SD.RootPath + "\\Folder\\";
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
            var folderPath = SD.RootPath + "\\Folder\\";
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
        //public async Task<IActionResult> DownloadFile(int id, string path)
        //{
        //    var response = await _blockService.DownloadDocument(id, path);
        //    return Json(new { isSuccess = response != null, file = response, info = path });
        //}

        //public async Task<IActionResult> CreateFolder(LitigationCreateFolderViewModel model)
        //{
        //    var result = await _blockService.CreateFolder(model);
        //    return Json(new { isSuccess = result });
        //}

        //public async Task<IActionResult> DeleteFolder(LitigationDeleteFolderViewModel model)
        //{
        //    var result = await _blockService.DeleteFolder(model);
        //    return Json(new { isSuccess = result != null, message = result });
        //}

        //public async Task<IActionResult> DeleteFile(LitigationDeleteFileViewModel model)
        //{
        //    var result = await _blockService.DeleteFile(model);
        //    return Json(new { isSuccess = result });
        //}

        //public async Task<IActionResult> UploadFile(LitigationFileUploadViewModel model)
        //{
        //    var result = await _blockService.UploadFile(model);
        //    return RedirectToAction("UploadDocuments", new { id = model.litId, path = model.URL });
        //}
        #endregion upload document

    }
}
