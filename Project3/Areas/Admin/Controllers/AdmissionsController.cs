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
    public class AdmissionsController : Controller
    {
        private readonly TestContext _context;

        public AdmissionsController(TestContext context)
        {
            _context = context;
        }

        // GET: Admin/Admissions
        public async Task<IActionResult> Index()
        {
            var testContext = _context.Admissions.Include(a => a.Account);
            return View(await testContext.ToListAsync());
        }

        // GET: Admin/Admissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Admissions == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.AdmissionId == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // GET: Admin/Admissions/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserId");
            return View();
        }

        // POST: Admin/Admissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdmissionId,AccountId,FullName,Email,Address,Phone,Birthday,Maths,Englishs")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserId", admission.AccountId);
            return View(admission);
        }

        // GET: Admin/Admissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Admissions == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserId", admission.AccountId);
            return View(admission);
        }

        // POST: Admin/Admissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdmissionId,AccountId,FullName,Email,Address,Phone,Birthday,Maths,Englishs")] Admission admission)
        {
            if (id != admission.AdmissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionExists(admission.AdmissionId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserId", admission.AccountId);
            return View(admission);
        }

        // GET: Admin/Admissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Admissions == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.AdmissionId == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // POST: Admin/Admissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admissions == null)
            {
                return Problem("Entity set 'TestContext.Admissions'  is null.");
            }
            var admission = await _context.Admissions.FindAsync(id);
            if (admission != null)
            {
                _context.Admissions.Remove(admission);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionExists(int id)
        {
          return (_context.Admissions?.Any(e => e.AdmissionId == id)).GetValueOrDefault();
        }
    }
}
