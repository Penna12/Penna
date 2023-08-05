using Penna.Data.EntityFramework;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using System.Threading.Tasks;

namespace Penna.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
            //sP_Call = new SP_Call(_context);
        }

        private CountryRepository _countryRepository;
        private CityRepository _cityRepository;
        private TownRepository _townRepository;
        private UnitRepository _unitRepository;
        private TenantRepository _tenantRepository;
        private ProjectRepository _projectRepository;
        private ProductRepository _productRepository;
        private ProductInOutRepository _productInOutRepository;
        private FixtureRepository _fixtureRepository;
        private FixtureEmbezzledRepository _fixtureEmbezzledRepository;
        private LaborRepository _laborRepository;
        private CurrentAccountRepository _currentAccountRepository;
        private BlockRepository _blockRepository;
        private ApartmentRepository _apartmentRepository;
        private ProjectFileRepository _projectFileRepository;
        private BlockTeamRepository _blockTeamRepository;
        private BlockSubcontractorRepository _blockSubcontractorRepository;
        private BlockSubcontractorBusinessGroupRepository _blockSubcontractorBusinessGroupRepository;
        private CurrentAccountDetailRepository _currentAccountDetailRepository;
        private PurchaseRequestRepository _purchaseRequestRepository;
        private PurchaseRepository _purchaseRepository;
        private PurchaseTenderRepository _purchaseTenderRepository;
        private ConcreteRequestRepository _concreteRequestRepository;
        private ConcreteCastingRepository _concreteCastingRepository;
        private WorkPlanRepository _workPlanRepository;
        private WorkPlanDetailRepository _workPlanDetailRepository;

        private AuthorityRepository _authorityRepository;
        private AuthorityGroupRepository _authorityGroupRepository;
        private UserPositionRepository _userPositionRepository;
        private UserPositionAuthorityRepository _userPositionAuthorityRepository;


        private AppUserRepository _appUserRepository;
        private SP_Call _sP_Call;


        public IAppUserRepository AppUser => _appUserRepository = _appUserRepository ?? new AppUserRepository(_context);
        public ICountryRepository Country => _countryRepository = _countryRepository ?? new CountryRepository(_context);
        public ICityRepository City => _cityRepository = _cityRepository ?? new CityRepository(_context);
        public ITownRepository Town => _townRepository = _townRepository ?? new TownRepository(_context);
        public IUnitRepository Unit => _unitRepository = _unitRepository ?? new UnitRepository(_context);
        
        public ITenantRepository Tenant => _tenantRepository = _tenantRepository ?? new TenantRepository(_context);
        public IProjectRepository Project => _projectRepository = _projectRepository ?? new ProjectRepository(_context);
        public IProductRepository Product => _productRepository = _productRepository ?? new ProductRepository(_context);
        public IProductInOutRepository ProductInOut => _productInOutRepository = _productInOutRepository ?? new ProductInOutRepository(_context);
        public IFixtureRepository Fixture => _fixtureRepository = _fixtureRepository ?? new FixtureRepository(_context);
        public IFixtureEmbezzledRepository FixtureEmbezzled => _fixtureEmbezzledRepository = _fixtureEmbezzledRepository ?? new FixtureEmbezzledRepository(_context);
        public ILaborRepository Labor => _laborRepository = _laborRepository ?? new LaborRepository(_context);

        public ICurrentAccountRepository CurrentAccount => _currentAccountRepository = _currentAccountRepository ?? new CurrentAccountRepository(_context);
        public IBlockRepository Block => _blockRepository = _blockRepository ?? new BlockRepository(_context);
        public IApartmentRepository Apartment => _apartmentRepository = _apartmentRepository ?? new ApartmentRepository(_context);
        public IProjectFileRepository ProjectFile => _projectFileRepository = _projectFileRepository ?? new ProjectFileRepository(_context);
        public IBlockTeamRepository BlockTeam => _blockTeamRepository = _blockTeamRepository ?? new BlockTeamRepository(_context);
        public IBlockSubcontractorRepository BlockSubcontractor => _blockSubcontractorRepository = _blockSubcontractorRepository ?? new BlockSubcontractorRepository(_context);
        public IBlockSubcontractorBusinessGroupRepository BlockSubcontractorBusinessGroup => _blockSubcontractorBusinessGroupRepository = _blockSubcontractorBusinessGroupRepository ?? new BlockSubcontractorBusinessGroupRepository(_context);
        public ICurrentAccountDetailRepository CurrentAccountDetail => _currentAccountDetailRepository = _currentAccountDetailRepository ?? new CurrentAccountDetailRepository(_context);
        public IPurchaseRequestRepository PurchaseRequest => _purchaseRequestRepository = _purchaseRequestRepository ?? new PurchaseRequestRepository(_context);
        public IPurchaseRepository Purchase => _purchaseRepository = _purchaseRepository ?? new PurchaseRepository(_context);
        public IPurchaseTenderRepository PurchaseTender => _purchaseTenderRepository = _purchaseTenderRepository ?? new PurchaseTenderRepository(_context);
        public IConcreteRequestRepository ConcreteRequest => _concreteRequestRepository = _concreteRequestRepository ?? new ConcreteRequestRepository(_context);
        public IConcreteCastingRepository ConcreteCasting => _concreteCastingRepository = _concreteCastingRepository ?? new ConcreteCastingRepository(_context);    
        public IWorkPlanRepository WorkPlan => _workPlanRepository = _workPlanRepository ?? new WorkPlanRepository(_context);
        public IWorkPlanDetailRepository WorkPlanDetail => _workPlanDetailRepository = _workPlanDetailRepository ?? new WorkPlanDetailRepository(_context);

        public IAuthorityRepository Authority => _authorityRepository = _authorityRepository ?? new AuthorityRepository(_context);
        public IAuthorityGroupRepository AuthorityGroup => _authorityGroupRepository = _authorityGroupRepository ?? new AuthorityGroupRepository(_context);
        public IUserPositionRepository UserPosition => _userPositionRepository = _userPositionRepository ?? new UserPositionRepository(_context);
        public IUserPositionAuthorityRepository UserPositionAuthority => _userPositionAuthorityRepository = _userPositionAuthorityRepository ?? new UserPositionAuthorityRepository(_context);


        public ISP_Call SP_Call => _sP_Call = _sP_Call ?? new SP_Call(_context);

        //public ISP_Call sP_Call { get; private set; }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
