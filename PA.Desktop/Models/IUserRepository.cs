using PA.ApplicationCore.Domain;

namespace PA.Desktop.Models
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateUser(LoginModel loginModel);
        Task Add(Utilisateur userModel);
        Task Edit(Utilisateur userModel);
        Task Remove(int utilisateurId);
        Task<Utilisateur> GetById(int utilisateurId);
        Task<Utilisateur> GetByEmail(string email);
        Task<IEnumerable<Utilisateur>> GetAll();
        Task<Utilisateur> GetByUsername(string username);
    }
}
