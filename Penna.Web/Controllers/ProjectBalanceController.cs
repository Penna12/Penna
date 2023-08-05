using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Extensions;
using Penna.Core.Utilities.Constants;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class ProjectBalanceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly ICurrentAccountService _currentAccountService;
        private readonly ICurrentAccountDetailService _currentAccountDetailService;

        public ProjectBalanceController(IMapper mapper, IProjectService projectService, 
            ICurrentAccountService currentAccountService, ICurrentAccountDetailService currentAccountDetailService)
        {
            _mapper = mapper;
            _projectService = projectService;
            _currentAccountService = currentAccountService;
            _currentAccountDetailService = currentAccountDetailService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["active"] = "ProjectBalance";
            Toolbar.Title = "Proje Bilanço";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Proje Bilanço" };
            Toolbar.Urls = new[] { "/", "#" };

            ProjectViewDto projectViewDto = new ProjectViewDto();
            projectViewDto.Project = await _projectService.SingleOrDefaultAsync(p => p.Id == SD.ProjectId, includeProperties: "CurrentAccountDetails");
            if (projectViewDto.Project == null)
            {
                return RedirectToAction(nameof(Index));
            }
            projectViewDto.CurrentAccount = await _currentAccountService.SingleOrDefaultAsync(a => a.Id == projectViewDto.Project.CurrentAccountId, includeProperties:"Country,City,Town");
            projectViewDto.CurrentAccountDetail = await _currentAccountDetailService.Where(d => d.CurrentAccountId == projectViewDto.CurrentAccount.Id); //  && d.ProjectId  == projectViewDto.Project.Id
            return View(projectViewDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentAccountDetails()
        {
            var list = await _currentAccountDetailService.Where(d => d.ProjectId == SD.ProjectId);
            return Json(new { data = list });
        }
        
        [HttpGet]
        public async Task<IActionResult> TaksitHesaplaGetir()
        {
            try
            {
                var project = await _projectService.SingleOrDefaultAsync(p => p.Id == SD.ProjectId);
                double taahhutTutari = project.CommitmentAmount;
                float pesinatOrani = project.DownPaymentRate;
                double pesinatTutari = 0;
                int taksitSayisi = project.InstallmentCount;
                DateTime pesinatTarihi = DateTime.Now;

                pesinatTutari = Math.Round(taahhutTutari * (pesinatOrani / 100));
                double TaksitlenecekKisim = taahhutTutari - pesinatTutari;
                double Taksit = Math.Round((TaksitlenecekKisim / taksitSayisi));

                List<InstallmentListDto> installmentListDtos = new List<InstallmentListDto>();
                for (byte i = 1; i <= taksitSayisi; i++)
                {
                    InstallmentListDto installment = new InstallmentListDto()
                    {
                        TaksitNo = i,
                        Aciklama = $"{i} nolu taksit",
                        TaksitTarihi = DateTime.Now.AddMonths(i),
                        TaksitTutari = Taksit
                    };
                    installmentListDtos.Add(installment);
                }
                // kusurat arttıysa peşinata ekleyelim
                var artanKusurat = taahhutTutari - (pesinatTutari + (Taksit * taksitSayisi));
                if (artanKusurat > 0)
                {
                    pesinatTutari = Math.Round((pesinatTutari + artanKusurat));
                }
                // peşinatı da listeye ekleyelim
                InstallmentListDto pesinat = new InstallmentListDto()
                {
                    TaksitNo = 0,
                    Aciklama = "Peşinat tutarı",
                    TaksitTarihi = DateTime.Now,
                    TaksitTutari = pesinatTutari
                };
                installmentListDtos.Add(pesinat);
                // oluşan listeyi dönelim
                return Json(new { success = true, data = installmentListDtos.OrderBy(x => x.TaksitNo).ToList() });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveInstallments(List<InstallmentSaveDto> installments)
        {
            if (installments.Count > 0)
            {
                List<CurrentAccountDetail> currentAccountDetails = new List<CurrentAccountDetail>();
                foreach (var item in installments)
                {
                    CurrentAccountDetail currentAccountDetail = new CurrentAccountDetail()
                    {
                        CurrentAccountId = SD.CurAccountId,
                        CurDate = item.TaksitTarihi,
                        Description = item.Aciklama,
                        Debit = item.TaksitTutari,
                        Credit = 0,
                        ProjectId = SD.ProjectId,
                        ProjectInstallmentNo = item.TaksitNo,
                        CreatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier),
                        CreatedDate = DateTime.Now
                    };
                    currentAccountDetails.Add(currentAccountDetail);
                }
                if (currentAccountDetails.Count > 0)
                { 
                    await _currentAccountDetailService.AddRangeAsync(currentAccountDetails);
                    return Json(new { success = true });
                }

            }
            return Json(new { success = false });
        }
    }
}
