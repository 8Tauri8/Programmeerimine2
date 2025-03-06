using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Search;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class PatientServiceTests : ServiceTestBase
    {
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _patientService = new PatientService(DbContext);
        }

        [Fact]
        public async Task List_ShouldReturnPagedResult()
        {
            // Arrange
            DbContext.Patient.AddRange(new List<Patient>
            {
                new Patient { Name = "Patient1", HealthData = "Data1", Nutrition = "Nutrition1" },
                new Patient { Name = "Patient2", HealthData = "Data2", Nutrition = "Nutrition2" }
            });
            await DbContext.SaveChangesAsync();

            var search = new PatientsSearch(); // Create an empty search object

            // Act
            var result = await _patientService.List(1, 5, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count());
        }

        [Fact]
        public async Task Get_ShouldReturnPatientById()
        {
            // Arrange
            var patient = new Patient { Name = "Patient1", HealthData = "Data1", Nutrition = "Nutrition1" };
            DbContext.Patient.Add(patient);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _patientService.Get(patient.id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Patient1", result.Name);
        }

        [Fact]
        public async Task Save_ShouldAddNewPatient()
        {
            // Arrange
            var patient = new Patient { Name = "Patient1", HealthData = "Data1", Nutrition = "Nutrition1" };

            // Act
            await _patientService.Save(patient);

            // Assert
            var savedPatient = await DbContext.Patient.FirstOrDefaultAsync();
            Assert.NotNull(savedPatient);
            Assert.Equal("Patient1", savedPatient.Name);
        }

        [Fact]
        public async Task Save_ShouldUpdateExistingPatient()
        {
            // Arrange
            var patient = new Patient { Name = "Patient1", HealthData = "Data1", Nutrition = "Nutrition1" };
            DbContext.Patient.Add(patient);
            await DbContext.SaveChangesAsync();

            var patientId = patient.id;

            DbContext.ChangeTracker.Clear();

            // Act
            patient.Name = "UpdatedPatient";
            await _patientService.Save(patient);

            // Assert
            var updatedPatient = await DbContext.Patient.FindAsync(patientId);
            Assert.NotNull(updatedPatient);
            Assert.Equal("UpdatedPatient", updatedPatient.Name);
        }

        [Fact]
        public async Task Delete_ShouldRemovePatient()
        {
            // Arrange
            var patient = new Patient { Name = "Patient1", HealthData = "Data1", Nutrition = "Nutrition1" };
            DbContext.Patient.Add(patient);
            await DbContext.SaveChangesAsync();

            // Act
            await _patientService.Delete(patient.id);

            // Assert
            var deletedPatient = await DbContext.Patient.FindAsync(patient.id);
            Assert.Null(deletedPatient);
        }

        [Fact]
        public async Task Get_ShouldReturnDefaultPatientWhenNotFound()
        {
            // Act
            var result = await _patientService.Get(999);

            // Assert
            Assert.NotNull(result); // It should not be null, but a default Patient object
            Assert.Equal(0, result.id); // Verify it's a default object
        }
    }
}
