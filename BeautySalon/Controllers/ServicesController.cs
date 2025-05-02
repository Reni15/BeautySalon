using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySalon.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace BeautySalon.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServicesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: Services
        public async Task<IActionResult> IndexNails(string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return View(await _context.Services
                .Where(x => x.TypeServicesId == 19|| 
                x.TypeServicesId == 20 || 
                x.TypeServicesId == 21 || 
                x.TypeServicesId == 22 ||
                x.TypeServicesId == 23)
                .ToListAsync());
            }
            if (_context.Services == null)
            {
                return Problem("Context is empty");
            }
            var products = from m in _context.Services select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            return View(products.ToList()); //return View(await _context.Flowers.ToListAsync());
        }
          public async Task<IActionResult> IndexMakeUp(string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return View(await _context.Services
                .Where(x => x.TypeServicesId == 14||
                    x.TypeServicesId == 15||
                    x.TypeServicesId == 16||
                    x.TypeServicesId == 17||
                    x.TypeServicesId == 24||
                    x.TypeServicesId == 18)
                .ToListAsync());
            }
            if (_context.Services == null)
            {
                return Problem("Context is empty");
            }
            var products = from m in _context.Services select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            return View(products.ToList()); //return View(await _context.Flowers.ToListAsync());
        }

        public async Task<IActionResult> IndexProduct(string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return View(await _context.Services.Where(x => x.TypeServicesId == 2).ToListAsync());
            }
            if (_context.Services == null)
            {
                return Problem("Context is empty");
            }
            var products = from m in _context.Services select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            return View(products.ToList());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.TypeServices)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["TypeServicesId"] = new SelectList(_context.TypeServices, "Id", "Name");
            return View();
        }
        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,TypeServicesId,Description,URLimages,DateRegister")] Service service)
        {
            service.Description = "..";
            service.DateRegister = DateTime.Now;
            if (!ModelState.IsValid)
            { 
                ViewData["TypeServicesId"] = new SelectList(_context.TypeServices, "Id", "Name", service.TypeServicesId);
                return View(service);
            }
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexNails));
        }

        // GET: Services/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["TypeServicesId"] = new SelectList(_context.TypeServices, "Id", "Name", service.TypeServicesId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,TypeServicesId,Description,URLimages,DateRegister")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["TypeServicesId"] = new SelectList(_context.TypeServices, "Id", "Name", service.TypeServicesId);
                return View(service);
            }
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexNails));
        }

        // GET: Services/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.TypeServices)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexNails));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
