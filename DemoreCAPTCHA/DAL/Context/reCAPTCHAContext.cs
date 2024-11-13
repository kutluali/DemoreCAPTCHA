using DemoreCAPTCHA.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoreCAPTCHA.DAL.Context
{
    public class reCAPTCHAContext:IdentityDbContext<AppUser, AppRole, int>
    {

        public reCAPTCHAContext(DbContextOptions<reCAPTCHAContext> options) : base(options) 
        {
        }

        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<ContactFormReCAPTCHA> ContactFormReCAPTCHAS { get; set; }
        
    }
}
