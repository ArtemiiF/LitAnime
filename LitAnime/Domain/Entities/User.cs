using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LitAnime.Domain
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }
        public User(string userName, string password, string email)
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpper();
            Email = email;
            NormalizedEmail = email.ToUpper();
            EmailConfirmed = false;
            PasswordHash = new PasswordHasher<User>().HashPassword(null, password);
            SecurityStamp = string.Empty;
            Minus = 1;
            Plus = 1;
            Reg_date = DateTime.Now.ToUniversalTime();
            Acc_type = "None";
        }
        public DateTime Reg_date { get; set; }
        public string? Acc_type { get; set; }
        public int Plus { get; set; } = 1;
        public int Minus { get; set; } = 1;
    }
}
