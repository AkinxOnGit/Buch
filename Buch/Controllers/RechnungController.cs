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
    public class RechnungController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RechnungController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rechnung
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rechnung.
                Include(r => r.Kassenbuch);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rechnung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rechnung == null)
            {
                return NotFound();
            }

            var rechnung = await _context.Rechnung
                .Include(r => r.Kassenbuch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rechnung == null)
            {
                return NotFound();
            }

            return View(rechnung);
        }
        // GET: Rechnung/Create
        public IActionResult Create()
        {
            ViewBag.KassenbuchId = new SelectList(_context.Kassenbuch, "Id", "Monat");
            ViewBag.ArtList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Eingangsrechnung", Text = "Eingangsrechnung" },
            new SelectListItem { Value = "Ausgangsrechnung", Text = "Ausgangsrechnung" }
        };
            return View();
        }

        // POST: Rechnung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Datum,Betrag,Art,KassenbuchId")] Rechnung rechnung)
        {

                _context.Add(rechnung);
                await _context.SaveChangesAsync();
            
            ViewBag.KassenbuchId = new SelectList(_context.Kassenbuch, "Id", "Monat");
            ViewBag.ArtList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Eingangsrechnung", Text = "Eingangsrechnung" },
            new SelectListItem { Value = "Ausgangsrechnung", Text = "Ausgangsrechnung" }
        };
            return RedirectToAction(nameof(Index));
        }



        // GET: Rechnung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rechnung == null)
            {
                return NotFound();
            }

            var rechnung = await _context.Rechnung.FindAsync(id);
            if (rechnung == null)
            {
                return NotFound();
            }
            ViewBag.KassenbuchId = new SelectList(_context.Kassenbuch, "Id", "Monat");
            ViewBag.ArtList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Eingangsrechnung", Text = "Eingangsrechnung" },
            new SelectListItem { Value = "Ausgangsrechnung", Text = "Ausgangsrechnung" }
        }; return View(rechnung);
        }

        // POST: Rechnung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rechnung rechnung)
        {
            if (id != rechnung.Id)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(rechnung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechnungExists(rechnung.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            
            ViewBag.KassenbuchId = new SelectList(_context.Kassenbuch, "Id", "Monat");
            ViewBag.ArtList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Eingangsrechnung", Text = "Eingangsrechnung" },
            new SelectListItem { Value = "Ausgangsrechnung", Text = "Ausgangsrechnung" }
        };
            return RedirectToAction(nameof(Index));
        }

        // GET: Rechnung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rechnung == null)
            {
                return NotFound();
            }

            var rechnung = await _context.Rechnung
                .Include(r => r.Kassenbuch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rechnung == null)
            {
                return NotFound();
            }

            return View(rechnung);
        }

        // POST: Rechnung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rechnung == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rechnung'  is null.");
            }
            var rechnung = await _context.Rechnung.FindAsync(id);
            if (rechnung != null)
            {
                _context.Rechnung.Remove(rechnung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechnungExists(int id)
        {
            return (_context.Rechnung?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
