using AutoMapper;
using Penna.Entities.DTOs;
using Penna.Entities.Models;

namespace Penna.Web.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            // Tenants
            CreateMap<Tenant, TenantAddDto>();
            CreateMap<TenantAddDto, Tenant>();

            CreateMap<Tenant, TenantEditDto>();
            CreateMap<TenantEditDto, Tenant>();

            CreateMap<Tenant, TenantInfoDto>();
            CreateMap<TenantInfoDto, Tenant>();

            // Project
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();

            //CreateMap<Project, ProjectViewDto>();
            //CreateMap<ProjectViewDto, Project>();

            // Block
            CreateMap<Block, BlockDto>();
            CreateMap<BlockDto, Block>();

            CreateMap<Block, BlockViewDto>();
            CreateMap<BlockViewDto, Block>();

            // Product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductViewDto>();
            CreateMap<ProductViewDto, Product>();

            // Labor
            CreateMap<Labor, LaborAddDto>();
            CreateMap<LaborAddDto, Labor>();

            CreateMap<Labor, LaborEditDto>();
            CreateMap<LaborEditDto, Labor>();

            CreateMap<Labor, LaborViewDto>();
            CreateMap<LaborViewDto, Labor>();

            // Countries
            CreateMap<Country, CountryAddDto>();
            CreateMap<CountryAddDto, Country>();

            CreateMap<Country, CountryEditDto>();
            CreateMap<CountryEditDto, Country>();

            CreateMap<Country, CountryViewDto>();
            CreateMap<CountryViewDto, Country>();

            // Cities
            CreateMap<City, CityAddDto>();
            CreateMap<CityAddDto, City>();

            CreateMap<City, CityEditDto>();
            CreateMap<CityEditDto, City>();

            CreateMap<City, CityViewDto>();
            CreateMap<CityViewDto, City>();

            // Towns
            CreateMap<Town, TownAddDto>();
            CreateMap<TownAddDto, Town>();

            CreateMap<Town, TownEditDto>();
            CreateMap<TownEditDto, Town>();

            CreateMap<Town, TownViewDto>();
            CreateMap<TownViewDto, Town>();

            // Units
            CreateMap<Unit, UnitDto>();
            CreateMap<UnitDto, Unit>();

            // AppUser
            CreateMap<AppUserEditDto, AppUser>();
            CreateMap< AppUser, AppUserEditDto >();
            CreateMap<AppUserInfoDto, AppUser>();
            CreateMap<AppUser, AppUserInfoDto>();

            // CurrentAccount
            CreateMap<CurrentAccount, CurrentAccountDto>();
            CreateMap<CurrentAccountDto, CurrentAccount>();

            CreateMap<CurrentAccount, CurrentAccountViewDto>();
            CreateMap<CurrentAccountViewDto, CurrentAccount>();
        }
    }
}
