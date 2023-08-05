using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Filters;
using Penna.Entities.DTOs;

namespace Penna.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ProjectUnselectedFilter))]
    public class FloorController : Controller
    {
        private readonly IMapper _mapper;

        public FloorController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["active"] = "FloorList";
            Toolbar.Title = "Kat Listesi";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Kat Listesi" };
            Toolbar.Urls = new[] { "/", "#" };
            return View();
        }

        public IActionResult Create()
        {
            TempData["active"] = "FloorCreate";
            Toolbar.Title = "Kat İrtifakı Kur";
            Toolbar.Breadcrumbs = new[] { "Ana Sayfa", "Kat İrtifakı Kur" };
            Toolbar.Urls = new[] { "/", "#" };
            return View();
        }
    }
}
