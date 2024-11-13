using DemoreCAPTCHA.DAL.Context;
using DemoreCAPTCHA.DAL.Entities;
using DemoreCAPTCHA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v3;
using System.Diagnostics;

namespace DemoreCAPTCHA.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly reCAPTCHAContext _context;
        private readonly IreCAPTCHASiteVerifyV3 _siteVerify;

        public HomeController(reCAPTCHAContext context, IreCAPTCHASiteVerifyV3 siteVerify)
        {
            _context = context;
            _siteVerify = siteVerify;
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
        public async Task<IActionResult> ContactForm(ContactForm model)
        {
            if (string.IsNullOrEmpty(model.RecaptchaToken))
            {
                return Json(new { success = false, errorMessage = "reCAPTCHA do�rulamas� ba�ar�s�z." });
            }

            var response = await _siteVerify.Verify(new reCAPTCHASiteVerifyRequest
            {
                Response = model.RecaptchaToken,
                RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
            });

            if (!response.Success || response.Score < 0.5)
            {
                return Json(new { success = false, errorMessage = "reCAPTCHA do�rulamas� ba�ar�s�z." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ContactForms.Add(model);
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Mesaj�n�z ba�ar�yla g�nderildi." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = "Veritaban�na kay�t s�ras�nda bir hata olu�tu: " + ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Form verileri ge�ersiz." });
            }
        }

    }
}
