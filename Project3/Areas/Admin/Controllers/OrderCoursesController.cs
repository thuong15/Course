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
    public class OrderCoursesController : Controller
    {
        private readonly TestContext _context;

        public OrderCoursesController(TestContext context)
        {
            _context = context;
        }
       
        // GET: Admin/OrderCourses
        public async Task<IActionResult> Index()
        {
            var testContext = _context.OrderCourses.Include(o => o.Account).Include(o => o.Course);
            return View(await testContext.ToListAsync());
        }

        // GET: Admin/OrderCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderCourses == null)
            {
                return NotFound();
            }

            var orderCourse = await _context.OrderCourses
                .Include(o => o.Account)
                .Include(o => o.Course)
                .FirstOrDefaultAsync(m => m.OrderCourseId == id);
            if (orderCourse == null)
            {
                return NotFound();
            }

            return View(orderCourse);
        }

        // GET: Admin/OrderCourses/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserName");
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: Admin/OrderCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderCourseId,CourseId,AccountId,CreatDate")] OrderCourse orderCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserId", orderCourse.AccountId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", orderCourse.CourseId);
            return View(orderCourse);
        }

        // GET: Admin/OrderCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderCourses == null)
            {
                return NotFound();
            }

            var orderCourse = await _context.OrderCourses.FindAsync(id);
            if (orderCourse == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserId", orderCourse.AccountId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", orderCourse.CourseId);
            return View(orderCourse);
        }

        // POST: Admin/OrderCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderCourseId,CourseId,AccountId,CreatDate")] OrderCourse orderCourse)
        {
            if (id != orderCourse.OrderCourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderCourseExists(orderCourse.OrderCourseId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "UserId", "UserId", orderCourse.AccountId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", orderCourse.CourseId);
            return View(orderCourse);
        }

        // GET: Admin/OrderCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderCourses == null)
            {
                return NotFound();
            }

            var orderCourse = await _context.OrderCourses
                .Include(o => o.Account)
                .Include(o => o.Course)
                .FirstOrDefaultAsync(m => m.OrderCourseId == id);
            if (orderCourse == null)
            {
                return NotFound();
            }

            return View(orderCourse);
        }

        // POST: Admin/OrderCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderCourses == null)
            {
                return Problem("Entity set 'TestContext.OrderCourses'  is null.");
            }
            var orderCourse = await _context.OrderCourses.FindAsync(id);
            if (orderCourse != null)
            {
                _context.OrderCourses.Remove(orderCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderCourseExists(int id)
        {
          return (_context.OrderCourses?.Any(e => e.OrderCourseId == id)).GetValueOrDefault();
        }
    }
}
