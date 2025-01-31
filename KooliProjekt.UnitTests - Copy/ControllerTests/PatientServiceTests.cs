using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _repositoryMock;
        private readonly PatientService _service;

        public PatientServiceTests()
        {
            // Prepare some mock data for patients
            var patientList = new List<Patient>
            {
                new Patient { id = 1, Name = "John Doe", HealthData = "Normal", Nutrition = "Balanced" },
                new Patient { id = 2, Name = "Jane Smith", HealthData = "High Blood Pressure", Nutrition = "Low Sodium" }
            };

            // Mock the repository methods
            _repositoryMock = new Mock<IPatientRepository>();
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PagedResult<Patient>
            {
                Results = patientList,
                RowCount = patientList.Count,
                CurrentPage = 1,
                PageSize = 5,
                PageCount = 1
            });
            _repositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => patientList.FirstOrDefault(p => p.id == id));
            _repositoryMock.Setup(r => r.Save(It.IsAny<Patient>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Initialize the service with the mock repository
            _service = new PatientService(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_paginated_data()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;

            // Act
            var result = await _service.List(page, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.RowCount);
            Assert.Equal(page, result.CurrentPage);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(2, result.Results.Count());
        }

        [Fact]
        public async Task Get_should_return_patient_when_found()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.id);
        }

        [Fact]
        public async Task Get_should_return_empty_patient_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.id);  // Since it returns a default Patient if not found
        }

        [Fact]
        public async Task Save_should_add_new_patient_when_id_is_zero()
        {
            // Arrange
            var newPatient = new Patient { id = 0, Name = "Sam Wilson", HealthData = "Healthy", Nutrition = "High Protein" };

            // Act
            await _service.Save(newPatient);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Patient>()), Times.Once);
        }

        [Fact]
        public async Task Save_should_update_existing_patient_when_id_is_not_zero()
        {
            // Arrange
            var existingPatient = new Patient { id = 1, Name = "John Doe", HealthData = "Normal", Nutrition = "Balanced" };

            // Act
            await _service.Save(existingPatient);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Patient>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_remove_patient_when_found()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_not_remove_patient_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never); // Verify Delete was never called
        }
    }
}
