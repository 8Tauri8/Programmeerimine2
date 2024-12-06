namespace KooliProjekt.Data.Repositories
{
    public class NutrientsRepository : BaseRepository<HealthData>
    {

        public NutrientsRepository(ApplicationDbContext context) : base(context) { }

    }
}