﻿using DemoreCAPTCHA.DAL.Context;
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
