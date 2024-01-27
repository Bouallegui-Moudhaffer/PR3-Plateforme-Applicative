using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore;
using PA.ApplicationCore.Domain;

namespace PA.ApplicationCore.Services
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthenticationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Utilisateur AuthenticateUser(LoginModel login)
        {
            // Retrieve the user from the database
            var user = _dbContext.Utilisateurs.FirstOrDefault(u => u.Username == login.Username);

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
