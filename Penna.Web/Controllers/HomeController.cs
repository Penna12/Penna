using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Penna.Business.Filters;
using Penna.Business.Interfaces;
using Penna.Core.Extensions;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using Penna.Utility.Service;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Penna.Web.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly ITownService _townService;
        private readonly ITenantService _tenantService;
        private readonly IProjectService _projectService;

        public HomeController(ILogger<HomeController> logger, 
            IStringLocalizer<HomeController> localizer,
            ICountryService countryService, ICityService cityService, ITownService townService,
            ITenantService tenantService, IProjectService projectService)
        {
            _logger = logger;
            _localizer = localizer;
            _countryService = countryService;
            _cityService = cityService;
            _townService = townService;
            _tenantService = tenantService;
            _projectService = projectService;
        }


        public IActionResult Index()
        {
            /// Culture Localization değiştirme,
            var cultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            TempData["active"] = "Dashboard";
            _logger.LogInformation(_localizer["Home_Index_Call"].Value, System.DateTime.Now);

            Toolbar.Title = _localizer["Dashboard"].Value;
            Toolbar.Breadcrumbs = new[] { _localizer["Main_Page"].Value, _localizer["Dashboard"].Value };
            Toolbar.Urls = new[] { "/", "#" };

            TenantListDto tenantDto = new TenantListDto();
            tenantDto.CountryList = _countryService.GetCountryListForDropDown();
            tenantDto.CityList = _cityService.GetCityListForDropDown(222);
            tenantDto.TownList = _townService.GetTownListForDropDown(40);

            return View(tenantDto);
        }

        public IActionResult Privacy()
        {
            TempData["active"] = "Privacy";
            Toolbar.Title = "Gizlilik Şartları";
            Toolbar.Breadcrumbs = new[] { _localizer["Main_Page"].Value, _localizer["Privacy"].Value };
            Toolbar.Urls = new[] { "/", "#" };

            var user = App.Session.GetObject<AppUser>("activeUser");
            return View(user ?? new AppUser());
        }



        public IActionResult Create()
        {
            Toolbar.Title = "Yeni Kayıt";
            Toolbar.Breadcrumbs = new[] { _localizer["Main_Page"].Value, _localizer["New_Record"].Value };
            Toolbar.Urls = new[] { "/", "#" };

            return View(new Product());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            Toolbar.Title = "Yeni Kayıt";
            Toolbar.Breadcrumbs = new[] { _localizer["Main_Page"].Value, _localizer["New_Record"].Value };
            Toolbar.Urls = new[] { "/", "#" };

            var tenant = await _tenantService.GetByIdAsync(1);
            tenant.CityId = 40;
            _tenantService.Update(tenant);
            return View();
        }


        [ServiceFilter(typeof(TenantNotFoundFilter))]
        public async Task<IActionResult> Detail(int id)
        {
            Toolbar.Title = "Kayıt Detayı";
            Toolbar.Breadcrumbs = new[] { _localizer["Main_Page"].Value, _localizer["Detail"].Value };
            Toolbar.Urls = new[] { "/", "#" };

            return View(await _tenantService.GetByIdAsync(id));
        }
    }
}
