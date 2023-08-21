using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CentresController : Controller
    {
        private readonly TestContext _context;

        public CentresController(TestContext context)
        {
            _context = context;
        }

        // GET: Admin/Centres
        public async Task<IActionResult> Index()
        {
              return _context.Centres != null ? 
                          View(await _context.Centres.ToListAsync()) :
                          Problem("Entity set 'TestContext.Centres'  is null.");
        }

        // GET: Admin/Centres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Centres == null)
            {
                return NotFound();
            }

            var centre = await _context.Centres
                .FirstOrDefaultAsync(m => m.CentreId == id);
            if (centre == null)
            {
                return NotFound();
            }

            return View(centre);
        }

        // GET: Admin/Centres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Centres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CentreId,CentreName,Address,Telephone")] Centre centre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centre);
        }

        // GET: Admin/Centres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Centres == null)
            {
                return NotFound();
            }

            var centre = await _context.Centres.FindAsync(id);
            if (centre == null)
            {
                return NotFound();
            }
            return View(centre);
        }

        // POST: Admin/Centres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CentreId,CentreName,Address,Telephone")] Centre centre)
        {
            if (id != centre.CentreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentreExists(centre.CentreId))
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
            return View(centre);
        }

        // GET: Admin/Centres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Centres == null)
            {
                return NotFound();
            }

            var centre = await _context.Centres
                .FirstOrDefaultAsync(m => m.CentreId == id);
            if (centre == null)
            {
                return NotFound();
            }

            return View(centre);
        }

        // POST: Admin/Centres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Centres == null)
            {
                return Problem("Entity set 'TestContext.Centres'  is null.");
            }
            var centre = await _context.Centres.FindAsync(id);
            if (centre != null)
            {
                _context.Centres.Remove(centre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentreExists(int id)
        {
          return (_context.Centres?.Any(e => e.CentreId == id)).GetValueOrDefault();
        }
    }
}
