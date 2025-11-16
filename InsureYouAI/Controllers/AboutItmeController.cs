using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
            var value= _context.AboutItems.ToList();

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
    }
}
