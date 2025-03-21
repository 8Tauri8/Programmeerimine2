using System;
using System.Windows.Forms;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly IApiClient _apiClient;
        private HealthData _selectedHealthData;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();

            TodoListsGrid.SelectionChanged += TodoListsGrid_SelectionChanged;
            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (_selectedHealthData != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    await _apiClient.Delete(_selectedHealthData.id);
                    await LoadData();
                    MessageBox.Show("Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            if (_selectedHealthData == null)
            {
                _selectedHealthData = new HealthData();
            }

            if (int.TryParse(IdField.Text, out int id) && id > 0)
            {
                _selectedHealthData.id = id;
            }
            else
            {
                _selectedHealthData.id = 0; // New object
            }

            _selectedHealthData.Weight = float.TryParse(WeightField.Text, out float weight) ? weight : 0;
            _selectedHealthData.Blood_pressure = float.TryParse(BloodPressureField.Text, out float bp) ? bp : 0;
            _selectedHealthData.Blood_sugar = float.TryParse(BloodSugarField.Text, out float sugar) ? sugar : 0;

            try
            {
                await _apiClient.Save(_selectedHealthData);
                await LoadData();
                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            IdField.Text = string.Empty;
            TitleField.Text = string.Empty;
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
            {
                IdField.Text = string.Empty;
                TitleField.Text = string.Empty;
                WeightField.Text = string.Empty;
                BloodPressureField.Text = string.Empty;
                BloodSugarField.Text = string.Empty;
                return;
            }

            _selectedHealthData = healthdata; // Save the selected object
            IdField.Text = healthdata.id.ToString();
            WeightField.Text = healthdata.Weight.ToString();
            BloodPressureField.Text = healthdata.Blood_pressure.ToString();
            BloodSugarField.Text = healthdata.Blood_sugar.ToString();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await LoadData();
        }

        private async Task LoadData()
        {
            var response = await _apiClient.List();

            if (response.HasError)
            {
                MessageBox.Show(response.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TodoListsGrid.AutoGenerateColumns = true;
            TodoListsGrid.DataSource = response.Value; // Bind data to grid
        }
    }
}
