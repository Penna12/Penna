using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Web.ViewComponents
{
    public class ProjectSubMenu : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly IBlockService _blockService;

        public ProjectSubMenu(IMapper mapper, IProjectService projectService, IBlockService blockService)
        {
            _mapper = mapper;
            _projectService = projectService;
            _blockService = blockService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int tenantId)
        {
            var project = await _projectService.Where(x => x.TenantId == tenantId, includeProperties: "Tenant");
            if (project.Count() > 0)
            {
                var tenant = project.First()?.Tenant;
                SD.TenantId = (tenant != null) ? tenant.Id : tenantId;
                SD.TenantName = (tenant != null) ? tenant.Title : string.Empty;
            }
            // Block kayıtlarını alalım menüde kullanacağız
            if (SD.ProjectId > 0)
            {
                var blocks = await _blockService.Where(b => b.ProjectId == SD.ProjectId);
                TempData["Blocks"] = blocks;
            }
            return View(project);
        }
    }
}
