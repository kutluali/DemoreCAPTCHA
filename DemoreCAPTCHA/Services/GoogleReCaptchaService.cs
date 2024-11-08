using DemoreCAPTCHA.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MySite.Services
{
    public class GoogleReCaptchaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GoogleReCAPTCHA _reCaptchaSettings;

        public GoogleReCaptchaService(IHttpClientFactory httpClientFactory, IOptions<GoogleReCAPTCHA> reCaptchaSettings)
        {
            _httpClientFactory = httpClientFactory;
            _reCaptchaSettings = reCaptchaSettings.Value;
        }

        public async Task<bool> VerifyToken(string token)
        {
            var secretKey = _reCaptchaSettings.SecretKey;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}");

            var reCaptchaResponse = JsonSerializer.Deserialize<ReCaptchaResponse>(response);

            return reCaptchaResponse.Success && reCaptchaResponse.Score >= 0.5;
        }
    }
    public class ReCaptchaResponse
    {
        public bool Success { get; set; }
        public float Score { get; set; }
        public string Action { get; set; }
        public string Challenge_ts { get; set; }
        public string Hostname { get; set; }
    }
}
