namespace KooliProjekt.Data.Repositories
{
    public class PatientRepository : BaseRepository<HealthData>
    {

        public PatientRepository(ApplicationDbContext context) : base(context) { }

    }
}