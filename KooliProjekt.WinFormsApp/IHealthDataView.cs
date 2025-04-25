using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    public interface IHealthDataView
    {
        IList<HealthData> HealthDatas { get; set; }
        HealthData SelectedItem { get; set; }
        HealthDataPresenter Presenter { get; set; }
    }
}
