using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace InsureYouAI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly InsureContext _context;

        public ArticleController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult ArticleList()
        {
            var values = _context.Articles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateArticle(Article article)
        {
            article.CreatedDate = DateTime.Now;
            var values = _context.Articles.Add(article);
            _context.SaveChanges();
            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public IActionResult DeleteArticle(int id)
        {
            var value = _context.Articles.Find(id);
            _context.Articles.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public IActionResult UpdateArticle(int id)
        {
            var value = _context.Articles.Find(id);

            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateArticle(Article article)
        {
            var value = _context.Articles.Update(article);
            _context.SaveChanges();

            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public IActionResult CreateArticleWithOpenAI()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticleWithOpenAI(string prompt)
        {
            var apiKey = "";

            using var clinet = new HttpClient();
            clinet.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestDate = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role="system", content="sen bir sigorta şirketi için çalışan, içerik yazarlığı yapan bir yapay zekasın.Kullanıcının verdiği özet ve anaktar kelimelerine göre, sigortacılık sektörüyle ilgili makele üret. en Fazla 500 karakterli" },
                    new { role="user", content=prompt}
                },
                temperature = 0.7
            };
            var response = await clinet.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestDate);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                var content = result.choices[0].message.content;
                ViewBag.article= content;
            }
            else
            {
                ViewBag.article= "Bir hata oluştu: " + response.StatusCode;
            }
            return View();
        }
        public class OpenAIResponse
        {
            public List<Choice> choices { get; set; }
        }
        public class Choice
        {
            public Message message { get; set; }
        }
        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
    }
}
     