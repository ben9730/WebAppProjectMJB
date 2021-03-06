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
    public class AccessoriesController : Controller
    {
        private readonly WebAppProjectMJBContext _context;

        public AccessoriesController(WebAppProjectMJBContext context)
        {
            _context = context;
        }

        // GET: Accessories
        public async Task<IActionResult> Index()
        {
            var webAppProjectMJBContext = _context.Accessories.Include(a => a.Console);
            return View(await webAppProjectMJBContext.ToListAsync());
        }


        public async Task<IActionResult> Search(string query)
        {

            var webAppProjectMJBContext = _context.Accessories.Include(g => g.Console).Where(g => g.Name.Contains(query) || g.Console.Name.Contains(query) || g.Price.ToString().Contains(query));
            return PartialView(await webAppProjectMJBContext.ToListAsync());

        }


        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessories
                .Include(a => a.Console)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessories == null)
            {
                return NotFound();
            }

            return View(accessories);
        }

        [Authorize(Roles = "Admin")]
        // GET: Accessories/Create
        public IActionResult Create()
        {
            ViewData["GameConsoleId"] = new SelectList(_context.GameConsole, nameof(GameConsole.Id), nameof(GameConsole.Name));
            return View();
        }

        // POST: Accessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Summary,Image,GameConsoleId")] Accessories accessories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameConsoleId"] = new SelectList(_context.GameConsole, "Id", "Id", accessories.GameConsoleId);
            return View(accessories);
        }

        [Authorize(Roles = "Admin")]
        // GET: Accessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessories.FindAsync(id);
            if (accessories == null)
            {
                return NotFound();
            }
            ViewData["GameConsoleId"] = new SelectList(_context.GameConsole, "Id", "Name", accessories.GameConsoleId);
            return View(accessories);
        }

        // POST: Accessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Summary,Image,GameConsoleId")] Accessories accessories)
        {
            if (id != accessories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessoriesExists(accessories.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameConsoleId"] = new SelectList(_context.GameConsole, "Id", "Id", accessories.GameConsoleId);
            return View(accessories);
        }

        [Authorize(Roles = "Admin")]
        // GET: Accessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessories
                .Include(a => a.Console)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessories == null)
            {
                return NotFound();
            }

            return View(accessories);
        }

        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessories = await _context.Accessories.FindAsync(id);
            _context.Accessories.Remove(accessories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoriesExists(int id)
        {
            return _context.Accessories.Any(e => e.Id == id);
        }
    }
}
