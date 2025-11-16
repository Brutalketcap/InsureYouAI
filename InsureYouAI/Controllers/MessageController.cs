using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.Controllers
{
    public class MessageController : Controller
    {
        private readonly InsureContext _context;

        public MessageController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult MessageList()
        {
            var value = _context.Messages.ToList();
            return View(value);
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMessage(Message Message)
        {
             Message.IsRead = false;   
             Message.SendDate = DateTime.Now;   
            _context.Messages.Add(Message);
            _context.SaveChanges();

            return RedirectToAction("MessageList");
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            _context.Messages.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var value = _context.Messages.Find(id);

            return View(value);
        }

        public IActionResult UpdateMessage(Message Message)
        {
            var value = _context.Messages.Update(Message);
            _context.SaveChanges();

            return RedirectToAction("MessageList");
        }
    }
}
