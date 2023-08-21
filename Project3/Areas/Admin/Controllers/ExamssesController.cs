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
    public class ExamssesController : Controller
    {
        private readonly TestContext _context;

        public ExamssesController(TestContext context)
        {
            _context = context;
        }

        // GET: Admin/Examsses
        public async Task<IActionResult> Index()
        {
            var testContext = _context.Examsses.Include(e => e.Topic).Include(e => e.User);
            return View(await testContext.ToListAsync());
        }

        // GET: Admin/Examsses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Examsses == null)
            {
                return NotFound();
            }

            var examss = await _context.Examsses
                .Include(e => e.Topic)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ExamId == id);
            if (examss == null)
            {
                return NotFound();
            }

            return View(examss);
        }

        // GET: Admin/Examsses/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "TopicId");
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "UserId");
            return View();
        }

        // POST: Admin/Examsses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamId,UserId,TopicId,Point")] Examss examss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "TopicId", examss.TopicId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "UserId", examss.UserId);
            return View(examss);
        }

        // GET: Admin/Examsses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Examsses == null)
            {
                return NotFound();
            }

            var examss = await _context.Examsses.FindAsync(id);
            if (examss == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "TopicId", examss.TopicId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "UserId", examss.UserId);
            return View(examss);
        }

        // POST: Admin/Examsses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamId,UserId,TopicId,Point")] Examss examss)
        {
            if (id != examss.ExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamssExists(examss.ExamId))
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
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "TopicId", examss.TopicId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "UserId", examss.UserId);
            return View(examss);
        }

        // GET: Admin/Examsses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Examsses == null)
            {
                return NotFound();
            }

            var examss = await _context.Examsses
                .Include(e => e.Topic)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ExamId == id);
            if (examss == null)
            {
                return NotFound();
            }

            return View(examss);
        }

        // POST: Admin/Examsses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Examsses == null)
            {
                return Problem("Entity set 'TestContext.Examsses'  is null.");
            }
            var examss = await _context.Examsses.FindAsync(id);
            if (examss != null)
            {
                _context.Examsses.Remove(examss);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamssExists(int id)
        {
          return (_context.Examsses?.Any(e => e.ExamId == id)).GetValueOrDefault();
        }
    }
}
