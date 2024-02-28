using PA.ApplicationCore.Domain;

namespace PA.Web.Data
{
    public class PostesService
    {
        private readonly HttpClient _httpClient;

        public PostesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Postes>> GetPostessAsync()
        {
            var fullUri = new Uri("https://localhost:7029/api/Postes");
            return await _httpClient.GetFromJsonAsync<IEnumerable<Postes>>(fullUri.ToString());
        }
        public async Task<Postes> GetPostesAsync(int id)
        {
            var fullUri = new Uri("https://localhost:7029/api/Postes");
            return await _httpClient.GetFromJsonAsync<Postes>($"{fullUri.ToString}" + "/" + $"{id}");
        }
        public async Task<Postes> AddPostesAsync(Postes Postes)
        {
            var fullUri = new Uri("https://localhost:7029/api/Postes/create");
            var response = await _httpClient.PostAsJsonAsync($"{fullUri}", Postes);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return await response.Content.ReadFromJsonAsync<Postes>();
        }
        public async Task UpdatePostesAsync(Postes Postes)
        {
            var fullUri = new Uri("https://localhost:7029/api/Postes");

            await _httpClient.PutAsJsonAsync($"{fullUri.ToString}" + "/" + $"{Postes.PostesId}", Postes);
        }
        public async Task DeletePostesAsync(int id)
        {
            var fullUri = new Uri("https://localhost:7029/api/Postes");
            await _httpClient.DeleteAsync($"{fullUri.ToString}" + "/" + $"{id}");
        }
    }
}
