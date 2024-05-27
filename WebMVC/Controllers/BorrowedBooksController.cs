using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastracture;
using Microsoft.IdentityModel.Tokens;
using WebMVC.Views.Shared;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebMVC.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowedBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BorrowedBooks
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            int? pageNumber)
        {
            var borrowedBooks = from m in _context.BorrowedBooks
                                select m;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["StartSortParm"] = String.IsNullOrEmpty(sortOrder) ? "start_desc" : "";
            ViewData["TimeSortParm"] = sortOrder == "time" ? "time_desc" : "time";

            switch (sortOrder)
            {
                case "start_desc":
                    borrowedBooks = borrowedBooks.OrderByDescending(s => s.BorrowStart);
                    break;
                case "time":
                    borrowedBooks = borrowedBooks.OrderBy(s => s.BorrowTime);
                    break;
                case "time_desc":
                    borrowedBooks = borrowedBooks.OrderByDescending(s => s.BorrowTime);
                    break;
                default:
                    borrowedBooks = borrowedBooks.OrderBy(s => s.BorrowStart);
                    break;
            }

            int pageSize = 7;

            return View(await PaginatedList<BorrowedBook>.CreateAsync(borrowedBooks.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: BorrowedBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "ID", "Title");
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ID", "Name");
            return View();
        }

        // POST: BorrowedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,ReaderId,BorrowStart,BorrowTime,ID")] BorrowedBook borrowedBook)
        {
            if (ModelState.IsValid)
            {
                Book book = _context.Books.FirstOrDefault(b => b.ID == borrowedBook.BookId);
                borrowedBook.BookId = book.ID;
                borrowedBook.BookTitle = book.Title;

                Reader reader = _context.Readers.FirstOrDefault(r => r.ID == borrowedBook.ReaderId);
                borrowedBook.ReaderId = reader.ID;
                borrowedBook.ReaderName = reader.Name;

                _context.Add(borrowedBook);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "ID", "Title", borrowedBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ID", "Name", borrowedBook.ReaderId);
            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "ID", "Title", borrowedBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ID", "Name", borrowedBook.ReaderId);
            return View(borrowedBook);
        }

        // POST: BorrowedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,ReaderId,BorrowStart,BorrowTime,ID")] BorrowedBook borrowedBook)
        {
            if (id != borrowedBook.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Book book = _context.Books.FirstOrDefault(b => b.ID == borrowedBook.BookId);
                    borrowedBook.BookId = book.ID;
                    borrowedBook.BookTitle = book.Title;

                    Reader reader = _context.Readers.FirstOrDefault(r => r.ID == borrowedBook.ReaderId);
                    borrowedBook.ReaderId = reader.ID;
                    borrowedBook.ReaderName = reader.Name;

                    _context.Update(borrowedBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedBookExists(borrowedBook.ID))
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
            ViewData["BookId"] = new SelectList(_context.Books, "ID", "Title", borrowedBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ID", "Name", borrowedBook.ReaderId);
            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            return View(borrowedBook);
        }

        // POST: BorrowedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook != null)
            {
                _context.BorrowedBooks.Remove(borrowedBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedBookExists(int id)
        {
            return _context.BorrowedBooks.Any(e => e.ID == id);
        }
    }
}
