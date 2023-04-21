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
    public class ItemsController : Controller
    {
        private readonly LibraryContext _context;

        public ItemsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(string ItemSearch, string ItemYear, Category? ItemCategory)
        {
            if (_context.Item == null)
            {
                return Problem("Entity set 'MvcLibraryContext.Item'  is null.");
            }

            IQueryable<string> CateQuery = _context.Item.OrderBy(x => x.Category).Select(x => x.Category.ToString()!);

            IQueryable<string> YearQuery = _context.Item.OrderBy(x=>x.PublishDate.Year).Select(x => x.PublishDate.Year.ToString()!);

            IQueryable<Item> items = _context.Item.OrderBy(x=>x.Id);

            if (!string.IsNullOrEmpty(ItemSearch))
            {
                items = items.Where(x => x.Title!.Contains(ItemSearch)!);
            }

            if (ItemCategory!=null)
            {
                items = items.Where(x => x.Category == ItemCategory);
            }

            if (!string.IsNullOrEmpty(ItemYear))
            {
                items = items.Where(x => x.PublishDate.Year.ToString() == ItemYear);
            }

            var itemVM = new ItemViewModel
            {
                Categories = new SelectList(await CateQuery.Distinct().ToListAsync()),
                Years = new SelectList(await YearQuery.Distinct().ToListAsync()),
                Items = await items.ToListAsync()
            };

            return View(itemVM);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(Enum.GetValues(typeof(Category)));

            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,PublishDate,RunTime,NumOfPage,Quantity,Price,Category")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PublishDate,RunTime,NumOfPage,Quantity,Price,Category")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Item == null)
            {
                return Problem("Entity set 'LibraryContext.Item'  is null.");
            }
            var item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                _context.Item.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Item?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
