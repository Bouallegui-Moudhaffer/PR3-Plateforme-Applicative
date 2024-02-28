using PA.ApplicationCore.Domain;

namespace PA.Web.Data
{
    public class TypeService
    {
        private readonly HttpClient _httpClient;
        private Uri fullUri = new Uri("https://localhost:7029/api/Types");
        public TypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Types>> GetTypessAsync()
        {

            return await _httpClient.GetFromJsonAsync<IEnumerable<Types>>(fullUri.ToString());
        }
        public async Task<Types> GetTypesAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Types>($"{fullUri.ToString}" + "/" + $"{id}");
        }
        public async Task<Types> AddTypesAsync(Types Types)
        {
            var response = await _httpClient.PostAsJsonAsync($"{fullUri}", Types);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return await response.Content.ReadFromJsonAsync<Types>();
        }
        public async Task UpdateTypesAsync(Types Types)
        {
            await _httpClient.PutAsJsonAsync($"{fullUri.ToString}" + "/" + $"{Types.TypeId}", Types);
        }
        public async Task DeleteTypesAsync(int id)
        {
            await _httpClient.DeleteAsync($"{fullUri.ToString}" + "/" + $"{id}");
        }
    }
}
