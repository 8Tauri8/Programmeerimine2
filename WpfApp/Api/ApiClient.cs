using System.Net.Http;
using System.Net.Http.Json;

namespace WpfApp.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<HealthData>>> List()
        {
            var result = new Result<List<HealthData>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<HealthData>>("HealthDatas");
            }
            catch(Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task Save(HealthData list)
        {
            if(list.id == 0)
            {
                await _httpClient.PostAsJsonAsync("HealthDatas", list);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("HealthDatas/" + list.id, list);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("HealthDatas/" + id);
        }
    }
}