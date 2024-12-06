namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        // Repository-dele ligipääs
        IHealthDataRepository HealthDataRepository { get; }
        INutrientsRepository NutrientsRepository { get; }
        INutritionRepository NutritionRepository { get; }
        IPatientRepository PatientRepository { get; }
        IQuantityRepository QuantityRepository { get; }

        // Transaktsiooni haldamise meetodid
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
