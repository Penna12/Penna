using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class VendorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICurrentAccountService _currentAccountService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public VendorController(ICurrentAccountService currentAccountService, IMapper mapper, IUserService userService, IAccountService accountService)
        {
            _currentAccountService = currentAccountService;
            _mapper = mapper;
            _userService = userService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            TempData["active"] = "VendorList";
            Toolbar.Title = "Kontrol Panel";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Tedarikçiler" };
            Toolbar.Urls = new[] { "/", "#" };
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            CurrentAccountDto currentAccountDto = new CurrentAccountDto()
            {
                CountryList = _currentAccountService.GetCountryListForDropDown()
            };

            if (id == null)
            {
                currentAccountDto.CurrentAccount = new CurrentAccount();
                return View(currentAccountDto); 
            }

            currentAccountDto.CurrentAccount = await _currentAccountService.SingleOrDefaultAsync(x => x.Id == id, includeProperties: "Country,City,Town");
            currentAccountDto.CityList = _currentAccountService.GetCityListForDropDown(currentAccountDto.CurrentAccount.CountryId, currentAccountDto.CurrentAccount.CityId);
            currentAccountDto.TownList = _currentAccountService.GetTownListForDropDown(currentAccountDto.CurrentAccount.CityId, currentAccountDto.CurrentAccount.TownId);
            if (currentAccountDto.CurrentAccount == null)
            {
                return NotFound();
            }

            return View(currentAccountDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CurrentAccountDto currentAccountDto)
        {
            if (ModelState.IsValid)
            {
                if (currentAccountDto.CurrentAccount.Id == 0)
                {
                    currentAccountDto.CurrentAccount.TenantId = Convert.ToInt32(User.FindFirst("TenantId")?.Value);
                    currentAccountDto.CurrentAccount.AccountTypeId = CurrentAccountTypeEnum.Vendor;
                    currentAccountDto.CurrentAccount.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    currentAccountDto.CurrentAccount.CreatedDate = DateTime.Now;
                    await _currentAccountService.AddAsync(currentAccountDto.CurrentAccount);
                    
                    // AppUser kaydını oluştur, 
                    CreateUserDto createUserDto = new CreateUserDto();
                    createUserDto.AppUser = ConvertToAppUser(currentAccountDto.CurrentAccount);
                    createUserDto.Role = SD.ROLE_Supplier;
                    createUserDto.Password = RandomPassword.Generate(8);
                    await _accountService.CreateUserAsync(createUserDto);

                    // şifre maili gönder ---------------------------- _accountService.CreateUserAsync içinde halledilecek
                }
                else
                {
                    currentAccountDto.CurrentAccount.TenantId = Convert.ToInt32(User.FindFirst("TenantId")?.Value);
                    currentAccountDto.CurrentAccount.AccountTypeId = CurrentAccountTypeEnum.Vendor;
                    currentAccountDto.CurrentAccount.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    currentAccountDto.CurrentAccount.UpdatedDate = DateTime.Now;
                    _currentAccountService.Update(currentAccountDto.CurrentAccount);

                    // currentAccountDto.CurrentAccount.Id ile AppUser da ara ve eposta adresini güncelle
                    var user = await _accountService.GetUserByCurrentAccountIdAsync(currentAccountDto.CurrentAccount.Id);
                    if (user != null)
                    {
                        user.Address = currentAccountDto.CurrentAccount.Address;
                        user.CityId = currentAccountDto.CurrentAccount.CityId;
                        user.CountryId = currentAccountDto.CurrentAccount.CountryId;
                        user.CurrentAccountId = currentAccountDto.CurrentAccount.Id;
                        user.Email = currentAccountDto.CurrentAccount.Email;
                        user.FullName = currentAccountDto.CurrentAccount.AuthorizedPerson;
                        user.PhoneNumber = currentAccountDto.CurrentAccount.Phone1;
                        user.Status = true;
                        user.TenantId = currentAccountDto.CurrentAccount.TenantId;
                        user.UserName = currentAccountDto.CurrentAccount.Email;
                        await _accountService.UpdateAsync(user);
                    }
                    else
                    {
                        CreateUserDto createUserDto = new CreateUserDto();
                        createUserDto.AppUser = ConvertToAppUser(currentAccountDto.CurrentAccount);
                        createUserDto.Role = SD.ROLE_Supplier;
                        createUserDto.Password = RandomPassword.Generate(8);
                        await _accountService.CreateUserAsync(createUserDto);

                        // şifre maili gönder ---------------------- _accountService.CreateUserAsync içinde halledilecek
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(currentAccountDto);
        }

        public AppUser ConvertToAppUser(CurrentAccount currentAccount)
        {
            return new AppUser()
            {
                Address = currentAccount.Address,
                CityId = currentAccount.CityId,
                CountryId = currentAccount.CountryId,
                CurrentAccountId = currentAccount.Id,
                Email = currentAccount.Email,
                FullName = currentAccount.AuthorizedPerson,
                PhoneNumber = currentAccount.Phone1,
                Status = true,
                TenantId = currentAccount.TenantId,
                UserName = currentAccount.Email
            };
        }

        public async Task<IActionResult> SendPassword(int id)
        {
            var vendor = await _currentAccountService.SingleOrDefaultAsync(v => v.Id == id && v.AccountTypeId == CurrentAccountTypeEnum.Vendor);
            if (vendor == null)
            {
                return Json(new { success = false, message = "Tedarikçi kaydına ulaşılamadı.!"});
            }
            
            var user = await _accountService.GetUserByCurrentAccountIdAsync(vendor.Id);
            if (user == null)
            {
                // kullanıcı kaydı yoksa kullanıcı oluştur, random şifre ata, role ata, şifreyi mail gönder
                CreateUserDto createUserDto = new CreateUserDto();
                createUserDto.AppUser = ConvertToAppUser(vendor);
                createUserDto.Role = SD.ROLE_Supplier;
                createUserDto.Password = RandomPassword.Generate(8);
                await _accountService.CreateUserAsync(createUserDto);
            } else
            {
                // kullanıcı kaydı varsa, random şifre ata, şifre değiştir, şifreyi mail gönder
                string newPassword = RandomPassword.Generate(8);
                await _accountService.UpdateNewPasswordAsync(user.Id, newPassword);
                //await _accountService.GenerateForgotPasswordTokenAsync(user);
            }


            return Json(new { success = true, message = "Tedarikçiye yeni şifre gönderildi." });
        }

        #region Calling Json data
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = await _currentAccountService.GetByIdAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Data silinirken bir hata oldu." });
            }
            _currentAccountService.Remove(objFromDb);
            return Json(new { success = true, message = "Data başarıyla silindi." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var list = await _currentAccountService.Where(v => v.AccountTypeId == CurrentAccountTypeEnum.Vendor); 
            return Json(new { success = list != null, data = list });
        }

        [HttpGet]
        public IActionResult GetCityData(int cid)
        {
            return Json(new { data = _currentAccountService.GetCityListForDropDown(cid) });
        }

        [HttpGet]
        public IActionResult GetTownData(int cid)
        {
            return Json(new { data = _currentAccountService.GetTownListForDropDown(cid) });
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
