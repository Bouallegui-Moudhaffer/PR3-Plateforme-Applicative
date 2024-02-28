using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace PA.Desktop.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly HttpClient HttpClient;
        protected readonly string ApiBaseUrl = "https://localhost:7029";
        public string JwtToken { get; set; }
        protected BaseRepository(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        protected void AddAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(JwtToken))
            {
                HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JwtToken);
            }
        }

        protected async Task<T> GetAsync<T>(string uri)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var response = await HttpClient.GetAsync($"{ApiBaseUrl}/{uri}");
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), options);
        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string uri, T data)
        {
            var json = JsonSerializer.Serialize(data);
            Console.WriteLine($"Request JSON: {json}");

            return await HttpClient.PostAsJsonAsync($"{ApiBaseUrl}/{uri}", data);
        }

        protected async Task<HttpResponseMessage> PutAsync<T>(string uri, T data)
        {
            return await HttpClient.PutAsJsonAsync($"{ApiBaseUrl}/{uri}", data);
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            AddAuthorizationHeader();
            return await HttpClient.DeleteAsync($"{ApiBaseUrl}/{uri}");
        }
    }
}
