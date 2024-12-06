namespace KooliProjekt.Data.Repositories
{
    public class HealthDataRepository : BaseRepository<HealthData>
    {

        public HealthDataRepository(ApplicationDbContext context) : base(context) { }

    }
}