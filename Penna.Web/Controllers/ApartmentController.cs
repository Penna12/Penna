using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Entities.DTOs;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class ApartmentController : Controller
    {
        private readonly IMapper _mapper;

        public ApartmentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["active"] = "ApartmentList";
            Toolbar.Title = "Daire Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Daire List" };
            Toolbar.Urls = new[] { "/", "#" };
            return View();
        }

        public IActionResult Create()
        {
            TempData["active"] = "ApartmentCreate";
            Toolbar.Title = "Daire Oluştur";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Daire Oluştur" };
            Toolbar.Urls = new[] { "/", "#" };
            return View();
        }
    }
}
