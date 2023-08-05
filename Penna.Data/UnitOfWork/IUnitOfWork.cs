using Penna.Data.Interfaces;
using System.Threading.Tasks;

namespace Penna.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUser { get; }
        ICountryRepository Country { get; }
        ICityRepository City { get; }
        ITownRepository Town { get; }
        IUnitRepository Unit { get; }

        ITenantRepository Tenant { get; }
        IProjectRepository Project { get; }
        IProductRepository Product { get; }
        IProductInOutRepository ProductInOut { get; }
        IFixtureRepository Fixture { get; }
        IFixtureEmbezzledRepository FixtureEmbezzled { get; }
        ILaborRepository Labor { get; }
        ICurrentAccountRepository CurrentAccount { get; }
        IBlockRepository Block { get; }
        IApartmentRepository Apartment { get; }
        IProjectFileRepository ProjectFile { get; }
        IBlockTeamRepository BlockTeam { get; }
        IBlockSubcontractorRepository BlockSubcontractor { get; }
        IBlockSubcontractorBusinessGroupRepository BlockSubcontractorBusinessGroup { get; }
        ICurrentAccountDetailRepository CurrentAccountDetail { get; }
        IAuthorityRepository Authority { get; }
        IAuthorityGroupRepository AuthorityGroup { get; }
        IUserPositionRepository UserPosition { get; }
        IUserPositionAuthorityRepository UserPositionAuthority { get; }
        IPurchaseRequestRepository PurchaseRequest { get; }
        IPurchaseRepository Purchase { get; }
        IPurchaseTenderRepository PurchaseTender { get; }
        IConcreteRequestRepository ConcreteRequest { get; }
        IConcreteCastingRepository ConcreteCasting { get; }
        IWorkPlanRepository WorkPlan { get; }
        IWorkPlanDetailRepository WorkPlanDetail { get; }


        ISP_Call SP_Call { get; }

        Task CommitAsync();
        void Commit();

    }
}
