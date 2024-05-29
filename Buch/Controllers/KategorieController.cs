using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Buch.Data;
using Buch.Models;

namespace Buch.Controllers
{
    public class KategorieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategorieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kategorie
        public async Task<IActionResult> Index()
        {
              return _context.Kategorie != null ? 
                          View(await _context.Kategorie.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Kategorie'  is null.");
        }

        // GET: Kategorie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kategorie == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategorie == null)
            {
                return NotFound();
            }

            return View(kategorie);
        }

        // GET: Kategorie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategorie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Farbe")] Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategorie);
        }

        // GET: Kategorie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kategorie == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie.FindAsync(id);
            if (kategorie == null)
            {
                return NotFound();
            }
            return View(kategorie);
        }

        // POST: Kategorie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Farbe")] Kategorie kategorie)
        {
            if (id != kategorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorieExists(kategorie.Id))
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
            return View(kategorie);
        }

        // GET: Kategorie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kategorie == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategorie == null)
            {
                return NotFound();
            }

            return View(kategorie);
        }

        // POST: Kategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kategorie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kategorie'  is null.");
            }
            var kategorie = await _context.Kategorie.FindAsync(id);
            if (kategorie != null)
            {
                _context.Kategorie.Remove(kategorie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategorieExists(int id)
        {
          return (_context.Kategorie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
