using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PontoColeta.Models;

namespace PontoColeta.Controllers
{
    public class CoordinatesController : Controller
    {
        private readonly Context _context;

        public CoordinatesController(Context context)
        {
            _context = context;
        }

        // GET: Coordinates
        public async Task<IActionResult> Index(int? idItem)
        {
            if (idItem == null)
                return View(await _context.Coordinates.Include(c => c.Items).ToListAsync());
            else
                return View(await _context.Coordinates.Include(c => c.Items).Where(i => i.IdRefItem == idItem).ToListAsync());
        }

        // GET: Coordinates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinates = await _context.Coordinates
                .Include(c => c.Items)
                .FirstOrDefaultAsync(m => m.IdCoordinates == id);
            if (coordinates == null)
            {
                return NotFound();
            }

            return View(coordinates);
        }

        // GET: Coordinates/Create
        public IActionResult Create()
        {
            ViewData["IdRefItem"] = new SelectList(_context.Items, "IdItem", "IdItem");
            return View();
        }

        // POST: Coordinates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCoordinates,Latitude,Longitude,NomeLugar,IdRefItem")] Coordinates coordinates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordinates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRefItem"] = new SelectList(_context.Items, "IdItem", "IdItem", coordinates.IdRefItem);
            return View(coordinates);
        }

        // GET: Coordinates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinates = await _context.Coordinates.FindAsync(id);
            if (coordinates == null)
            {
                return NotFound();
            }
            ViewData["IdRefItem"] = new SelectList(_context.Items, "IdItem", "IdItem", coordinates.IdRefItem);
            return View(coordinates);
        }

        // POST: Coordinates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCoordinates,Latitude,Longitude,NomeLugar,IdRefItem")] Coordinates coordinates)
        {
            if (id != coordinates.IdCoordinates)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordinates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinatesExists(coordinates.IdCoordinates))
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
            ViewData["IdRefItem"] = new SelectList(_context.Items, "IdItem", "IdItem", coordinates.IdRefItem);
            return View(coordinates);
        }

        // GET: Coordinates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinates = await _context.Coordinates
                .Include(c => c.Items)
                .FirstOrDefaultAsync(m => m.IdCoordinates == id);
            if (coordinates == null)
            {
                return NotFound();
            }

            return View(coordinates);
        }

        // POST: Coordinates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordinates = await _context.Coordinates.FindAsync(id);
            _context.Coordinates.Remove(coordinates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinatesExists(int id)
        {
            return _context.Coordinates.Any(e => e.IdCoordinates == id);
        }
    }
}
