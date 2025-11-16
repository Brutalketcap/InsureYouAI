using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly InsureContext _context;

        public CategoryController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var values = _context.Categorys.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            var values = _context.Categorys.Add(category);
            _context.SaveChanges();

            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categorys.Find(id);
            _context.Categorys.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categorys.Find(id);

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            var value = _context.Categorys.Update(category);
            _context.SaveChanges();

            return RedirectToAction("CategoryList");
        }





    }
}
