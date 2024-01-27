using PA.ApplicationCore.Domain;
using PA.Desktop.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace PA.Desktop.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<bool> AuthenticateUser(LoginModel loginModel)
        {
            var response = await PostAsync("api/Utilisateurs/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                JwtToken = tokenResponse.Token;
                return true;
            }
            return false;
        }
        public async Task Add(Utilisateur userModel)
        {
            await PostAsync("api/Utilisateurs", userModel);
        }

        public async Task Edit(Utilisateur userModel)
        {
            await PutAsync($"api/Utilisateurs/{userModel.UtilisateurId}", userModel);
        }

        public async Task Remove(int utilisateurId)
        {
            await DeleteAsync($"api/Utilisateurs/{utilisateurId}");
        }

        public async Task<Utilisateur> GetById(int utilisateurId)
        {
            return await GetAsync<Utilisateur>($"api/Utilisateurs/{utilisateurId}");
        }

        public async Task<Utilisateur> GetByEmail(string email)
        {
            return await GetAsync<Utilisateur>($"api/Utilisateurs?email={email}");
        }

        public async Task<IEnumerable<Utilisateur>> GetAll()
        {
            return await GetAsync<IEnumerable<Utilisateur>>("api/Utilisateurs");
        }
        public async Task<Utilisateur> GetByUsername(string username)
        {
            return await GetAsync<Utilisateur>($"api/Utilisateurs/byUsername?username={username}");
        }
    }
}
