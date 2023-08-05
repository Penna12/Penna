using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class ConcreteController : Controller
    {
        private readonly IConcreteRequestService _concreteRequestService;
        private readonly IConcreteCastingService _concreteCastingService;
        private readonly IProductService _productService;
        private readonly IBlockService _blockService;

        public ConcreteController(IConcreteRequestService concreteRequestService,
            IConcreteCastingService concreteCastingService, 
            IProductService productService, IBlockService blockService)
        {
            _concreteRequestService = concreteRequestService;
            _concreteCastingService = concreteCastingService;
            _productService = productService;
            _blockService = blockService;
        }

        public IActionResult PurchaseRequest()
        {
            TempData["active"] = "PurchaseRequest";
            Toolbar.Title = "Beton Alım Talebi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Beton Alım Talebi" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            var model = new ConcreteRequestDto
            {
                ProductList = _productService.GetConcreteListForDropDown(SD.ProjectId),
                BlockList = _blockService.GetBlockListForDropDown(SD.ProjectId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SavePurchaseRequest(ConcreteRequestDto concreteRequestDto)
        {
            if (ModelState.IsValid)
            {
                concreteRequestDto.ConcreteRequest.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                concreteRequestDto.ConcreteRequest.CreatedDate = System.DateTime.Now;
                var addedData = await _concreteRequestService.AddAsync(concreteRequestDto.ConcreteRequest);
            }
            TempData["Message"] = "Beton alım talebi kayıt edilmiştir.";
            return RedirectToAction(nameof(TransactionTracking));
        }

        public IActionResult TransactionTracking()
        {
            TempData["active"] = "TransactionTracking";
            Toolbar.Title = "Beton İşlemleri Takip";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Beton İşlemleri Takip" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            return View();
        }

        public async Task<IActionResult> ConcreteCasting(int id)
        {
            TempData["active"] = "ConcreteCasting";
            Toolbar.Title = "Beton İşlemleri Raporu";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Beton İşlemleri Raporu" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            ConcreteCastingDto model = new ConcreteCastingDto()
            {
                ConcreteRequest = await _concreteRequestService.SingleOrDefaultAsync(r => r.Id == id, includeProperties: "Block,Product,Product.Unit,ConcreteCastings"),
                ConcreteCasting = new ConcreteCasting() //await _concreteCastingService.SingleOrDefaultAsync(c => c.ConcreteRequestId == id)
            };
            model.TotalCasting = model.ConcreteRequest.ConcreteCastings.Sum(c => c.Quantity);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveConcreteCasting(ConcreteCastingDto concreteCastingDto)
        {
            if (ModelState.IsValid)
            {
                var model = concreteCastingDto.ConcreteCasting;
                model.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.CreatedDate = System.DateTime.Now;
                var result = await _concreteCastingService.AddAsync(model);
            }
            return RedirectToAction(nameof(TransactionTracking));
        }


        public IActionResult Report()
        {
            TempData["active"] = "ConcreteReport";
            Toolbar.Title = "Beton İşlemleri Raporu";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Beton İşlemleri Raporu" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            return View();
        }


        
        #region Calling Json data
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            // Alımı yapılmış, işi bitmiş isteklerin listelenmemesi yapılmalı
            // Where şartsız çalışıyor olmalı, test yap
            var list = await _concreteRequestService.Where(r => r.Block.Project.TenantId == SD.TenantId &&
            (r.ConcreteCastings.Count == 0 || r.ConcreteCastings.Sum(x => x.Quantity) < r.Quantity), 
                includeProperties: "Block.Project,Product,Product.Unit,ConcreteCastings");
            return Json(new { data = list });
        }

        [HttpPost]
        public IActionResult Save()
        {

            return Json(new { success = true });
        }
        #endregion
    }
}
