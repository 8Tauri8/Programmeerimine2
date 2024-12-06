namespace KooliProjekt.Data.Repositories
{
    public class PatientRepository : BaseRepository<HealthData>
    {

        public PatientRepository(IPatientRepository context) : base(context) { }

    }
}