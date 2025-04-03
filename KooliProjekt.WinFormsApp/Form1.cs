using System;
using System.Windows.Forms;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IHealthDataView
    {
        public IList<HealthData> HealthDatas
        {
            get => (IList<HealthData>)TodoListsGrid.DataSource;
            set => TodoListsGrid.DataSource = value;
        }

        public HealthData SelectedItem
        {
            get => _selectedHealthData;
            set => _selectedHealthData = value;
        }

        public HealthDataPresenter Presenter { get; set; }

        private readonly IApiClient _apiClient;
        private HealthData _selectedHealthData;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();

            Presenter = new HealthDataPresenter(this, _apiClient);

            TodoListsGrid.SelectionChanged += TodoListsGrid_SelectionChanged;
            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;

            Load += async (sender, args) => await Presenter.LoadHealthDataAsync();
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                SelectedItem = new HealthData();
            }

            SelectedItem.id = int.TryParse(IdField.Text, out int id) ? id : 0;
            SelectedItem.Weight = float.TryParse(WeightField.Text, out float weight) ? weight : 0;
            SelectedItem.Blood_pressure = float.TryParse(BloodPressureField.Text, out float bp) ? bp : 0;
            SelectedItem.Blood_sugar = float.TryParse(BloodSugarField.Text, out float sugar) ? sugar : 0;

            await Presenter.SaveSelectedItemAsync();
        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    await Presenter.DeleteSelectedItemAsync();
                }
            }
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            IdField.Text = string.Empty;
            WeightField.Text = string.Empty;
            BloodPressureField.Text = string.Empty;
            BloodSugarField.Text = string.Empty;
            TodoListsGrid.ClearSelection();
        }

        private void TodoListsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (TodoListsGrid.SelectedRows.Count == 0)
                return;

            var healthdata = (HealthData)TodoListsGrid.SelectedRows[0].DataBoundItem;

            if (healthdata == null)
                return;

            Presenter.UpdateSelectedItem(healthdata);

            IdField.Text = healthdata.id.ToString();
            WeightField.Text = healthdata.Weight.ToString();
            BloodPressureField.Text = healthdata.Blood_pressure.ToString();
            BloodSugarField.Text = healthdata.Blood_sugar.ToString();
        }
    }
}
