using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class UnitController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitService _unitService;
        public UnitController(IMapper mapper, IUnitService unitService)
        {
            _mapper = mapper;
            _unitService = unitService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "UnitList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Birimler" };
            Toolbar.Urls = new[] { "/", "#" };

            return View();
        }

        #region Calling Json data
        public async Task<IActionResult> GetAllData()
        {
            var list = await _unitService.GetAllAsync();
            return Json(new { success = list != null, data = list });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            return Json(new { success = unit != null, data = unit });
        }


        [HttpPost]
        public async Task<IActionResult> Create(UnitDto unitDto)
        {
            if (ModelState.IsValid)
            {
                await _unitService.AddAsync(_mapper.Map<Unit>(unitDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(UnitDto unitDto)
        {
            if (ModelState.IsValid)
            {
                _unitService.Update(_mapper.Map<Unit>(unitDto));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null)
            {
                return Json(new { success = false });
            }
            _unitService.Remove(unit);
            return Json(new { success = true });
        }

        #endregion Calling Json data
    }
}
