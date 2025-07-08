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
    public class ConsultationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consultation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Consultation.Include(c => c.Animal).Include(c => c.Veterinarian);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Consultation By User 
        public async Task<IActionResult> ConsultationByUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultations = await _context.Consultation
                .Include(c => c.Animal)
                .Include(c => c.Veterinarian)
                .Where(c => c.Animal.ClientId == id)
                .ToListAsync();

            return View(consultations);
        }

        // GET: Consultation By Veterinarian
        public async Task<IActionResult> ConsultationByVeterinarian(int? veterinarianId)
        {
            if (veterinarianId == null)
            {
                return NotFound();
            }

            var consultations = await _context.Consultation
                                      .Include(c => c.Animal)
                                      .Include(c => c.Veterinarian)
                                      .Where(c => c.VeterinarianId == veterinarianId)
                                      .ToListAsync();

            return View(consultations);
        }

        // GET: Consultation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation
                .Include(c => c.Animal)
                .Include(c => c.Veterinarian)
                .FirstOrDefaultAsync(m => m.ConsultationId == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // GET: Consultation/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "AnimalId");
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarian, "UserId", "UserId");
            return View();
        }

        // POST: Consultation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationId,ConsultationDate,Note,AnimalId,VeterinarianId")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "AnimalId", consultation.AnimalId);
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarian, "UserId", "UserId", consultation.VeterinarianId);
            return View(consultation);
        }

        // GET: Consultation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Name", consultation.AnimalId);
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarian, "UserId", "Name", consultation.VeterinarianId);
            return View(consultation);
        }

        // POST: Consultation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultationId,ConsultationDate,Note,AnimalId,VeterinarianId")] Consultation consultation)
        {
            if (id != consultation.ConsultationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExists(consultation.ConsultationId))
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "AnimalId", "Name", consultation.AnimalId);
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarian, "UserId", "Name", consultation.VeterinarianId);
            return View(consultation);
        }

        // GET: Consultation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation
                .Include(c => c.Animal)
                .Include(c => c.Veterinarian)
                .FirstOrDefaultAsync(m => m.ConsultationId == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: Consultation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultation = await _context.Consultation.FindAsync(id);
            if (consultation != null)
            {
                _context.Consultation.Remove(consultation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(int id)
        {
            return _context.Consultation.Any(e => e.ConsultationId == id);
        }
    }
}