using Microsoft.AspNetCore.Identity;

namespace DemoreCAPTCHA.DAL.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
