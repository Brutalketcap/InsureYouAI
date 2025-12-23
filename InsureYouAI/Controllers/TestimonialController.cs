using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Net.Http.Headers;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly InsureContext _context;

        public TestimonialController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult TestimonialList()
        {
            var value = _context.Testimonials.ToList();

            return View(value);
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        public IActionResult DeleteTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            _context.Testimonials.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            return View(value);
        }

        public IActionResult UpdateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            _context.SaveChanges();

            return RedirectToAction("TestimonialList");
        }
        public async Task<IActionResult> CreateAboutWhitGoogleGemini()
         {
            // Google Gemini API entegrasyonu burada yapılacak

            string apiKey = "";

            string propt = "Bir sigorta şirketi için müşteri deneyimlerine dair yorum oluşturma istiyorum yani inglizce karşılığı ile: Testimonial. Bu alanda Türkçe olarak 6 tane yorum, 6 tane müsteri adı ve soyadı, bu  müşterilerin unvanı olsun. buna göre içeriği hazırla.";
            using var clent = new HttpClient();

            clent.BaseAddress = new Uri("https://api.anthropic.com/");
            clent.DefaultRequestHeaders.Add("x-api-key", apiKey);
            clent.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
            clent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody = new
            {
                Model = "cloud-3-opus-20241229",
                max_tokens = 100,
                temperature = 0.5,
                message = new[]
                {
                    new {
                        role= "user",
                        content= propt
                    }
                }

            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody));

            var response = await clent.PostAsync("v1/message", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.testimonials = new List<string>
                {
                    $"Claude api'den cevap alınamadı.\nHata: {response.StatusCode}"
                };
                return View();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);

            var fullText = doc.RootElement
                .GetProperty("content")[0]
                .GetProperty("Text")
                .GetString();

            var testimonials = fullText.Split('/')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.TrimStart('1','2','3','4','5','6','.',' '))
                .ToList();

            ViewBag.testimonials = testimonials;
            return View();
        }


    }
}