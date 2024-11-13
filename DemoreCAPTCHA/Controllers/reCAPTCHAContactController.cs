using DemoreCAPTCHA.DAL.Context;
using DemoreCAPTCHA.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v3;

namespace DemoreCAPTCHA.Controllers
{
    public class ContactController : Controller
    {
        private readonly reCAPTCHAContext _context;
        private readonly IreCAPTCHASiteVerifyV3 _siteVerify;

        public ContactController(reCAPTCHAContext context, IreCAPTCHASiteVerifyV3 siteVerify)
        {
            _context = context;
            _siteVerify = siteVerify;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormReCAPTCHA model)
        {
            if (ModelState.IsValid)
            {
                var response = await _siteVerify.Verify(new reCAPTCHASiteVerifyRequest
                {
                    Response = model.RecaptchaToken,
                    RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
                });

                if (!response.Success || response.Score < 0.5)
                {
                    return Json(new { success = false, errorMessage = "reCAPTCHA doğrulaması başarısız." });
                }

                try
                {
                    _context.ContactFormReCAPTCHAS.Add(model);
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Mesajınız başarıyla gönderildi." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = "Veritabanına kayıt sırasında bir hata oluştu: " + ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Form verileri geçersiz." });
            }
        }

    }
}
