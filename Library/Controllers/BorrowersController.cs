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
    public class BorrowersController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Borrowers
        public async Task<IActionResult> Index()
        {
              return _context.Borrower != null ? 
                          View(await _context.Borrower.ToListAsync()) :
                          Problem("Entity set 'LibraryContext.Borrower'  is null.");
        }

        // GET: Borrowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrower
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        // GET: Borrowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrower.FindAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Borrower borrower)
        {
            if (id != borrower.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowerExists(borrower.Id))
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
            return View(borrower);
        }

        // GET: Borrowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrower
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Borrower == null)
            {
                return Problem("Entity set 'LibraryContext.Borrower'  is null.");
            }
            var borrower = await _context.Borrower.FindAsync(id);
            if (borrower != null)
            {
                _context.Borrower.Remove(borrower);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowerExists(int id)
        {
          return (_context.Borrower?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
