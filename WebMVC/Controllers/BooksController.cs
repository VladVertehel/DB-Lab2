using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastracture;
using Microsoft.Data.SqlClient;
using WebMVC.Views.Shared;

namespace WebMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            var books = from m in _context.Books
                        select m;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthorSortParm"] = sortOrder == "author" ? "author_desc" : "author";
            ViewData["PublisherSortParm"] = sortOrder == "publisher" ? "publisher_desc" : "publisher";
            ViewData["YearSortParm"] = sortOrder == "year" ? "year_desc" : "year";
            ViewData["GenreSortParm"] = sortOrder == "genre" ? "genre_desc" : "genre";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(s => s.Title);
                    break;
                case "author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author);
                    break;
                case "publisher":
                    books = books.OrderBy(s => s.Publisher);
                    break;
                case "publisher_desc":
                    books = books.OrderByDescending(s => s.Publisher);
                    break;
                case "year":
                    books = books.OrderBy(s => s.PublishingYear);
                    break;
                case "year_desc":
                    books = books.OrderByDescending(s => s.PublishingYear);
                    break;
                case "genre":
                    books = books.OrderBy(s => s.Genre);
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(s => s.Genre);
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            int pageSize = 7;

            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorNavigation)
                .Include(b => b.GenreNavigation)
                .Include(b => b.PublisherNavigation)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "ID", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "ID", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,AuthorId,Author,PublisherId,Publisher,PublishingYear,GenreId,Genre,ID")] Book book)
        {
            if (ModelState.IsValid)
            {
                Author author = _context.Authors.FirstOrDefault(x => x.ID == book.AuthorId);
                book.AuthorId = author.ID;
                book.Author = author.Name;
                Publisher publisher = _context.Publishers.FirstOrDefault(x => x.ID == book.PublisherId);
                book.PublisherId = publisher.ID;
                book.Publisher = publisher.Name;
                Genre genre = _context.Genres.FirstOrDefault(x => x.ID == book.GenreId);
                book.GenreId = genre.ID;
                book.Genre = genre.Name;

                _context.Add(book);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(_context.Authors, "ID", "Name", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "Name", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "ID", "Name", book.PublisherId);

            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "ID", "Name", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "Name", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "ID", "ContactInfo", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,AuthorId,Author,PublisherId,Publisher,PublishingYear,GenreId,Genre,ID")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "ID", "Name", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "ID", "Name", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "ID", "ContactInfo", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorNavigation)
                .Include(b => b.GenreNavigation)
                .Include(b => b.PublisherNavigation)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            var borrowedBooksToDelete = await _context.BorrowedBooks.Where(b => b.Book.ID == id).ToListAsync();

            if (bookToDelete != null)
            {
                _context.BorrowedBooks.RemoveRange(borrowedBooksToDelete);
                _context.Books.Remove(bookToDelete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.ID == id);
        }
    }
}
