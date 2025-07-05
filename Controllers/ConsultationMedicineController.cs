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
    public class ConsultationMedicineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultationMedicineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConsultationMedicine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConsultationMedicine.Include(c => c.Consultation).Include(c => c.Medicine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConsultationMedicine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationMedicine = await _context.ConsultationMedicine
                .Include(c => c.Consultation)
                .Include(c => c.Medicine)
                .FirstOrDefaultAsync(m => m.ConsultationMedicineId == id);
            if (consultationMedicine == null)
            {
                return NotFound();
            }

            return View(consultationMedicine);
        }

        // GET: ConsultationMedicine/Create
        public IActionResult Create()
        {
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId");
            ViewData["MedicineId"] = new SelectList(_context.Medicine, "MedicineId", "MedicineId");
            return View();
        }

        // POST: ConsultationMedicine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationMedicineId,Amount,UnitPrice,ConsultationId,MedicineId")] ConsultationMedicine consultationMedicine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultationMedicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId", consultationMedicine.ConsultationId);
            ViewData["MedicineId"] = new SelectList(_context.Medicine, "MedicineId", "MedicineId", consultationMedicine.MedicineId);
            return View(consultationMedicine);
        }

        // GET: ConsultationMedicine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationMedicine = await _context.ConsultationMedicine.FindAsync(id);
            if (consultationMedicine == null)
            {
                return NotFound();
            }
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId", consultationMedicine.ConsultationId);
            ViewData["MedicineId"] = new SelectList(_context.Medicine, "MedicineId", "MedicineId", consultationMedicine.MedicineId);
            return View(consultationMedicine);
        }

        // POST: ConsultationMedicine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultationMedicineId,Amount,UnitPrice,ConsultationId,MedicineId")] ConsultationMedicine consultationMedicine)
        {
            if (id != consultationMedicine.ConsultationMedicineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultationMedicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationMedicineExists(consultationMedicine.ConsultationMedicineId))
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
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId", consultationMedicine.ConsultationId);
            ViewData["MedicineId"] = new SelectList(_context.Medicine, "MedicineId", "MedicineId", consultationMedicine.MedicineId);
            return View(consultationMedicine);
        }

        // GET: ConsultationMedicine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationMedicine = await _context.ConsultationMedicine
                .Include(c => c.Consultation)
                .Include(c => c.Medicine)
                .FirstOrDefaultAsync(m => m.ConsultationMedicineId == id);
            if (consultationMedicine == null)
            {
                return NotFound();
            }

            return View(consultationMedicine);
        }

        // POST: ConsultationMedicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultationMedicine = await _context.ConsultationMedicine.FindAsync(id);
            if (consultationMedicine != null)
            {
                _context.ConsultationMedicine.Remove(consultationMedicine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationMedicineExists(int id)
        {
            return _context.ConsultationMedicine.Any(e => e.ConsultationMedicineId == id);
        }
    }
}
