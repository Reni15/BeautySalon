using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySalon.Data;
using Microsoft.AspNetCore.Authorization;

namespace BeautySalon.Controllers
{
    public class TypeServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeServices.ToListAsync());
        }

        // GET: TypeServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeService = await _context.TypeServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeService == null)
            {
                return NotFound();
            }

            return View(typeService);
        }

        // GET: TypeServices/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateTimeRegister")] TypeService typeService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeService);
        }

        // GET: TypeServices/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeService = await _context.TypeServices.FindAsync(id);
            if (typeService == null)
            {
                return NotFound();
            }
            return View(typeService);
        }

        // POST: TypeServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateTimeRegister")] TypeService typeService)
        {
            if (id != typeService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeServiceExists(typeService.Id))
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
            return View(typeService);
        }

        // GET: TypeServices/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeService = await _context.TypeServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeService == null)
            {
                return NotFound();
            }

            return View(typeService);
        }

        // POST: TypeServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeService = await _context.TypeServices.FindAsync(id);
            if (typeService != null)
            {
                _context.TypeServices.Remove(typeService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeServiceExists(int id)
        {
            return _context.TypeServices.Any(e => e.Id == id);
        }
    }
}
