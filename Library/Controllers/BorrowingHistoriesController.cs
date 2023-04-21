using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

namespace Library.Controllers
{
    public class BorrowingHistoriesController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowingHistoriesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BorrowingHistories
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.BorrowingHistory.Include(b => b.Borrower);
            return View(await libraryContext.ToListAsync());
        }

        // GET: BorrowingHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BorrowingHistory == null)
            {
                return NotFound();
            }

            var borrowingHistory = await _context.BorrowingHistory
                .Include(b => b.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowingHistory == null)
            {
                return NotFound();
            }

            return View(borrowingHistory);
        }

        // GET: BorrowingHistories/Create
        public IActionResult Create()
        {
            ViewData["BorrowerId"] = new SelectList(_context.Borrower, "Id", "Id");
            return View();
        }

        // POST: BorrowingHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowerId")] BorrowingHistory borrowingHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowingHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowerId"] = new SelectList(_context.Borrower, "Id", "Id", borrowingHistory.BorrowerId);
            return View(borrowingHistory);
        }

        // GET: BorrowingHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BorrowingHistory == null)
            {
                return NotFound();
            }

            var borrowingHistory = await _context.BorrowingHistory.FindAsync(id);
            if (borrowingHistory == null)
            {
                return NotFound();
            }
            ViewData["BorrowerId"] = new SelectList(_context.Borrower, "Id", "Id", borrowingHistory.BorrowerId);
            return View(borrowingHistory);
        }

        // POST: BorrowingHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BorrowerId")] BorrowingHistory borrowingHistory)
        {
            if (id != borrowingHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowingHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingHistoryExists(borrowingHistory.Id))
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
            ViewData["BorrowerId"] = new SelectList(_context.Borrower, "Id", "Id", borrowingHistory.BorrowerId);
            return View(borrowingHistory);
        }

        // GET: BorrowingHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BorrowingHistory == null)
            {
                return NotFound();
            }

            var borrowingHistory = await _context.BorrowingHistory
                .Include(b => b.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowingHistory == null)
            {
                return NotFound();
            }

            return View(borrowingHistory);
        }

        // POST: BorrowingHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BorrowingHistory == null)
            {
                return Problem("Entity set 'LibraryContext.BorrowingHistory'  is null.");
            }
            var borrowingHistory = await _context.BorrowingHistory.FindAsync(id);
            if (borrowingHistory != null)
            {
                _context.BorrowingHistory.Remove(borrowingHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowingHistoryExists(int id)
        {
          return (_context.BorrowingHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
