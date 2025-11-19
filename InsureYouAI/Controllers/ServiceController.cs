using InsureYouAI.Context;
using InsureYouAI.Controllers;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
    
    }
}

