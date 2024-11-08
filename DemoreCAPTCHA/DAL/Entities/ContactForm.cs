using System.ComponentModel.DataAnnotations;

namespace DemoreCAPTCHA.DAL.Entities
{
    public class ContactForm
    {
        public int ContactFormId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime SentDate { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;

        //[Required(ErrorMessage = "reCAPTCHA doğrulaması gereklidir.")]
        //public string RecaptchaToken { get; set; }
    }
}
