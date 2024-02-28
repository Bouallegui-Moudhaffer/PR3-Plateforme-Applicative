using PA.ApplicationCore.Domain;

namespace PA.Web.Data
{
    public class StatusService
    {
        private readonly HttpClient _httpClient;

        public StatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Status>> GetStatussAsync()
        {
            var fullUri = new Uri("https://localhost:7029/api/Status");
            return await _httpClient.GetFromJsonAsync<IEnumerable<Status>>(fullUri.ToString());
        }
        public async Task<Status> GetStatusAsync(int id)
        {
            var fullUri = new Uri("https://localhost:7029/api/Status");
            return await _httpClient.GetFromJsonAsync<Status>($"{fullUri.ToString}" + "/" + $"{id}");
        }
        public async Task<Status> AddStatusAsync(Status Status)
        {
            var fullUri = new Uri("https://localhost:7029/api/Status");
            var response = await _httpClient.PostAsJsonAsync($"{fullUri}", Status);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return await response.Content.ReadFromJsonAsync<Status>();
        }
        public async Task UpdateStatusAsync(Status Status)
        {
            var fullUri = new Uri("https://localhost:7029/api/Status");

            await _httpClient.PutAsJsonAsync($"{fullUri.ToString}" + "/" + $"{Status.StatusId}", Status);
        }
        public async Task DeleteStatusAsync(int id)
        {
            var fullUri = new Uri("https://localhost:7029/api/Status");
            await _httpClient.DeleteAsync($"{fullUri.ToString}" + "/" + $"{id}");
        }
    }
}
