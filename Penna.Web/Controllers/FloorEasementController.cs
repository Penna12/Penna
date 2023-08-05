using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
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
    public class FloorEasementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBlockService _blockService;
        private readonly IApartmentService _apartmentService;
        //private readonly IUnitOfWork _unitOfWork;

        public FloorEasementController(IMapper mapper, IBlockService blockService, IApartmentService apartmentService)
        {
            _mapper = mapper;
            _blockService = blockService;
            _apartmentService = apartmentService;
        }

        public async Task<IActionResult> Manage(int id)
        {
            TempData["active"] = "FloorEasementList";
            Toolbar.Title = "Kat Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Blok List" };
            Toolbar.Urls = new[] { "/", "#" };
            //============================================================
            // Kat irtifakı kur, önce katları oluşturalım.
            var block = await _blockService.SingleOrDefaultAsync(b => b.Id == id, includeProperties:"Project,Apartments");
            if (block == null)
            {
                return RedirectToAction("Index","Block");
            }

            SD.BlockId = id;
            SD.BlockName = block.Name;
            HttpContext.Session.SetString(SD.SESSION_KEY_BLOCK_ID, id.ToString());
            HttpContext.Session.SetString(SD.SESSION_KEY_BLOCK_NAME, block.Name);

            // Block kaydını DTO ya atama yapıyoruz
            BlockDto blockDto = new BlockDto();
            blockDto.Block = block;
            blockDto.Floors = await _apartmentService.GetBlockFloorsAsync(id);
            // Bloğun apartmanları varsa alıyoruz
            blockDto.Apartments = block.Apartments?.ToList();
            blockDto.BlockTypeEnumList = _blockService.GetBlockTypeListForDropDown();
            blockDto.CostCalculationEnumList = _blockService.GetCostCalculationListForDropDown();

            if (blockDto.Apartments?.Count() == 0)
                return View("AutoGenerateFloors", blockDto);
            else return View("Manage", blockDto); 
        }


        #region Calling Json data
        public async Task<IActionResult> GetAllData(int? floor = null)
        {
            IEnumerable<Apartment> apartment = await _apartmentService.Where(a => a.BlockId == SD.BlockId && (floor == null || a.Floor == floor), includeProperties:"CurrentAccount");
            return Json(new { success = apartment != null, data = apartment, floor = floor });
        }

        [HttpPost]
        public async Task<IActionResult> GenerateApartments(GenerateApartmentParamsDto dto)
        {
            if (ModelState.IsValid)
            {
                int olusacakDaireSayisi = (dto.FloorCount * dto.StartFloorNo * dto.NumberOfHousesOnEachFloor);
                int endLoop = olusacakDaireSayisi + dto.StartApartmentNo - 1;
                int KapiNo = dto.StartApartmentNo;
                List<Apartment> apartments = new List<Apartment>();
                for (int i = 0; i < dto.FloorCount; i++)
                {
                    for (int d = 0; d < dto.NumberOfHousesOnEachFloor; d++)
                    {
                        Apartment apartment = new Apartment()
                        {
                            CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                            CreatedDate = DateTime.Now,
                            Floor = dto.StartFloorNo+i,
                            SectionName = $"Daire {KapiNo}",
                            Gross = dto.Gross,
                            Net = dto.Net,
                            Gabari = dto.Gabari,
                            BlockId = SD.BlockId
                        };
                        apartments.Add(apartment);
                        KapiNo++;
                    }
                }
                try
                {
                    await _apartmentService.AddRangeAsync(apartments);
                    return Json(new { success = true });
                }
                catch (Exception)
                {
                    throw;
                }
                
                
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            // hiç bir yerde kullanılmadı
            var apartment = await _apartmentService.GetByIdAsync(id);
            return Json(new { success = apartment != null, data = apartment });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Apartment model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model.CreatedDate = DateTime.Now;
                await _apartmentService.AddAsync(model);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(Apartment model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model.UpdatedDate = DateTime.Now;
                _apartmentService.Update(model);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var apartment = await _apartmentService.GetByIdAsync(id);
            if (apartment == null)
            {
                return Json(new { success = false });
            }
            _apartmentService.Remove(apartment);
            return Json(new { success = true });
        }
        

        #endregion Calling Json data
    }
}
