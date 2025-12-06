using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class AboutItmeController : Controller
    {
        private readonly InsureContext _context;

        public AboutItmeController(InsureContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AboutItemList()
        {
            var value = _context.AboutItems.ToList();

            return View(value);
        }
        [HttpGet]
        public IActionResult CreateAboutItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAboutItem(AboutItem aboutItem)
        {
            _context.AboutItems.Add(aboutItem);
            _context.SaveChanges();

            return RedirectToAction("AboutItemList");
        }

        public IActionResult DeleteAboutItme(int id)
        {
            var value = _context.AboutItems.Find(id);
            _context.AboutItems.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("AboutItemList");
        }

        [HttpGet]
        public IActionResult UpdateAboutItme(int id)
        {
            var value = _context.AboutItems.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateAboutItme(AboutItem aboutItem)
        {
            _context.AboutItems.Update(aboutItem);
            _context.SaveChanges();
            return RedirectToAction("AboutItemList");
        }
        [HttpGet]
        public async Task<IActionResult> CreateAboutItemWhitGoogleGemini()
        {
            string apiKey = "";
            var model = "gemini-1.5-flash";
            string url = $"https://generativelanguage.googleapis.com/v1/models/{model}:generateContent?key={apiKey}";
            var requestBody = new
            {
                content = new[]
                {
                    new
                    {
                        parts=new[]
                        {
                            new
                            {
                                text="KurumsaL bir sigorta firması için etkileyici, güven verici ve profesyonel bir' Hakkımızda alanları (about item)' yazısı oluştur. Örneğin: 'Geleceğinizi güvence altına alan kapsamlı sigorta çözümleri sunuyoruz. 'şeklinde veya bunun gibi ve buna benzer daha zengin içerikler gelsin. En az 10 tane item istiyorum."
                            }
                        }
                    }
                }
            };
            var content = new StringContent(JsonConvert.SerializeObject(requestBody),Encoding.UTF8,"application/json");
            
            var client = new HttpClient();
            var response = await client.PostAsync(url,content);

            var requestJson = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(requestJson);
            var abaoutItemsText = jsonDoc.RootElement
                                     .GetProperty("candidates")[0]
                                     .GetProperty("content")
                                     .GetProperty("pars")[0]
                                     .GetProperty("text")
                                     .GetString();

            ViewBag.value = abaoutItemsText;


            return View();
        }
    }
}
