using System.Net.Http;
using System.Net.Http.Json;


namespace KooliProjekt.PublicAPI.Api
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
            try
            {
                // Attempt to fetch data from the API
                var healthDataList = await _httpClient.GetFromJsonAsync<List<HealthData>>("healthdata");

                // Return the result with data and no error
                return new Result<List<HealthData>>(healthDataList, null);  // Success case
            }
            catch (HttpRequestException ex)
            {
                // Handle connection errors
                string errorMessage = ex.HttpRequestError == HttpRequestError.ConnectionError
                    ? "Ei saa serveriga ühendust. Palun proovi hiljem uuesti."
                    : ex.Message;

                // Return the result with error message
                return new Result<List<HealthData>>(null, errorMessage);  // Error case
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                return new Result<List<HealthData>>(null, ex.Message);  // Error case
            }
        }


        public async Task Save(HealthData list)
        {
            if(list.id == 0)
            {
                await _httpClient.PostAsJsonAsync("healthdata", list);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("healthdata/" + list.id, list);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("healthdata/" + id);
        }
    }
}