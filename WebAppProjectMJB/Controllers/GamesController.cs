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
    public class GamesController : Controller
    {
        private readonly WebAppProjectMJBContext _context;

        public GamesController(WebAppProjectMJBContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var webAppProjectMJBContext = _context.Game.Include(g => g.Console);
            return View(await webAppProjectMJBContext.ToListAsync());
        }

        //Main Search Page
        public async Task<IActionResult> GameSearch()
        {
            var webAppProjectMJBContext = _context.Game.Include(g => g.Console);
            return View(await webAppProjectMJBContext.ToListAsync());
        }

        //דרך ליצור מנוע חיפוש בעמוד שלנו, מה שהפונצקיה מקבלת זה שם המחרוזת והוא זהה גם בוויו שלנו
        //השימוש ב"" עם אינדקס זה להגיד לו באיזה עמוד לחפש תפונקציה
        //the Where func do the filtering by game name in this case
       
        //main search function

        public async Task<IActionResult> Search(string query)
        {

            var webAppProjectMJBContext = _context.Game.Include(g => g.Console).Where(g => g.Name.Contains(query) || g.Console.Name.Contains(query)||g.Price.ToString().Contains(query));
            return PartialView(await webAppProjectMJBContext.ToListAsync());
        }



        //do this if want to add more console game page to the web
        public async Task<IActionResult> NintendoSwitch()
        {
            var webAppProjectMJBContext = _context.Game.Include(g => g.Console);
            return View(await webAppProjectMJBContext.ToListAsync());

        }

        public async Task<IActionResult> XboxOne()
        {
            var webAppProjectMJBContext = _context.Game.Include(g => g.Console);
            return View(await webAppProjectMJBContext.ToListAsync());
        }




        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Console)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public async Task<IActionResult> Cart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Console)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }


        [Authorize(Roles = "Admin")]
        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["GameConsoleId"] = new SelectList(_context.Set<GameConsole>(), nameof(GameConsole.Id), nameof(GameConsole.Name));
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Summary,CoverImage,Trailer,GameConsoleId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameConsoleId"] = new SelectList(_context.Set<GameConsole>(), nameof(game.GameConsoleId), nameof(game.Console), game.GameConsoleId);
            return View(game);
        }

        [Authorize(Roles = "Admin")]
        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["GameConsoleId"] = new SelectList(_context.Set<GameConsole>(), "Id", "Name", game.GameConsoleId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Summary,CoverImage,Trailer,GameConsoleId")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["GameConsoleId"] = new SelectList(_context.Set<GameConsole>(), "Id", "Id", game.GameConsoleId);
            return View(game);
        }

        [Authorize(Roles = "Admin")]
        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Console)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
