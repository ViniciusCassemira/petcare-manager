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
    public class ConsultationExamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultationExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConsultationExam
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConsultationExam.Include(c => c.Consultation).Include(c => c.Exam);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConsultationExam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationExam = await _context.ConsultationExam
                .Include(c => c.Consultation)
                .Include(c => c.Exam)
                .FirstOrDefaultAsync(m => m.ConsultationExamId == id);
            if (consultationExam == null)
            {
                return NotFound();
            }

            return View(consultationExam);
        }

        // GET: ConsultationExam/Create
        public IActionResult Create()
        {
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId");
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId");
            return View();
        }

        // POST: ConsultationExam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationExamId,Amount,UnitPrice,ConsultationId,ExamId")] ConsultationExam consultationExam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultationExam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId", consultationExam.ConsultationId);
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", consultationExam.ExamId);
            return View(consultationExam);
        }

        // GET: ConsultationExam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationExam = await _context.ConsultationExam.FindAsync(id);
            if (consultationExam == null)
            {
                return NotFound();
            }
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId", consultationExam.ConsultationId);
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", consultationExam.ExamId);
            return View(consultationExam);
        }

        // POST: ConsultationExam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultationExamId,Amount,UnitPrice,ConsultationId,ExamId")] ConsultationExam consultationExam)
        {
            if (id != consultationExam.ConsultationExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultationExam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExamExists(consultationExam.ConsultationExamId))
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
            ViewData["ConsultationId"] = new SelectList(_context.Consultation, "ConsultationId", "ConsultationId", consultationExam.ConsultationId);
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", consultationExam.ExamId);
            return View(consultationExam);
        }


        // GET: ConsultationExam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationExam = await _context.ConsultationExam
                .Include(c => c.Consultation)
                .Include(c => c.Exam)
                .FirstOrDefaultAsync(m => m.ConsultationExamId == id);
            if (consultationExam == null)
            {
                return NotFound();
            }

            return View(consultationExam);
        }

        // POST: ConsultationExam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultationExam = await _context.ConsultationExam.FindAsync(id);
            if (consultationExam != null)
            {
                _context.ConsultationExam.Remove(consultationExam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExamExists(int id)
        {
            return _context.ConsultationExam.Any(e => e.ConsultationExamId == id);
        }
    }
}
