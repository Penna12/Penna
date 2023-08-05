using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Extensions;
using Penna.Core.Utilities.Constants;
using Penna.Core.Utilities.Enums;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    //[ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IProductInOutService _productInOutService;

        public ProductController(IProductService productService, IMapper mapper, 
            IProductInOutService productInOutService)
        {
            _productService = productService;
            _mapper = mapper;
            _productInOutService = productInOutService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "ProductList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Malzemeler" };
            Toolbar.Urls = new[] { "/", "#" };
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            TempData["active"] = "ProductList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "<a href='/Product/Index' class='btn btn-link'>Malzemeler</a>", "Yeni Malzeme" };
            Toolbar.Urls = new[] { "/", "#" };

            ProductDto productDto = new ProductDto()
            {
                UnitList = _productService.GetUnitListForDropDown(),
                BusinessGroupList = _productService.GetBusinessGroupListForDropDown(),
                StatusEnumList = _productService.GetStatusListForDropDown()
            };

            if (id == null)
            {
                productDto.Product.ProjectId = SD.ProjectId;
                productDto.Product.Status = StatusEnum.Aktif;
                return View(productDto);
            }

            productDto.Product = await _productService.SingleOrDefaultAsync(x => x.Id == id, includeProperties: "Unit");
            if (productDto.Product == null)
            {
                return NotFound();
            }

            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                //if (CheckProductNameIfExists(productDto.Product.Name, productDto.Product.Id))
                //{
                //    TempData["message"] = "Bu isimde ürün var, farklı bir ürün girişi yapınız.";
                //    return View(productDto);
                //}
                if (productDto.Product.Id == 0)
                {
                    productDto.Product.ProjectId = SD.ProjectId;
                    productDto.Product.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    productDto.Product.CreatedDate = DateTime.Now;
                    await _productService.AddAsync(productDto.Product);
                }
                else
                {
                    productDto.Product.ProjectId = SD.ProjectId;
                    productDto.Product.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    productDto.Product.UpdatedDate = DateTime.Now;
                    _productService.Update(productDto.Product);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }


        public IActionResult ProductIn(int id)
        {
            TempData["active"] = "ProductIn";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Malzeme Giriş" };
            Toolbar.Urls = new[] { "/", "#" };
            //-----------------------------------
            var model = new ProductInOutDto();
            model.Input_fl = true;
            model.ProductInOut.ProductId = id;
            model.ProductList = _productService.GetProductListForDropDown(SD.ProjectId);
            return View("InOut", model);
        }

        public IActionResult ProductOut(int id)
        {
            TempData["active"] = "ProductOut";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Malzeme Çıkış" };
            Toolbar.Urls = new[] { "/", "#" };
            //-----------------------------------
            var model = new ProductInOutDto();
            model.Input_fl = false;
            model.ProductInOut.ProductId = id;
            model.ProductList = _productService.GetProductListForDropDown(SD.ProjectId);
            return View("InOut", model);
        }

        public async Task<IActionResult> SaveProductInOut(ProductInOutDto productInOutDto)
        {
            if (ModelState.IsValid)
            {
                ProductInOut model = productInOutDto.ProductInOut;
                model.TransactionDate = DateTime.Now;
                model.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier); //User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model.CreatedDate = DateTime.Now;
                await _productInOutService.AddAsync(model);

                // Product tablosu miktar alanını güncelleyelim
                var product = await _productService.GetByIdAsync(model.ProductId);
                if (productInOutDto.Input_fl)
                    product.Quantity += model.Quantity;
                else
                    product.Quantity -= model.Quantity;
                _productService.Update(product);

                return RedirectToAction(nameof(Index));
            }
            productInOutDto.ProductList = _productService.GetProductListForDropDown(SD.ProjectId);
            return View("InOut", productInOutDto);
        }


        #region Calling Json data
        //private bool CheckProductNameIfExists(string name, int id = 0)
        //{
        //    if (_productService.SingleOrDefaultAsync(c => c.Name == name && (id == 0 || c.Id != id)) != null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.SingleOrDefaultAsync(p => p.Id == id, includeProperties:"Unit");
            return Json(new { data = product }); 
        }

        public async Task<IActionResult> GetAllData()
        {
            var list = await _productService.GetAllAsync(includeProperties: "Unit");
            return Json(new { success = list != null, data = list });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Data silinirken bir hata oldu." });
            }
            _productService.Remove(product);
            return Json(new { success = true, message = "Data başarıyla silindi." });
        }

        [HttpGet]
        public IActionResult GetBusinessGroupEnumList()
        {
            var list = EnumExtensions.GetAttributeList(typeof(BusinessGroupEnum));
            return Json(list);
        }

        #endregion Calling Json data
    }
}
