using InsureYouAI.Context;
using InsureYouAI.Controllers;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;


namespace InsureYouAI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly InsureContext _context;

        public ServiceController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult ServiceList()
        {
            var valeu = _context.Services.ToList();
            return View(valeu);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }

        public IActionResult DeleteService(int id)
        {
            var vaule = _context.Services.Find(id);
            _context.Services.Remove(vaule);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        [HttpGet]
        public IActionResult UpdateService(int id)
        {
            var value = _context.Services.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateService(Service service)
        {
            var value = _context.Services.Update(service);
            _context.SaveChanges();

            return RedirectToAction("ServiceList");
        }


        public async Task<IActionResult> CreateServiceWithAnthropicClaude()
        {

            string apiKey = "";

            string prompt = "Bir sigorata şirketi için hizmetler bölümü hazırlamanı istiyorum. Burada 5 farklı hizmet olmalı. Bana maksimun 100 karakterden oluşan cümlelerle 5 tane hizmet içeriği yazar mısın?";

            using var clent = new HttpClient();
            clent.BaseAddress = new Uri("https://api.anthropic.com/");
            clent.DefaultRequestHeaders.Add("x-api-key", apiKey);
            clent.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
            clent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody = new
            {
                model = "claude-3-opus-20241229",
                max_tokens = 100,
                temperature = 0.5,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };


            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody));

            var response = await clent.PostAsync("v1/message", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.service = new List<string>
                {
                    $"Claude api'den cevep alınamandı. Hata: {response.StatusCode}"
                };
                return View();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);

            var fullText = doc.RootElement
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            var services = fullText.Split('/')
                                    .Where(x => !string.IsNullOrWhiteSpace(x))
                                    .Select(x => x.TrimStart('1', '2', '3', '4', '5', '.', ' '))
                                    .ToList();
            ViewBag.service = services;

            return View();
        }

    }
}

