using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class LaborController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILaborService _laborService;
        public LaborController(IMapper mapper, ILaborService laborService)
        {
            _mapper = mapper;
            _laborService = laborService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "LaborList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "İşçilikler" };
            Toolbar.Urls = new[] { "/", "#" };

            return View();
        }

        #region Calling Json data
        public async Task<IActionResult> GetAllData()
        {
            var list = await _laborService.GetAllAsync();
            return Json(new { success = list != null, data = _mapper.Map<IEnumerable<LaborViewDto>>(list) });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var labor = await _laborService.GetByIdAsync(id);
            return Json(new { success = labor != null, data = _mapper.Map<LaborViewDto>(labor) });
        }


        [HttpPost]
        public async Task<IActionResult> Create(LaborAddDto laborAddDto)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Labor>(laborAddDto);
                model.TenantId = Convert.ToInt32(User.FindFirst("TenantId")?.Value);
                model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).ToString();
                model.CreatedDate = DateTime.Now;
                await _laborService.AddAsync(model);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(LaborEditDto laborEditDto)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Labor>(laborEditDto);
                model.TenantId = Convert.ToInt32(User.FindFirst("TenantId")?.Value);
                model.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier).ToString();
                model.UpdatedDate = DateTime.Now;
                _laborService.Update(model);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var labor = await _laborService.GetByIdAsync(id);
            if (labor == null)
            {
                return Json(new { success = false });
            }
            _laborService.Remove(labor);
            return Json(new { success = true });
        }

        #endregion Calling Json data
    }
}
