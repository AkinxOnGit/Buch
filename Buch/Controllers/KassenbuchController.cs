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
    public class KassenbuchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KassenbuchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kassenbuch
        public async Task<IActionResult> Index()
        {
              return _context.Kassenbuch != null ? 
                          View(await _context.Kassenbuch.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Kassenbuch'  is null.");
        }

        // GET: Kassenbuch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kassenbuch == null)
            {
                return NotFound();
            }

            var kassenbuch = await _context.Kassenbuch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kassenbuch == null)
            {
                return NotFound();
            }

            return View(kassenbuch);
        }

        // GET: Kassenbuch/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Rechnungen(int id)
        {
            if (id == null || _context.Kassenbuch == null)
            {
                return NotFound();
            }

            var kassenbuch = await _context.Kassenbuch
                .Include(k => k.Rechnungen)
                .FirstOrDefaultAsync(k => k.Id == id);

            return View(kassenbuch);
        }

        // POST: Kassenbuch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Anfangsbestand,Monat")] Kassenbuch kassenbuch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kassenbuch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kassenbuch);
        }

        // GET: Kassenbuch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kassenbuch == null)
            {
                return NotFound();
            }

            var kassenbuch = await _context.Kassenbuch.FindAsync(id);
            if (kassenbuch == null)
            {
                return NotFound();
            }
            return View(kassenbuch);
        }

        // POST: Kassenbuch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Anfangsbestand,Monat")] Kassenbuch kassenbuch)
        {
            if (id != kassenbuch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kassenbuch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KassenbuchExists(kassenbuch.Id))
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
            return View(kassenbuch);
        }

        // GET: Kassenbuch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kassenbuch == null)
            {
                return NotFound();
            }

            var kassenbuch = await _context.Kassenbuch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kassenbuch == null)
            {
                return NotFound();
            }

            return View(kassenbuch);
        }

        // POST: Kassenbuch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kassenbuch == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kassenbuch'  is null.");
            }
            var kassenbuch = await _context.Kassenbuch.FindAsync(id);
            if (kassenbuch != null)
            {
                _context.Kassenbuch.Remove(kassenbuch);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KassenbuchExists(int id)
        {
          return (_context.Kassenbuch?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
