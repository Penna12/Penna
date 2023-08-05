using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.UnitOfWork;
using Penna.Entities.DTOs;
using Penna.Entities.Models;
using Penna.Business.Filters;
using Penna.Business.Concrete;
using Penna.Business.ValidationRules.FluentValidation;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.EntityFramework;
using Penna.Core.Utilities.EmailService.Microsoft;

namespace Penna.Business.DependencyResolver
{
    public static class MicrosoftDependencies
    {
        public static void AddCustomDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // DB Connection
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"].ToString(), o =>
                {
                    o.MigrationsAssembly("Penna.Data");
                });
            });

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMailService, EmailHelper>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAppUserService, AppUserService>();

            // Dependency Injection Classes
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(BaseService<>));
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ITownService, TownService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductInOutService, ProductInOutService>();
            services.AddScoped<IFixtureService, FixtureService>();
            services.AddScoped<IFixtureEmbezzledService, FixtureEmbezzledService>();
            services.AddScoped<ILaborService, LaborService>();
            services.AddScoped<ICurrentAccountService, CurrentAccountService>();
            services.AddScoped<ICurrentAccountDetailService, CurrentAccountDetailService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IProjectFileService, ProjectFileService>();
            services.AddScoped<IBlockTeamService, BlockTeamService>();
            services.AddScoped<IBlockSubcontractorService, BlockSubcontractorService>();
            services.AddScoped<IBlockSubcontractorBusinessGroupService, BlockSubcontractorBusinessGroupService>();
            services.AddScoped<IPurchaseRequestService, PurchaseRequestService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseTenderService, PurchaseTenderService>();
            services.AddScoped<IConcreteRequestService, ConcreteRequestService>();
            services.AddScoped<IConcreteCastingService, ConcreteCastingService>();
            services.AddScoped<IWorkPlanService, WorkPlanService>();
            services.AddScoped<IWorkPlanDetailService, WorkPlanDetailDetailService>();

            services.AddScoped<IAuthorityService, AuthorityService>();
            services.AddScoped<IAuthorityGroupService, AuthorityGroupService>();
            services.AddScoped<IUserPositionService, UserPositionService>();
            services.AddScoped<IUserPositionAuthorityService, UserPositionAuthorityService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            
            // Fluent Validation Classes
            services.AddTransient<IValidator<SignInModel>, SignInValidator>();
            services.AddTransient<IValidator<SignUpUserModel>, SignUpUserModelValidator>();
            services.AddTransient<IValidator<ChangePasswordModel>, ChangePasswordValidator>();
            services.AddTransient<IValidator<AppUserEditDto>, AppUserEditDtoValidator>();
            // Unit
            services.AddTransient<IValidator<Unit>, UnitValidator>();
            services.AddTransient<IValidator<UnitDto>, UnitDtoValidator>();
            // Tenant
            services.AddTransient<IValidator<Tenant>, TenantValidator>();
            services.AddTransient<IValidator<TenantAddDto>, TenantAddDtoValidator>();
            services.AddTransient<IValidator<TenantEditDto>, TenantEditDtoValidator>();
            // Product
            services.AddTransient<IValidator<Product>, ProductValidator>();
            services.AddTransient<IValidator<ProductDto>, ProductDtoValidator>();
            // ProductInOut
            services.AddTransient<IValidator<ProductInOut>, ProductInOutValidator>();
            // Fixture
            services.AddTransient<IValidator<Fixture>, FixtureValidator>();
            // FixtureEmbezzled
            services.AddTransient<IValidator<FixtureEmbezzled>, FixtureEmbezzledValidator>();
            // Labor
            services.AddTransient<IValidator<Labor>, LaborValidator>();
            services.AddTransient<IValidator<LaborAddDto>, LaborAddDtoValidator>();
            services.AddTransient<IValidator<LaborEditDto>, LaborEditDtoValidator>();
            // CurrentAccount
            services.AddTransient<IValidator<CurrentAccount>, CurrentAccountValidator>();
            services.AddTransient<IValidator<CurrentAccountDetail>, CurrentAccountDetailValidator>();
            services.AddTransient<IValidator<Project>, ProjectValidator>();
            services.AddTransient<IValidator<Block>, BlockValidator>();
            services.AddTransient<IValidator<Apartment>, ApartmentValidator>();
            services.AddTransient<IValidator<ProjectFile>, ProjectFileValidator>();
            services.AddTransient<IValidator<BlockTeam>, BlockTeamValidator>();
            services.AddTransient<IValidator<BlockSubcontractor>, BlockSubcontractorValidator>();
            services.AddTransient<IValidator<BlockSubcontractorAddDto>, BlockSubcontractorAddDtoValidator>();
            services.AddTransient<IValidator<BlockSubcontractorBusinessGroup>, BlockSubcontractorBusinessGroupValidator>();
            services.AddTransient<IValidator<PurchaseRequest>, PurchaseRequestValidator>();
            services.AddTransient<IValidator<Purchase>, PurchaseValidator>();
            services.AddTransient<IValidator<PurchaseTender>, PurchaseTenderValidator>();
            services.AddTransient<IValidator<Authority>, AuthorityValidator>();
            services.AddTransient<IValidator<AuthorityGroup>, AuthorityGroupValidator>();
            services.AddTransient<IValidator<UserPosition>, UserPositionValidator>();
            services.AddTransient<IValidator<UserPositionAuthority>, UserPositionAuthorityValidator>();
            services.AddTransient<IValidator<OfferCloseDto>, OfferCloseDtoValidator>();
            services.AddTransient<IValidator<ConcreteRequest>, ConcreteRequestValidator>();
            services.AddTransient<IValidator<ConcreteCasting>, ConcreteCastingValidator>();
            services.AddTransient<IValidator<WorkPlan>, WorkPlanValidator>();
            services.AddTransient<IValidator<WorkPlanDetail>, WorkPlanDetailValidator>();
            // UploadModelDto
            //services.AddTransient<IValidator<UploadModelDto>, UploadModelValidator>();

            // NotFount Filters
            services.AddScoped<UnitNotFoundFilter>();
            services.AddScoped<TenantNotFoundFilter>();
            services.AddScoped<ProductNotFoundFilter>();
            services.AddScoped<LaborNotFoundFilter>();

        }
    }
}
