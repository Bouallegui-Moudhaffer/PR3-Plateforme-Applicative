using PA.ApplicationCore.Domain;

namespace PA.Web.Data
{
    public class SalleService
    {
        private readonly HttpClient _httpClient;

        public SalleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Salles>> GetSallessAsync()
        {
            var fullUri = new Uri("https://localhost:7029/api/Salles");
            return await _httpClient.GetFromJsonAsync<IEnumerable<Salles>>(fullUri.ToString());
        }
        public async Task<Salles> GetSallesAsync(int id)
        {
            var fullUri = new Uri("https://localhost:7029/api/Salles");
            return await _httpClient.GetFromJsonAsync<Salles>($"{fullUri.ToString}" + "/" + $"{id}");
        }
        public async Task<Salles> AddSallesAsync(Salles Salles)
        {
            var fullUri = new Uri("https://localhost:7029/api/Salles");
            var response = await _httpClient.PostAsJsonAsync($"{fullUri}", Salles);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return await response.Content.ReadFromJsonAsync<Salles>();
        }
        public async Task UpdateSallesAsync(Salles Salles)
        {
            var fullUri = new Uri("https://localhost:7029/api/Salles");

            await _httpClient.PutAsJsonAsync($"{fullUri.ToString}" + "/" + $"{Salles.SallesId}", Salles);
        }
        public async Task DeleteSallesAsync(int id)
        {
            var fullUri = new Uri("https://localhost:7029/api/Salles");
            await _httpClient.DeleteAsync($"{fullUri.ToString}" + "/" + $"{id}");
        }
    }
}
