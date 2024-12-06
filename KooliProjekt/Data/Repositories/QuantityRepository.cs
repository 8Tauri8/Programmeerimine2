namespace KooliProjekt.Data.Repositories
{
    public class QuantityRepository : BaseRepository<HealthData>
    {

        public QuantityRepository(ApplicationDbContext context) : base(context) { }

    }
}