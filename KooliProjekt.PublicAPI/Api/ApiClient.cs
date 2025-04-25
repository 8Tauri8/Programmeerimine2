using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KooliProjekt.PublicAPI.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<HealthData>>> List()
        {
            try
            {
                var healthDataList = await _httpClient.GetFromJsonAsync<List<HealthData>>("healthdata");
                return new Result<List<HealthData>> { Data = healthDataList };
            }
            catch (HttpRequestException ex)
            {
                var result = new Result<List<HealthData>>();
                result.AddError("_", "Failed to connect to the API.");
                return result;
            }
        }

        public async Task<Result> Save(HealthData healthData)
        {
            HttpResponseMessage response;
            if (healthData.id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("healthdata", healthData);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"healthdata/{healthData.id}", healthData);
            }

            var result = new Result();
            if (!response.IsSuccessStatusCode)
            {
                // Attempt to read error details from the response
                try
                {
                    var errorResult = await response.Content.ReadFromJsonAsync<Result>();
                    if (errorResult != null && errorResult.Errors.Count > 0)
                    {
                        result.Errors = errorResult.Errors;
                    }
                    else
                    {
                        result.AddError("_", $"API returned an error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("_", $"Failed to read error from API: {ex.Message}");
                }
            }

            return result;
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"healthdata/{id}");
        }
    }
}
