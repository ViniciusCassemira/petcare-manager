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
    //[Authorize]
    //[Authorize(Roles = "client")]
    public class AnimalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animal
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Animal.Include(a => a.Breed).Include(a => a.Client).Include(a => a.Species);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Animal pelo ID do usuário
        public async Task<IActionResult> AnimalByUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animals = await _context.Animal
                .Include(a => a.Breed)
                .Include(a => a.Client)
                .Include(a => a.Species)
                .Where(a => a.ClientId == id)
                .ToListAsync();

            return View(animals);
        }

        // GET: Animal/Create
        public IActionResult Create()
        {   
            ViewData["BreedId"] = new SelectList(_context.Breed, "BreedId", "BreedId");
            ViewData["ClientId"] = new SelectList(_context.Client, "UserId", "UserId");
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "SpeciesId");
            return View();
        }

        // POST: Animal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Name,Description,DateBirth,BreedId,SpeciesId,ClientId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "BreedId", "BreedId", animal.BreedId);
            ViewData["ClientId"] = new SelectList(_context.Client, "UserId", "UserId", animal.ClientId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "SpeciesId", animal.SpeciesId);
            return View(animal);
        }

        // GET: Animal/Create by user
        public IActionResult CreateByUser()
        {
            ViewData["BreedId"] = new SelectList(_context.Breed, "BreedId", "Name");
            ViewData["ClientId"] = new SelectList(_context.Client, "UserId", "UserId");
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name");
            return View();
        }

        // POST: Animal/Create by user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByUser([Bind("AnimalId,Name,Description,DateBirth,BreedId,SpeciesId,ClientId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction("AnimalByUser", "Animal", new { id = animal.ClientId });
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "BreedId", "Name", animal.BreedId);
            ViewData["ClientId"] = new SelectList(_context.Client, "UserId", "UserId", animal.ClientId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", animal.SpeciesId);
            return View(animal);
        }

        // GET: Animal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "BreedId", "Name", animal.BreedId);
            ViewData["ClientId"] = new SelectList(_context.Client, "UserId", "UserId", animal.ClientId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", animal.SpeciesId);
            return View(animal);
        }

        // POST: Animal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,Description,DateBirth,BreedId,SpeciesId,ClientId")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
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
            ViewData["BreedId"] = new SelectList(_context.Breed, "BreedId", "Name", animal.BreedId);
            ViewData["ClientId"] = new SelectList(_context.Client, "UserId", "UserId", animal.ClientId);
            ViewData["SpeciesId"] = new SelectList(_context.Species, "SpeciesId", "Name", animal.SpeciesId);
            return View(animal);
        }

        // GET: Animal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Breed)
                .Include(a => a.Client)
                .Include(a => a.Species)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.AnimalId == id);
        }
    }
}