using KooliProjekt.WinFormsApp;
using KooliProjekt.PublicAPI.Api;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.WinFormsApp.Tests
{
    public class HealthDataPresenterTests
    {
        private readonly Mock<IHealthDataView> _mockView;
        private readonly Mock<IApiClient> _mockApiClient;
        private readonly HealthDataPresenter _presenter;

        public HealthDataPresenterTests()
        {
            _mockView = new Mock<IHealthDataView>();
            _mockApiClient = new Mock<IApiClient>();
            _presenter = new HealthDataPresenter(_mockView.Object, _mockApiClient.Object);
        }

        [Fact]
        public async Task LoadHealthDataAsync_NoError_SetsHealthDatas()
        {
            // Arrange
            var healthDatas = new List<HealthData> { new HealthData() };
            var result = new Result<List<HealthData>>
            {
                Data = healthDatas,
                Error = null
            };

            // Mock the API client response
            _mockApiClient.Setup(api => api.List()).ReturnsAsync(result);

            // Act
            await _presenter.LoadHealthDataAsync();

            // Assert
            _mockView.VerifySet(v => v.HealthDatas = healthDatas, Times.Once);
        }

        [Fact]
        public async Task LoadHealthDataAsync_HasError_ShowsMessageBox()
        {
            // Arrange
            var result = new Result<List<HealthData>>
            {
                Data = null,
                Error = "Test error"
            };

            // Mock the API client response
            _mockApiClient.Setup(api => api.List()).ReturnsAsync(result);

            // Act
            await _presenter.LoadHealthDataAsync();

            // Assert
            _mockApiClient.Verify(api => api.List(), Times.Once);
            // You can add verification for showing message box if implemented
        }

        [Fact]
        public async Task SaveSelectedItemAsync_SelectedItemNotNull_SavesAndReloads()
        {
            // Arrange
            var selectedItem = new HealthData();
            _mockView.SetupGet(v => v.SelectedItem).Returns(selectedItem);

            // Act
            await _presenter.SaveSelectedItemAsync();

            // Assert
            _mockApiClient.Verify(api => api.Save(selectedItem), Times.Once);
            _mockApiClient.Verify(api => api.List(), Times.Once);
        }

        [Fact]
        public async Task DeleteSelectedItemAsync_SelectedItemNotNull_DeletesAndReloads()
        {
            // Arrange
            var selectedItem = new HealthData { id = 1 };
            _mockView.SetupGet(v => v.SelectedItem).Returns(selectedItem);

            // Act
            await _presenter.DeleteSelectedItemAsync();

            // Assert
            _mockApiClient.Verify(api => api.Delete(selectedItem.id), Times.Once);
            _mockApiClient.Verify(api => api.List(), Times.Once);
        }

        [Fact]
        public void UpdateSelectedItem_SelectedItemNotNull_UpdatesView()
        {
            // Arrange
            var selectedItem = new HealthData();

            // Act
            _presenter.UpdateSelectedItem(selectedItem);

            // Assert
            _mockView.VerifySet(v => v.SelectedItem = selectedItem, Times.Once);
        }

        [Fact]
        public void UpdateSelectedItem_SelectedItemNull_ThrowsArgumentNullException()
        {
            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _presenter.UpdateSelectedItem(null));
        }
    }
}
