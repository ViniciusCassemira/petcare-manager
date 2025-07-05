using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using system_petshop.Data;
using system_petshop.Models;

namespace system_petshop.Controllers
{
    [Authorize]
    public class VeterinarianController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeterinarianController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veterinarian
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Veterinarian.Include(v => v.Specialty);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Veterinarian/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarian = await _context.Veterinarian
                .Include(v => v.Specialty)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (veterinarian == null)
            {
                return NotFound();
            }

            return View(veterinarian);
        }

        // GET: Veterinarian/Create
        public IActionResult Create()
        {
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "Description");
            return View();
        }

        // POST: Veterinarian/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cfmv,SpecialtyId,UserId,Name,Email,PassHash")] Veterinarian veterinarian)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var item in allErrors)
            {
                Console.WriteLine(item.ErrorMessage);
            }
            if (ModelState.IsValid)
            {
                _context.Add(veterinarian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "Description", veterinarian.SpecialtyId);
            return View(veterinarian);
        }

        // GET: Veterinarian/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarian = await _context.Veterinarian.FindAsync(id);
            if (veterinarian == null)
            {
                return NotFound();
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "SpecialtyId", veterinarian.SpecialtyId);
            return View(veterinarian);
        }

        // POST: Veterinarian/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cfmv,SpecialtyId,UserId,Name,Email,PassHash")] Veterinarian veterinarian)
        {
            if (id != veterinarian.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinarian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarianExists(veterinarian.UserId))
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
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "SpecialtyId", "SpecialtyId", veterinarian.SpecialtyId);
            return View(veterinarian);
        }

        // GET: Veterinarian/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarian = await _context.Veterinarian
                .Include(v => v.Specialty)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (veterinarian == null)
            {
                return NotFound();
            }

            return View(veterinarian);
        }

        // POST: Veterinarian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinarian = await _context.Veterinarian.FindAsync(id);
            if (veterinarian != null)
            {
                _context.Veterinarian.Remove(veterinarian);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarianExists(int id)
        {
            return _context.Veterinarian.Any(e => e.UserId == id);
        }
    }
}
