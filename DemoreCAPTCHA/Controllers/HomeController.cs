using DemoreCAPTCHA.DAL.Context;
using DemoreCAPTCHA.DAL.Entities;
using DemoreCAPTCHA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DemoreCAPTCHA.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly reCAPTCHAContext _context;

        public HomeController(reCAPTCHAContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult ContactForm()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult ContactForm(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ContactForms.Add(model);
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Mesajýnýz baþarýyla gönderildi." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = "Veritabanýna kayýt sýrasýnda bir hata oluþtu: " + ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Form verileri geçersiz." });
            }
        }
    }
}
