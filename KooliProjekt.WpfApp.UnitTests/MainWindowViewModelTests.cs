using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using KooliProjekt.PublicAPI.Api;
using WpfApp;

namespace KooliProjekt.WpfApp.UnitTests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public async Task Load_PopulatesListsFromApiClient()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(client => client.List())
                .ReturnsAsync(new Result<List<HealthData>>(new List<HealthData>
                {
                    new HealthData { id = 1, Weight = 70.5f, Blood_pressure = 120.0f, Blood_sugar = 90.0f },
                    new HealthData { id = 2, Weight = 80.0f, Blood_pressure = 130.0f, Blood_sugar = 100.0f }
                }));

            var viewModel = new MainWindowViewModel(mockApiClient.Object);

            // Act
            await viewModel.Load();

            // Assert
            Assert.Equal(2, viewModel.Lists.Count);
            Assert.Equal(70.5f, viewModel.Lists[0].Weight);
            Assert.Equal(130.0f, viewModel.Lists[1].Blood_pressure);
        }

        [Fact]
        public void SaveCommand_CanExecute_ReturnsTrueWhenSelectedItemIsNotNull()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            var viewModel = new MainWindowViewModel(mockApiClient.Object)
            {
                SelectedItem = new HealthData()
            };

            // Act
            var canExecute = viewModel.SaveCommand.CanExecute(null);

            // Assert
            Assert.True(canExecute);
        }

        [Fact]
        public void SaveCommand_CanExecute_ReturnsFalseWhenSelectedItemIsNull()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            var viewModel = new MainWindowViewModel(mockApiClient.Object)
            {
                SelectedItem = null
            };

            // Act
            var canExecute = viewModel.SaveCommand.CanExecute(null);

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public async Task DeleteCommand_ExecutesDeletesSelectedItem()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(client => client.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);
            var viewModel = new MainWindowViewModel(mockApiClient.Object)
            {
                SelectedItem = new HealthData { id = 1 }
            };

            // Act
            viewModel.DeleteCommand.Execute(null);

            // Assert
            Assert.Null(viewModel.SelectedItem);
        }
    }
}
