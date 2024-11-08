using DemoreCAPTCHA.DAL.Entities;
using DemoreCAPTCHA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoreCAPTCHA.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;


        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel logins)
        {
            var result = await _signInManager.PasswordSignInAsync(logins.Mail, logins.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Geçersiz Kullanıcı Bilgisi.");
            return View();
        }
    }
}
