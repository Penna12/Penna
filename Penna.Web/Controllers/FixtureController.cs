using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Entities.DTOs;
using Penna.Business.Interfaces;
using Penna.Entities.Models;
using System.Threading.Tasks;
using Penna.Core.Utilities.Constants;
using System.Collections.Generic;
using System;
using Penna.Core.Extensions;
using System.Security.Claims;
using System.Linq;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class FixtureController : Controller
    {
        public readonly IFixtureService _fixtureService;
        public readonly IFixtureEmbezzledService _fixtureEmbezzledService;
        private readonly IAppUserService _appUserService;

        public FixtureController(IFixtureService fixtureService, 
            IFixtureEmbezzledService fixtureEmbezzledService, 
            IAppUserService appUserService)
        {
            _fixtureService = fixtureService;
            _fixtureEmbezzledService = fixtureEmbezzledService;
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            TempData["active"] = "FixtureList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Demirbaşlar" };
            Toolbar.Urls = new[] { "/", "#" };
            //===========================================================
            var fixture = new FixtureDto();
            fixture.Fixtures = (List<Fixture>)await _fixtureService.Where(f => f.ProjectId == SD.ProjectId);
            if(id != null)
            {
                fixture.Fixture = await _fixtureService.GetByIdAsync(id.GetValueOrDefault());
            } else
            {
                fixture.Fixture = new Fixture();
            }

            return View(fixture);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FixtureDto fixtureDto)
        {
            TempData["active"] = "FixtureCreate";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Demirbaş" };
            Toolbar.Urls = new[] { "/", "#" };
            //----------------------------------------------------------
            if (ModelState.IsValid)
            {
                if (fixtureDto.Fixture.Id == 0)
                {
                    fixtureDto.Fixture.ProjectId = SD.ProjectId;
                    fixtureDto.Fixture.CreatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    fixtureDto.Fixture.CreatedDate = DateTime.Now;
                    await _fixtureService.AddAsync(fixtureDto.Fixture);
                } else
                {
                    fixtureDto.Fixture.UpdatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    fixtureDto.Fixture.UpdatedDate = DateTime.Now;
                    _fixtureService.Update(fixtureDto.Fixture);
                }
            }
            //fixtureDto.Fixtures = (List<Fixture>)await _fixtureService.Where(f => f.ProjectId == SD.ProjectId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Embezzled(int? id)
        {
            TempData["active"] = "FixtureEmbezzled";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Demirbaş Zimmetleme" };
            Toolbar.Urls = new[] { "/", "#" };
            //===========================================================
            var embezzled = new EmbezzledDto();
            embezzled.Fixtures = (List<Fixture>)await _fixtureService.Where(f => f.ProjectId == SD.ProjectId);
            embezzled.AppUsers = _appUserService.GetUserListForDropDown(SD.TenantId);
            if (id != null)
            {
                embezzled.Fixture = await _fixtureService.GetByIdAsync(id.GetValueOrDefault());
                embezzled.FixtureEmbezzled = new FixtureEmbezzled() { FixtureId = id.GetValueOrDefault() };
            }
            else
            {
                embezzled.Fixture = new Fixture();
                embezzled.FixtureEmbezzled = new FixtureEmbezzled();
            }

            return View(embezzled);
        }

        [HttpPost]
        public async Task<IActionResult> EmbezzledPost(EmbezzledDto embezzledDto)
        {
            TempData["active"] = "FixtureEmbezzled";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Demirbaş Zimmetleme" };
            Toolbar.Urls = new[] { "/", "#" };
            //----------------------------------------------------------
            if (ModelState.IsValid)
            {
                if (embezzledDto.FixtureEmbezzled.Id == 0)
                {
                    embezzledDto.FixtureEmbezzled.CreatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    embezzledDto.FixtureEmbezzled.CreatedDate = DateTime.Now;
                    await _fixtureEmbezzledService.AddAsync(embezzledDto.FixtureEmbezzled);
                    var fixture = await _fixtureService.GetByIdAsync(embezzledDto.FixtureEmbezzled.FixtureId);
                    fixture.Quantity = (fixture.Quantity - embezzledDto.FixtureEmbezzled.Quantity);
                    _fixtureService.Update(fixture);
                }
                else
                {
                    embezzledDto.FixtureEmbezzled.UpdatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                    embezzledDto.FixtureEmbezzled.UpdatedDate = DateTime.Now;
                    _fixtureEmbezzledService.Update(embezzledDto.FixtureEmbezzled);
                }
            }

            return RedirectToAction("Embezzled");
        }


        #region Call Api
        public async Task<IActionResult> GetEmbezzledList(int id)
        {
            var list = await _fixtureEmbezzledService.GetAllByFixtureIdAsync(id);
            return Json(new { data = list.Where(x => x.ReturnDate == null) });
        }

        public async Task<IActionResult> SetReturnEmbezzled(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false });
            }

            try
            {
                var embezzled = await _fixtureEmbezzledService.GetByIdAsync(id);
                var iadeMiktari = embezzled.Quantity;
                var fixtureId = embezzled.FixtureId;
                embezzled.ReturnDate = DateTime.Now;
                embezzled.UpdatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                embezzled.UpdatedDate = DateTime.Now;
                _fixtureEmbezzledService.Update(embezzled);

                var fixture = await _fixtureService.GetByIdAsync(fixtureId);
                fixture.Quantity = (fixture.Quantity + iadeMiktari);
                fixture.UpdatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                fixture.UpdatedDate = DateTime.Now;
                _fixtureService.Update(fixture);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        public async Task<IActionResult> DeleteEmbezzled(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false });
            }

            try
            {
                var embezzled = await _fixtureEmbezzledService.GetByIdAsync(id);
                var iadeMiktari = embezzled.Quantity;
                var fixtureId = embezzled.FixtureId;
                _fixtureEmbezzledService.Remove(embezzled);

                var fixture = await _fixtureService.GetByIdAsync(fixtureId);
                fixture.Quantity = (fixture.Quantity + iadeMiktari);
                fixture.UpdatedBy = User.GetClaimValue(ClaimTypes.NameIdentifier);
                fixture.UpdatedDate = DateTime.Now;
                _fixtureService.Update(fixture);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
        #endregion
    }
}
