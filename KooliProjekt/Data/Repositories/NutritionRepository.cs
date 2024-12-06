namespace KooliProjekt.Data.Repositories
{
    public class NutritionRepository : BaseRepository<HealthData>
    {

        public NutritionRepository(ApplicationDbContext context) : base(context) { }

    }
}