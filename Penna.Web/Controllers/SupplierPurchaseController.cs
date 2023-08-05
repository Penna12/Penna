using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using Penna.Core.Utilities.Enums;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Tedarikçi")]
    //[ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class SupplierPurchaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseRequestService _purchaseRequestService;
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseTenderService _purchaseTenderService;
        private readonly IProductService _productService;
        private readonly IBlockService _blockService;
        private readonly ICurrentAccountService _currentAccountService;

        public SupplierPurchaseController(IMapper mapper,
            IBlockService blockService, IProductService productService,
            IPurchaseRequestService purchaseRequestService,
            IPurchaseService purchaseService,
            IPurchaseTenderService purchaseTenderService,
            ICurrentAccountService currentAccountService)
        {
            _mapper = mapper;
            _productService = productService;
            _blockService = blockService;
            _purchaseRequestService = purchaseRequestService;
            _purchaseService = purchaseService;
            _purchaseTenderService = purchaseTenderService;
            _currentAccountService = currentAccountService;
        }




        public async Task<IActionResult> Offers()
        {
            TempData["active"] = "SupplierOfferPurchase";
            Toolbar.Title = "Teklif Bekleyen Siparişler";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Teklif Bekleyen Siparişler" };
            Toolbar.Urls = new[] { "/", "#" };

            int curId = int.Parse(User.FindFirstValue("CurrentAccountId"));

            IEnumerable<Purchase> purchase = await _purchaseService.Where(
                p => p.PurchaseType == PurchaseTypeEnum.Offer &&
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                p.FinalBidDateTime.Value > DateTime.Now &&
                p.SupplierCurrentAccountId == null &&
                p.PurchaseTenders.Any(t => t.SupplierCurrentAccountId == curId),
                includeProperties: "Product,Product.Unit,PurchaseRequest,PurchaseTenders,CurrentAccount");

            return View(purchase);
        }



        public async Task<IActionResult> Tenders()
        {
            TempData["active"] = "SupplierTenderPurchase";
            Toolbar.Title = "İhaleler";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "İhaleler" };
            Toolbar.Urls = new[] { "/", "#" };

            int curId = int.Parse(User.FindFirstValue("CurrentAccountId"));

            IEnumerable<Purchase> purchase = await _purchaseService.Where(
                p => p.PurchaseType == PurchaseTypeEnum.Tender &&
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                p.FinalBidDateTime.Value > DateTime.Now &&
                p.SupplierCurrentAccountId == null &&
                p.PurchaseTenders.Any(t => t.SupplierCurrentAccountId == curId),
                includeProperties: "Product,Product.Unit,PurchaseRequest,PurchaseTenders,CurrentAccount");

            return View(purchase);
        }

        public async Task<IActionResult> GiveOffer(int id)
        {
            TempData["active"] = "SupplierOfferPurchase";
            Toolbar.Title = "Teklif Bekleyen Siparişler";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Teklif Bekleyen Siparişler" };
            Toolbar.Urls = new[] { "/", "#" };

            int curId = int.Parse(User.FindFirstValue("CurrentAccountId"));
            IEnumerable<Purchase> purchase = await _purchaseService.Where(
                p => p.PurchaseType == PurchaseTypeEnum.Offer &&
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                p.FinalBidDateTime.Value > DateTime.Now &&
                p.SupplierCurrentAccountId == null &&
                p.PurchaseTenders.Any(t => t.SupplierCurrentAccountId == curId),
                includeProperties: "PurchaseTenders"); //,Product,Product.Unit,PurchaseRequest,CurrentAccount

            PurchaseTender offer = await _purchaseTenderService.SingleOrDefaultAsync(
                t => t.PurchaseId == id &&
                t.Purchase.PurchaseType == PurchaseTypeEnum.Offer &&
                t.Purchase.FinalBidDateTime.Value > DateTime.Now &&
                t.SupplierCurrentAccountId == curId,
                includeProperties: "Purchase,Purchase.Product,Purchase.Product.Unit");


            if (purchase.Count() == 0 || offer == null)
                return RedirectToAction(nameof(Offers));

            return View(offer);
        }

        public async Task<IActionResult> JoinTender(int id)
        {
            TempData["active"] = "SupplierTenderPurchase";
            Toolbar.Title = "İhaleler";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "İhaleler" };
            Toolbar.Urls = new[] { "/", "#" };

            int curId = int.Parse(User.FindFirstValue("CurrentAccountId"));
            IEnumerable<Purchase> purchase = await _purchaseService.Where(
                p => p.Id == id && 
                p.PurchaseType == PurchaseTypeEnum.Tender &&
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                p.FinalBidDateTime.Value > DateTime.Now &&
                p.SupplierCurrentAccountId == null &&
                p.PurchaseTenders.Any(t => t.SupplierCurrentAccountId == curId),
                includeProperties: "PurchaseTenders");

            PurchaseTender tender = await _purchaseTenderService.SingleOrDefaultAsync(
                t => t.PurchaseId == id && 
                t.Purchase.PurchaseType == PurchaseTypeEnum.Tender &&
                t.Purchase.FinalBidDateTime.Value > DateTime.Now &&
                t.SupplierCurrentAccountId == curId,
                includeProperties: "Purchase,Purchase.Product,Purchase.Product.Unit");

            

            if (purchase.Count() == 0 || tender == null)
                return RedirectToAction(nameof(Tenders));
            
            return View(tender);
        }

        public async Task<IActionResult> Save(PurchaseTender purchaseTender, PurchaseTypeEnum purchaseType)
        {
            if (purchaseTender.Id > 0)
            {
                PurchaseTender tender = await _purchaseTenderService.SingleOrDefaultAsync(
                    t => t.Id == purchaseTender.Id &&
                    t.Purchase.PurchaseType == purchaseType &&
                    t.Purchase.FinalBidDateTime.Value > DateTime.Now &&
                    t.SupplierCurrentAccountId == purchaseTender.SupplierCurrentAccountId);

                tender.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                tender.UpdatedDate = DateTime.Now;
                tender.OfferTime = DateTime.Now;
                tender.Amount = purchaseTender.Amount;
                tender.Joined = true;
                _purchaseTenderService.Update(tender);
            }

            if (purchaseType == PurchaseTypeEnum.Offer)
                return RedirectToAction(nameof(Offers));
            else
                return RedirectToAction(nameof(Tenders));
        }

    }
}
