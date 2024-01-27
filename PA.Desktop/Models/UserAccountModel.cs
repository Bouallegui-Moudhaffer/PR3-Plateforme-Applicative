namespace PA.Desktop.Models
{
    public class UserAccountModel
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Nom { get; set; } // Use Nom to be consistent with Utilisateur
        public string Email { get; set; } // Email
        public string Role { get; set; } // Role
    }
}
