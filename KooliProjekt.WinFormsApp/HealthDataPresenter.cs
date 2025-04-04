using System.Threading.Tasks;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public class HealthDataPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IHealthDataView _view;

        public HealthDataPresenter(IHealthDataView view, IApiClient apiClient)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

            _view.Presenter = this;
        }

        public async Task LoadHealthDataAsync()
        {
            try
            {
                var response = await _apiClient.List();
                if (!response.HasError)
                {
                    _view.HealthDatas = response.Data; // Use Data instead of Value
                }
                else
                {
                    MessageBox.Show(response.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task SaveSelectedItemAsync()
        {
            if (_view.SelectedItem != null)
            {
                await _apiClient.Save(_view.SelectedItem);
                await LoadHealthDataAsync();
                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public async Task DeleteSelectedItemAsync()
        {
            if (_view.SelectedItem != null)
            {
                await _apiClient.Delete(_view.SelectedItem.id);
                await LoadHealthDataAsync();
                MessageBox.Show("Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void UpdateSelectedItem(HealthData selectedItem)
        {
            if (selectedItem == null)
                throw new ArgumentNullException(nameof(selectedItem));

            _view.SelectedItem = selectedItem;
        }
    }
}
