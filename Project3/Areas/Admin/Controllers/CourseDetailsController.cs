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
    public class CourseDetailsController : Controller
    {
        private readonly TestContext _context;

        public CourseDetailsController(TestContext context)
        {
            _context = context;
        }

        // GET: Admin/CourseDetails
        public async Task<IActionResult> Index()
        {
            var testContext = _context.CourseDetails.Include(c => c.Course);
            return View(await testContext.ToListAsync());
        }

        // GET: Admin/CourseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseDetails == null)
            {
                return NotFound();
            }

            var courseDetail = await _context.CourseDetails
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.CourseDetailsId == id);
            if (courseDetail == null)
            {
                return NotFound();
            }

            return View(courseDetail);
        }

        // GET: Admin/CourseDetails/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: Admin/CourseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseDetailsId,CourseId,CourseDetailName")] CourseDetail courseDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseDetail.CourseId);
            return View(courseDetail);
        }

        // GET: Admin/CourseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CourseDetails == null)
            {
                return NotFound();
            }

            var courseDetail = await _context.CourseDetails.FindAsync(id);
            if (courseDetail == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseDetail.CourseId);
            return View(courseDetail);
        }

        // POST: Admin/CourseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseDetailsId,CourseId,CourseDetailName")] CourseDetail courseDetail)
        {
            if (id != courseDetail.CourseDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseDetailExists(courseDetail.CourseDetailsId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseDetail.CourseId);
            return View(courseDetail);
        }

        // GET: Admin/CourseDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseDetails == null)
            {
                return NotFound();
            }

            var courseDetail = await _context.CourseDetails
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.CourseDetailsId == id);
            if (courseDetail == null)
            {
                return NotFound();
            }

            return View(courseDetail);
        }

        // POST: Admin/CourseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseDetails == null)
            {
                return Problem("Entity set 'TestContext.CourseDetails'  is null.");
            }
            var courseDetail = await _context.CourseDetails.FindAsync(id);
            if (courseDetail != null)
            {
                _context.CourseDetails.Remove(courseDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseDetailExists(int id)
        {
          return (_context.CourseDetails?.Any(e => e.CourseDetailsId == id)).GetValueOrDefault();
        }
    }
}
