namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // Repository-de väljad
        public IHealthDataRepository HealthDataRepository { get; private set; }
        public INutrientsRepository NutrientsRepository { get; private set; }
        public INutritionRepository NutritionRepository { get; private set; }
        public IPatientRepository PatientRepository { get; private set; }
        public IQuantityRepository QuantityRepository { get; private set; }

        // Konstruktor, mis määrab sõltuvused repository-de jaoks
        public UnitOfWork(ApplicationDbContext context,
                          IHealthDataRepository healthDataRepository,
                          INutrientsRepository nutrientsRepository,
                          INutritionRepository nutritionRepository,
                          IPatientRepository patientRepository,
                          IQuantityRepository quantityRepository)
        {
            _context = context;
            HealthDataRepository = healthDataRepository;
            NutrientsRepository = nutrientsRepository;
            NutritionRepository = nutritionRepository;
            PatientRepository = patientRepository;
            QuantityRepository = quantityRepository;
        }

        // Transaktsiooni alustamine
        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        // Muudatuste kinnitamine
        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        // Transaktsiooni tagasivõtmine
        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
