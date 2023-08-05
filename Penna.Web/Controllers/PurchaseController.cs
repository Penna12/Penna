using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Extensions;
using Penna.Core.Utilities.Constants;
using Penna.Core.Utilities.EmailService.Microsoft;
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
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectSelectedCheckFilter))]
    public class PurchaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly IPurchaseRequestService _purchaseRequestService;
        private readonly IPurchaseService _purchaseService;
        private readonly IPurchaseTenderService _purchaseTenderService;
        private readonly IProductService _productService;
        private readonly IBlockService _blockService;
        private readonly ICurrentAccountService _currentAccountService;

        public PurchaseController(IMapper mapper,
            IBlockService blockService, IProductService productService,
            IPurchaseRequestService purchaseRequestService,
            IPurchaseService purchaseService,
            IPurchaseTenderService purchaseTenderService,
            ICurrentAccountService currentAccountService,  
            IMailService mailService)
        {
            _mapper = mapper;
            _productService = productService;
            _blockService = blockService;
            _purchaseRequestService = purchaseRequestService;
            _purchaseService = purchaseService;
            _purchaseTenderService = purchaseTenderService;
            _currentAccountService = currentAccountService;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "PurchaseList";
            Toolbar.Title = "Satın Alma Talepleri";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Satın Alma Talepleri" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            PurchaseDto purchaseDto = new PurchaseDto();
            purchaseDto.ProductList = _productService.GetProductListForDropDown(SD.ProjectId);
            //purchaseDto.SupplierList = _currentAccountService.GetCurrentAccountListForDropDown(SD.TenantId, CurrentAccountTypeEnum.Vendor);
            purchaseDto.BlockList = _blockService.GetBlockListForDropDown(SD.ProjectId);
            purchaseDto.BusinessGroupList = _productService.GetBusinessGroupListForDropDown();

            return View(purchaseDto);
        }

        [HttpGet]
        public IActionResult SendRequest()
        {
            TempData["active"] = "PurchaseRequest";
            Toolbar.Title = "Satın Alma Talebi Gönder";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Satın Alma Talebi Gönder" };
            Toolbar.Urls = new[] { "/", "#" };

            PurchaseRequestDto purchaseRequestDto = new PurchaseRequestDto();
            purchaseRequestDto.ProductList = _productService.GetProductListForDropDown(SD.ProjectId);

            return View(purchaseRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> SendRequest(PurchaseRequestDto model)
        {
            if (ModelState.IsValid)
            {
                model.PurchaseRequest.ProjectId = SD.ProjectId;
                model.PurchaseRequest.RemainingQuantity = model.PurchaseRequest.Quantity;
                model.PurchaseRequest.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.PurchaseRequest.CreatedDate = DateTime.Now;
                await _purchaseRequestService.AddAsync(model.PurchaseRequest);
                // =============================================================
                var product = await _productService.SingleOrDefaultAsync(p => p.Id == model.PurchaseRequest.ProductId, includeProperties: "Unit");
                TempData["message"] = $"{model.PurchaseRequest.Quantity} {product.Unit.Name}, {product.Name} talebiniz alınmıştır.";
                return RedirectToAction(nameof(SendRequest));
            }
            return View(model);
        }

        public async Task<IActionResult> DirectPurchase(int id)
        {
            TempData["active"] = "PurchaseList";
            Toolbar.Title = "Doğrudan Satın Alma";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Doğrudan Satın Alma" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            PurchaseDto purchaseDto = new PurchaseDto();
            purchaseDto.PurchaseRequest = await _purchaseRequestService.GetByIdAsync(id);
            purchaseDto.Purchase.PurchaseType = PurchaseTypeEnum.Direct;
            purchaseDto.Purchase.PurchaseRequestId = id;
            purchaseDto.Purchase.ProductId = purchaseDto.PurchaseRequest.ProductId;
            purchaseDto.Purchase.Quantity = purchaseDto.PurchaseRequest.RemainingQuantity;
            purchaseDto.ProductList = _productService.GetProductListForDropDown(SD.ProjectId, purchaseDto.PurchaseRequest.ProductId);
            //purchaseDto.SupplierList = _currentAccountService.GetCurrentAccountListForDropDown(SD.TenantId, CurrentAccountTypeEnum.Vendor);
            purchaseDto.BlockList = _blockService.GetBlockListForDropDown(SD.ProjectId);
            purchaseDto.BusinessGroupList = _productService.GetBusinessGroupListForDropDown();

            return View(purchaseDto);
        }

        public async Task<IActionResult> OfferPurchase(int id)
        {
            TempData["active"] = "PurchaseList";
            Toolbar.Title = "Teklif Usulü Satın Alma";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Teklif Usulü Satın Alma" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            PurchaseDto purchaseDto = new PurchaseDto();
            purchaseDto.PurchaseRequest = await _purchaseRequestService.GetByIdAsync(id);
            purchaseDto.Purchase.PurchaseType = PurchaseTypeEnum.Offer;
            purchaseDto.Purchase.PurchaseRequestId = id;
            purchaseDto.Purchase.ProductId = purchaseDto.PurchaseRequest.ProductId;
            purchaseDto.Purchase.Quantity = purchaseDto.PurchaseRequest.RemainingQuantity;
            purchaseDto.ProductList = _productService.GetProductListForDropDown(SD.ProjectId);
            //purchaseDto.SupplierList = _currentAccountService.GetCurrentAccountListForDropDown(SD.TenantId, CurrentAccountTypeEnum.Vendor);
            purchaseDto.BlockList = _blockService.GetBlockListForDropDown(SD.ProjectId);
            purchaseDto.BusinessGroupList = _productService.GetBusinessGroupListForDropDown();

            return View(purchaseDto);
        }

        public async Task<IActionResult> TenderPurchase(int id)
        {
            TempData["active"] = "PurchaseList";
            Toolbar.Title = "İhale Usulü Satın Alma";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "İhale Usulü Satın Alma" };
            Toolbar.Urls = new[] { "/", "#" };
            // ---------------------------------------------------------------
            PurchaseDto purchaseDto = new PurchaseDto();
            purchaseDto.PurchaseRequest = await _purchaseRequestService.GetByIdAsync(id);
            purchaseDto.Purchase.PurchaseType = PurchaseTypeEnum.Tender;
            purchaseDto.Purchase.PurchaseRequestId = id;
            purchaseDto.Purchase.ProductId = purchaseDto.PurchaseRequest.ProductId;
            purchaseDto.Purchase.Quantity = purchaseDto.PurchaseRequest.RemainingQuantity;
            purchaseDto.ProductList = _productService.GetProductListForDropDown(SD.ProjectId);
            //purchaseDto.SupplierList = _currentAccountService.GetCurrentAccountListForDropDown(SD.TenantId, CurrentAccountTypeEnum.Vendor);
            purchaseDto.BlockList = _blockService.GetBlockListForDropDown(SD.ProjectId);
            purchaseDto.BusinessGroupList = _productService.GetBusinessGroupListForDropDown();

            return View(purchaseDto);
        }

        public async Task<IActionResult> PendingDelivery()
        {
            TempData["active"] = "PendingDelivery";
            Toolbar.Title = "Teslim Bekleyen Alımlar";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Teslim Bekleyen Alımlar" };
            Toolbar.Urls = new[] { "/", "#" };

            var liste = await _purchaseService.Where(
                p => p.ProjectId == SD.ProjectId && 
                p.Delivered == false && 
                p.PurchaseStatus == PurchaseStatusEnum.Bought, 
                includeProperties: "Product,Product.Unit,PurchaseRequest");
            return View(liste);
        }

        public async Task<IActionResult> PendingOffer()
        {
            TempData["active"] = "PendingOffer";
            Toolbar.Title = "Teklif Bekleyen Siparişler";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Teklif Bekleyen Siparişler" };
            Toolbar.Urls = new[] { "/", "#" };

            var liste = await _purchaseService.Where(
                p => p.ProjectId == SD.ProjectId && 
                p.PurchaseType == PurchaseTypeEnum.Offer && 
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                //p.FinalBidDateTime.Value > DateTime.Now && 
                p.SupplierCurrentAccountId == null, 
                includeProperties: "Product,Product.Unit,PurchaseRequest");

            return View(liste);
        }

        public async Task<IActionResult> Tender()
        {
            TempData["active"] = "Tender";
            Toolbar.Title = "İhaleler";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "İhaleler" };
            Toolbar.Urls = new[] { "/", "#" };

            var liste = await _purchaseService.Where(
                p => p.ProjectId == SD.ProjectId && 
                p.PurchaseType == PurchaseTypeEnum.Tender && 
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                //p.FinalBidDateTime.Value > DateTime.Now &&
                p.SupplierCurrentAccountId == null, 
                includeProperties: "Product,Product.Unit,PurchaseRequest");

            return View(liste);
        }

        public async Task<IActionResult> OfferStatus(int id)
        {
            TempData["active"] = "OfferStatus";
            Toolbar.Title = "Teklif Durumu";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Teklif Durumu" };
            Toolbar.Urls = new[] { "/", "#" };

            var purchase = await _purchaseService.SingleOrDefaultAsync(
                p => p.Id == id && 
                p.PurchaseType == PurchaseTypeEnum.Offer, 
                includeProperties: "Product,Product.Unit,PurchaseRequest,PurchaseTenders,PurchaseTenders.CurrentAccount");
            return View(purchase);
        }

        public async Task<IActionResult> OfferClose(int id)
        {
            TempData["active"] = "OfferClose";
            Toolbar.Title = "Teklif Kapama";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Teklif Kapama" };
            Toolbar.Urls = new[] { "/", "#" };

            PurchaseTender purchaseTender = await _purchaseTenderService.SingleOrDefaultAsync(
                t => t.Id == id,
                includeProperties: "Purchase,CurrentAccount,CurrentAccount.Tenant");

            if (purchaseTender.CurrentAccount.TenantId != SD.TenantId)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            OfferCloseDto offerCloseDto = new OfferCloseDto();
            offerCloseDto.PurchaseId = purchaseTender.PurchaseId;
            offerCloseDto.PurchaseTenderId = purchaseTender.Id;
            offerCloseDto.PurchaseType = purchaseTender.Purchase.PurchaseType;
            offerCloseDto.CompanyName = purchaseTender.CurrentAccount.CompanyName;
            return View(offerCloseDto);
        }

        [HttpPost]
        public async Task<IActionResult> OfferClose(OfferCloseDto offerCloseDto)
        {
            if (ModelState.IsValid)
            {
                var purchaseTender = await _purchaseTenderService.GetByIdAsync(offerCloseDto.PurchaseTenderId);
                purchaseTender.Won = true;
                purchaseTender.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                purchaseTender.UpdatedDate = DateTime.Now;
                _purchaseTenderService.Update(purchaseTender);

                var purchase = await _purchaseService.GetByIdAsync(offerCloseDto.PurchaseId);
                purchase.InvoiceNo = offerCloseDto.InvoiceNo;
                purchase.InvoiceDate = offerCloseDto.InvoiceDate;
                purchase.InvoiceAmount = offerCloseDto.InvoiceAmount;
                purchase.Deadline = offerCloseDto.Deadline;
                purchase.PurchaseStatus = PurchaseStatusEnum.Bought;
                purchase.SupplierCurrentAccountId = purchaseTender.SupplierCurrentAccountId;
                purchase.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                purchase.UpdatedDate = DateTime.Now;
                _purchaseService.Update(purchase);
            }

            if (offerCloseDto.PurchaseType == PurchaseTypeEnum.Offer)
                return RedirectToAction("PendingOffer", "Purchase");
            else
                return RedirectToAction("Tender", "Purchase");
        }

        public async Task<IActionResult> TenderMonitoring(int id)
        {
            TempData["active"] = "TenderMonitoring";
            Toolbar.Title = "İhale izleme";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "İhale İzleme" };
            Toolbar.Urls = new[] { "/", "#" };

            var purchase = await _purchaseService.SingleOrDefaultAsync(
                p => p.Id == id && 
                p.PurchaseType == PurchaseTypeEnum.Tender, 
                includeProperties: "Product,Product.Unit,PurchaseRequest,PurchaseTenders,PurchaseTenders.CurrentAccount");
            
            return View(purchase);
        }

        public async Task<IActionResult> TenderClose(int id)
        {
            TempData["active"] = "TenderClose";
            Toolbar.Title = "İhale Kapama";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "İhale Kapama" };
            Toolbar.Urls = new[] { "/", "#" };

            var purchase = await _purchaseService.SingleOrDefaultAsync(
                p => p.Id == id && 
                p.PurchaseType == PurchaseTypeEnum.Tender, 
                includeProperties: "Product,Product.Unit,PurchaseRequest,PurchaseTenders,CurrentAccount");
            return View(purchase);
        }

        public IActionResult PurchaseReport()
        {
            TempData["active"] = "PurchaseReport";
            Toolbar.Title = "Satın Alma Raporu";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Satın Alma Raporu" };
            Toolbar.Urls = new[] { "/", "#" };

            return View();
        }



        #region Calling Json data
        public IActionResult GetPriorityEnumList()
        {
            return Json(new { list = EnumExtensions.GetAttributeList(typeof(PriorityEnum)) });
        }

        public async Task<IActionResult> GetAllData()
        {
            var list = await _purchaseRequestService
                .Where(p => p.ProjectId == SD.ProjectId && 
                (  !p.Purchases.Any(x => x.ProductId == p.ProductId) || 
                    p.Purchases.Where(x => x.ProductId == p.ProductId).Sum(x => x.Quantity) < p.Quantity ), 
                    includeProperties: "Product,Product.Unit");
            return Json(new { data = list });
        }

        public async Task<IActionResult> GetBlockName(int id)
        {
            var block = await _blockService.SingleOrDefaultAsync(b => b.Id == id, includeProperties: "Project");
            return Json(new { data = block });
        }

        public async Task<IActionResult> GetSupplierCurrentAccount(BusinessGroupEnum id)
        {
            var accounts = await _currentAccountService.Where(
                                    c => c.BusinessGroupId == id &&
                                    c.TenantId == SD.TenantId &&
                                    c.AccountTypeId == CurrentAccountTypeEnum.Vendor);
            return Json(new { data = accounts });
        }


        [HttpPost]
        public async Task<IActionResult> SavePurchase(PurchaseDto purchaseDto, int[] supplierList, int sendMail)
        {
            if (ModelState.IsValid)
            {
                purchaseDto.Purchase.ProjectId = SD.ProjectId;
                purchaseDto.Purchase.PurchaseStatus = (purchaseDto.Purchase.PurchaseType == PurchaseTypeEnum.Direct) 
                    ? PurchaseStatusEnum.Bought : PurchaseStatusEnum.Pending;
                purchaseDto.Purchase.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                purchaseDto.Purchase.CreatedDate = DateTime.Now;

                await _purchaseService.AddAsync(purchaseDto.Purchase);

                if (purchaseDto.Purchase.PurchaseType == PurchaseTypeEnum.Direct)
                {
                    var request = await _purchaseRequestService.SingleOrDefaultAsync(r => r.Id == purchaseDto.Purchase.PurchaseRequestId && r.ProductId == purchaseDto.Purchase.ProductId);
                    if (request != null)
                    {
                        request.RemainingQuantity = (request.RemainingQuantity >= purchaseDto.Purchase.Quantity)
                            ? request.RemainingQuantity - purchaseDto.Purchase.Quantity : 0;
                        request.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        request.UpdatedDate = DateTime.Now;
                        _purchaseRequestService.Update(request);
                    }
                }
                else if (purchaseDto.Purchase.PurchaseType == PurchaseTypeEnum.Offer || purchaseDto.Purchase.PurchaseType == PurchaseTypeEnum.Tender)
                {
                    foreach (var supplier in supplierList)
                    {
                        PurchaseTender purchaseTender = new PurchaseTender()
                        {
                            CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier),
                            CreatedDate = DateTime.Now,
                            PurchaseId = purchaseDto.Purchase.Id,
                            Amount = 0,
                            Joined = false,
                            Won = false,
                            SupplierCurrentAccountId = supplier
                        };
                        await _purchaseTenderService.AddAsync(purchaseTender);
                        if (sendMail == 1)
                        {
                            CurrentAccount currentAccount = await _currentAccountService.GetByIdAsync(supplier);
                            if (currentAccount != null)
                            {
                                string style = @"<style> body { font-size:1.2rem;color:#343a40; } * { padding:5px 10px } p { font-size:1.2rem; color:#343a40; } span { font-weight:bold; color:#ad2831 } </style>";
                                var product = await _productService.SingleOrDefaultAsync(p => p.Id == purchaseDto.Purchase.ProductId, includeProperties:"Unit");
                                string Body = $"<html><head><title></title>{style}</head><body>" +
                                    "<p>Sayın Tedarikçimiz, </p>" +
                                    $"<p>Aşağıda listelenen ürünler için <span>{purchaseDto.Purchase.FinalBidDateTime.Value}</span> tarihine kadar, sisteme giriş yaparak teklifinizi veriniz.</p>" +
                                    $"<p>Talep Edilen Yer: {purchaseDto.Purchase.RequestedPlace} </p>" +
                                    "<table style='width:600px'>" +
                                    "<thead><tr style='background-color:#0077B6;color:#f1faee'><th style='width:70%;text-align: left;'>Ürün</th><th style='width:30%;text-align: left;'>Miktar</th></tr></thead>" +
                                    $"<tbody><tr style='background-color:#dee2ff;color:#1D3557'><td>{product.Name}</td><td>{purchaseDto.Purchase.Quantity} {product.Unit.Name}</td></tr></tbody>" +
                                    "</table>" +
                                    "</body></html>";
                                string Subject = "Teklif bildirimi!";
                                string ToEmails = currentAccount.Email;
                                await _mailService.SendEmailAsync(ToEmails, Subject, Body);
                            }
                            
                        }
                    }
                }
            }
            return Json(new { success = true });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var purchase = await _purchaseRequestService.GetByIdAsync(id);
            if (purchase == null)
            {
                return Json(new { success = false, message = "Data silinirken bir hata oldu." });
            }
            _purchaseRequestService.Remove(purchase);
            return Json(new { success = true, message = "Data başarıyla silindi." });
        }

        public async Task<IActionResult> GetPendingOfferList()
        {
            var liste = await _purchaseService.Where(
                p => p.ProjectId == SD.ProjectId && 
                p.PurchaseType == PurchaseTypeEnum.Offer && 
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                //p.FinalBidDateTime.Value > DateTime.Now &&
                p.SupplierCurrentAccountId == null, 
                includeProperties: "Product,Product.Unit,PurchaseRequest");
            return Json(new { data = liste });
        }

        public async Task<IActionResult> GetTenderList()
        {
            var liste = await _purchaseService.Where(
                p => p.ProjectId == SD.ProjectId && 
                p.PurchaseType == PurchaseTypeEnum.Tender && 
                p.PurchaseStatus == PurchaseStatusEnum.Pending &&
                //p.FinalBidDateTime.Value > DateTime.Now &&
                p.SupplierCurrentAccountId == null, 
                includeProperties: "Product,Product.Unit,PurchaseRequest");
            return Json(new { data = liste });
        }

        public async Task<IActionResult> GetOfferStatusList()
        {
            var liste = await _purchaseService.Where(
                p => p.ProjectId == SD.ProjectId && 
                p.PurchaseType == PurchaseTypeEnum.Tender && 
                p.PurchaseStatus == PurchaseStatusEnum.Pending, 
                includeProperties: "Product,Product.Unit,PurchaseRequest");
            return Json(new { data = liste });
        }

        public async Task<IActionResult> GetPendingDeliveryList()
        {
            var liste = await _purchaseService.Where(
                p => p.ProjectId == SD.ProjectId && 
                p.Delivered == false && 
                p.PurchaseStatus == PurchaseStatusEnum.Bought, 
                includeProperties: "Product,Product.Unit,PurchaseRequest");
            return Json(new { data = liste });
        }
        #endregion Calling Json data
    }
}
