using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppProjectMJB.Data;
using WebAppProjectMJB.Models;

namespace WebAppProjectMJB.Controllers
{
    public class FeedbackMessagesController : Controller
    {
        private readonly WebAppProjectMJBContext _context;

        public FeedbackMessagesController(WebAppProjectMJBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: FeedbackMessages
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeedbackMessage.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: FeedbackMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackMessage = await _context.FeedbackMessage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedbackMessage == null)
            {
                return NotFound();
            }

            return View(feedbackMessage);
        }

        // GET: FeedbackMessages/Create
        public IActionResult ConnectUs()
        {
            return View();
        }

        // POST: FeedbackMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConnectUs([Bind("Id,Name,Email,PhoneNumber,Message")] FeedbackMessage feedbackMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedbackMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"Home");
                //return the user to index page in home controller 
            }
            return View(feedbackMessage);
        }



        [Authorize(Roles = "Admin")]
        // GET: FeedbackMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackMessage = await _context.FeedbackMessage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedbackMessage == null)
            {
                return NotFound();
            }

            return View(feedbackMessage);
        }

        // POST: FeedbackMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedbackMessage = await _context.FeedbackMessage.FindAsync(id);
            _context.FeedbackMessage.Remove(feedbackMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackMessageExists(int id)
        {
            return _context.FeedbackMessage.Any(e => e.Id == id);
        }
    }
}
