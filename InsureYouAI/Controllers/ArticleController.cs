using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;

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
    }
}
