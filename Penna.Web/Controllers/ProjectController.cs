using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Interfaces;
using Penna.Entities.DTOs;
using Penna.Core.Extensions;
using Penna.Business.Filters;
using System.Threading.Tasks;
using Penna.Entities.Models;
using System;
using System.Security.Claims;
using Penna.Core.Utilities.Enums;
using Penna.Core.Utilities.Constants;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly ICurrentAccountService _currentAccountService;
        private readonly ICurrentAccountDetailService _currentAccountDetailService;
        private readonly IAccountService _accountService;
        public ProjectController(IMapper mapper, IAccountService accountService, IProjectService projectService,
            ICurrentAccountService currentAccountService, ICurrentAccountDetailService currentAccountDetailService)
        {
            _projectService = projectService;
            _mapper = mapper;
            _currentAccountService = currentAccountService;
            _accountService = accountService;
            _currentAccountDetailService = currentAccountDetailService;
        }


        public IActionResult Index()
        {
            TempData["active"] = "ProjectList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Projeler" };
            Toolbar.Urls = new[] { "/", "#" };
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            TempData["active"] = "ProjectEdit";
            Toolbar.Title = "Proje Güncelle";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Proje Güncelle" };
            Toolbar.Urls = new[] { "/", "#" };

            ProjectDto projectDto = new ProjectDto();
            if (id == null)
            {
                projectDto.CurrentAccountList = _projectService.GetCurrentAccountListForDropDown(Convert.ToInt32(User.GetClaimValue("TenantId")), CurrentAccountTypeEnum.ProjectOwner);
                projectDto.CountryList = _projectService.GetCountryListForDropDown();
                projectDto.Project.TenantId = Convert.ToInt32(User.GetClaimValue("TenantId"));
                return View(projectDto);
            }

            projectDto.Project = await _projectService.SingleOrDefaultAsync(x => x.Id == id, includeProperties: "CurrentAccount");
            if (projectDto.Project == null)
            {
                return NotFound();
            }
            projectDto.CurrentAccountList = _projectService.GetCurrentAccountListForDropDown(Convert.ToInt32(User.GetClaimValue("TenantId")), CurrentAccountTypeEnum.ProjectOwner, projectDto.Project.CurrentAccountId);
            projectDto.CountryList = _projectService.GetCountryListForDropDown(projectDto.Project.CountryId);
            projectDto.CityList = _projectService.GetCityListForDropDown(projectDto.Project.CountryId, projectDto.Project.CityId);
            projectDto.TownList = _projectService.GetTownListForDropDown(projectDto.Project.CityId, projectDto.Project.TownId);

            return View(projectDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProjectDto projectDto)
        {
            if (ModelState.IsValid)
            {
                if (projectDto.Project.Id == 0)
                {
                    projectDto.Project.TenantId = Convert.ToInt32(User.GetClaimValue("TenantId"));
                    projectDto.Project.CreatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    projectDto.Project.CreatedDate = DateTime.Now;
                    projectDto.Project.Status = ProjectStatusEnum.Active;
                    projectDto.Project.IsLocked = false;
                    await _projectService.AddAsync(projectDto.Project);
                    // cari hesabını da kaydet
                }
                else
                {
                    projectDto.Project.TenantId = Convert.ToInt32(User.GetClaimValue("TenantId"));
                    projectDto.Project.UpdatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    projectDto.Project.UpdatedDate = DateTime.Now;
                    _projectService.Update(projectDto.Project);
                }
                return RedirectToAction(nameof(Index));
            }
            projectDto.CountryList = _projectService.GetCountryListForDropDown(projectDto.Project.CountryId);
            projectDto.CityList = _projectService.GetCityListForDropDown(projectDto.Project.CountryId, projectDto.Project.CityId);
            projectDto.TownList = _projectService.GetTownListForDropDown(projectDto.Project.CityId, projectDto.Project.TownId);
            return View(projectDto);
        }

        public async Task<IActionResult> Detail(int id)
        {
            TempData["active"] = "ProjectDetail";
            Toolbar.Title = "Proje Detay";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Proje Detayları" };
            Toolbar.Urls = new[] { "/", "#" };

            var project = await _projectService.GetWithTenantByProjectIdAsync(id);
            if (project == null)
            {
                return RedirectToAction(nameof(Index));
            }

            SD.TenantId = project.TenantId;
            SD.ProjectId = project.Id;
            SD.ProjectName = project.ProjectName;
            SD.CurAccountId = project.CurrentAccountId;
            SD.CurAccountName = project.CurrentAccount.CompanyName;
            HttpContext.Session.SetString(SD.SESSION_KEY_PROJECT_ID, project.Id.ToString());
            HttpContext.Session.SetString(SD.SESSION_KEY_PROJECT_NAME, project.ProjectName);

            return RedirectToAction("Index", "Block");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            TempData["active"] = "ProjectActivate";
            Toolbar.Title = "Proje Aktifleştirme";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Projeyi Aktif et" };
            Toolbar.Urls = new[] { "/", "#" };

            ProjectViewDto projectViewDto = new ProjectViewDto();
            projectViewDto.Project = await _projectService.SingleOrDefaultAsync(p => p.Id == id, includeProperties: "Tenant,CurrentAccount,CurrentAccountDetails,Blocks");
            if (projectViewDto.Project == null)
            {
                return RedirectToAction(nameof(Index));
            }

            projectViewDto.Blocks = projectViewDto.Project.Blocks;

            SD.TenantId = projectViewDto.Project.TenantId;
            SD.ProjectId = projectViewDto.Project.Id;
            SD.ProjectName = projectViewDto.Project.ProjectName;
            SD.CurAccountId = projectViewDto.Project.CurrentAccountId;
            SD.CurAccountName = projectViewDto.Project.CurrentAccount.CompanyName;
            HttpContext.Session.SetString(SD.SESSION_KEY_PROJECT_ID, projectViewDto.Project.Id.ToString());
            HttpContext.Session.SetString(SD.SESSION_KEY_PROJECT_NAME, projectViewDto.Project.ProjectName);

            projectViewDto.CurrentAccount = await _currentAccountService.SingleOrDefaultAsync(a => a.Id == projectViewDto.Project.CurrentAccountId, includeProperties: "Country,City,Town");
            projectViewDto.CurrentAccountDetail = projectViewDto.Project.CurrentAccountDetails;//await _currentAccountDetailService.Where(d => d.CurrentAccountId == projectViewDto.CurrentAccount.Id);

            projectViewDto.URL = SD.RootPath + "\\UploadedDocuments\\" + $"Tenant-{SD.TenantId}\\" + $"Project-{id}\\";
            projectViewDto.FileListModel = MapFiles(projectViewDto.URL);

            return View(projectViewDto);
        }


        private List<FileModel> MapFiles(String realPath)
        {
            List<FileModel> fileListModel = new List<FileModel>();

            IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
            foreach (string file in fileList)
            {
                FileInfo f = new FileInfo(file);

                FileModel fileModel = new FileModel();
                fileModel.FileName = Path.GetFileName(file);
                fileModel.FileAccessed = f.LastAccessTime;
                fileModel.FileSizeText = (f.Length < 1024) ? f.Length.ToString() + " B" : f.Length / 1024 + " KB";

                fileListModel.Add(fileModel);
            }

            return fileListModel;
        }


        #region Calling Json data

        [HttpPost]
        public async Task<IActionResult> SetChangeStatus(int id, ProjectStatusEnum status)
        {
            try
            {
                Project project = await _projectService.GetByIdAsync(id);
                project.Status = status;
                _projectService.Update(project);
                return Json(new { success = true });
            }
            catch (Exception)   
            {
                return Json(new { success = false });
            }
        }

        public async Task<IActionResult> GetAllProjects()
        {
            var list = await _projectService.GetAllAsync(includeProperties: "Tenant,Country,City,Town");
            return Json(new { success = list != null, data = list }); 
        }

        public async Task<IActionResult> GetActiveProjects()
        {
            return Json(new { data = await _projectService.Where(predicate: p => p.Status == ProjectStatusEnum.Active, includeProperties: "Tenant,Country,City,Town") });
        }
        
        public async Task<IActionResult> GetFinishedProjects()
        {
            return Json(new { data = await _projectService.Where(predicate: p => p.Status == ProjectStatusEnum.Closed, includeProperties: "Tenant,Country,City,Town") });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return Json(new { success = false, message = "Data silinirken bir hata oldu." });
            }
            _projectService.Remove(project);
            return Json(new { success = true, message = "Data başarıyla silindi." });
        }

        [HttpGet]
        public IActionResult GetCityData(int cid)
        {
            return Json(new { data = _projectService.GetCityListForDropDown(cid) });
        }

        [HttpGet]
        public IActionResult GetTownData(int cid)
        {
            return Json(new { data = _projectService.GetTownListForDropDown(cid) });
        }

        [HttpPost]
        public async Task<IActionResult> SaveCurrentAccount(CurrentAccount currentAccount)
        {
            currentAccount.TenantId = Convert.ToInt32(User.GetClaimValue("TenantId"));
            currentAccount.AccountTypeId = CurrentAccountTypeEnum.ProjectOwner;
            currentAccount.CompanyStatusId = CompanyStatusEnum.WhiteList;
            currentAccount.CreatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
            currentAccount.CreatedDate = DateTime.Now;
            await _currentAccountService.AddAsync(currentAccount);
            return Json(new { success = true, data = currentAccount });
        }
        
        public async Task<IActionResult> GetCurrentAccountData(int caid)
        {
            return Json(new { data = await _currentAccountService.SingleOrDefaultAsync(x => x.Id == caid && x.AccountTypeId == CurrentAccountTypeEnum.ProjectOwner) });
        }

        
        #endregion Calling Json data
    }
}
