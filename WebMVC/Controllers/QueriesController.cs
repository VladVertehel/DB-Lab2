using Infrastracture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LibraryWebApplication1.Controllers
{
    public class QueriesController : Controller
    {
        private readonly LibraryContext _context;
        public QueriesController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            var books = _context.Books.ToList();
            var borrowedBooks = _context.BorrowedBooks.ToList();
            var genres = _context.Genres.ToList();
            var publishers = _context.Publishers.ToList();
            var readers = _context.Readers.ToList();
            ViewBag.Authors = authors;
            ViewBag.Books = books;
            ViewBag.BorrowedBooks = borrowedBooks;
            ViewBag.Genres = genres;
            ViewBag.Publishers = publishers;
            ViewBag.Readers = readers;
            return View();
        }
        [HttpPost]
        public IActionResult QueryA1(int readerId)
        {
            var books = _context.Books
                .FromSqlInterpolated($@"
                SELECT *
                FROM Books
                WHERE book_ID IN
                    (SELECT book_ID
                     FROM BorrowedBooks
                    WHERE reader_ID={readerId})")
                .ToList();
            return View("QueryA1", books);
        }
        [HttpPost]
        public IActionResult QueryA2(int genreId)
        {
            var readers = _context.Readers
                .FromSqlInterpolated($@"
                SELECT *
                FROM Readers
                WHERE reader_ID IN
                    (SELECT reader_ID
                     FROM BorrowedBooks
                    WHERE book_ID IN
                        (SELECT book_ID
                         FROM Books
                         WHERE genre_ID={genreId}))").ToList();
            return View("QueryA2", readers);
        }
        [HttpPost]
        public IActionResult QueryA3(int publishingYear)
        {
            var books = _context.Books
                .FromSqlInterpolated($@"
                SELECT * 
                FROM Books 
                WHERE Books.publishing_year > {publishingYear}")
                .ToList();
            return View("QueryA3", books);
        }
        [HttpPost]
        public IActionResult QueryA4(int bookId)
        {
            var readers = _context.Readers
                .FromSqlInterpolated($@"
                SELECT *
                FROM Readers
                WHERE reader_ID IN
                    (SELECT reader_ID
                     FROM BorrowedBooks
                    WHERE book_ID={bookId})")
                .ToList();

            return View("QueryA4", readers);
        }
        [HttpPost]
        public IActionResult QueryA5(int authorId)
        {
            var books = _context.Books
                .FromSqlInterpolated($@"
                SELECT * 
                FROM Books 
                WHERE author_ID = {authorId}")
                .ToList();
            return View("QueryA5", books);
        }
        [HttpPost]
        public IActionResult QueryB1(int authorId)
        {
            var authors = _context.Authors
                .FromSqlInterpolated($@"
                SELECT *
                FROM Authors AS U
                WHERE NOT EXISTS
                (
                    SELECT *
                    FROM Books AS X
                    WHERE X.author_ID = U.author_ID
                    AND X.genre_ID NOT IN
                    (
                        SELECT Y.genre_ID
                        FROM Books AS Y
                        WHERE Y.author_ID = {authorId}
                    )
                )").ToList();
            return View("QueryB1", authors);
        }
        [HttpPost]
        public IActionResult QueryB2(int genreId)
        {
            var genres = _context.Genres
                .FromSqlInterpolated($@"
                SELECT *
                FROM Genres AS G
                WHERE NOT EXISTS
                (
                    SELECT *
                    FROM Books AS X
                    WHERE X.genre_ID = {genreId}
                    AND X.author_ID NOT IN
                    (
                        SELECT Y.author_ID
                        FROM Books AS Y
                        WHERE Y.genre_ID = G.genre_ID
                    )
                )").ToList();
            return View("QueryB2", genres);
        }
        [HttpPost]
        public IActionResult QueryB3(int readerId)
        {
            var readers = _context.Readers
                .FromSqlInterpolated($@"
                SELECT *
                FROM Readers AS R
                WHERE NOT EXISTS
                (
                    SELECT *
                    FROM BorrowedBooks AS X
                    WHERE X.reader_ID = {readerId}
                    AND X.book_ID NOT IN
                    (
                        SELECT Y.book_ID
                        FROM BorrowedBooks AS Y
                        WHERE Y.reader_ID = R.reader_ID
                    )
                )
                AND EXISTS
                (
                    SELECT *
                    FROM BorrowedBooks AS X0
                    WHERE X0.reader_ID = R.reader_ID
                    AND X0.book_ID NOT IN
                    (
                        SELECT Y0.book_ID
                        FROM BorrowedBooks AS Y0
                        WHERE Y0.reader_ID = {readerId}
                    )
                )"
                )
                .ToList();
            return View("QueryB3", readers);
        }
        [HttpPost]
        public IActionResult QueryB4(int readerId)
        {
            var readers = _context.Readers
                .FromSqlInterpolated($@"
                SELECT *
                FROM Readers AS R
                WHERE NOT EXISTS
                (
                    SELECT *
                    FROM BorrowedBooks AS X
                    WHERE X.reader_ID = R.reader_ID
                    AND X.book_ID NOT IN
                    (
                        SELECT Y.book_ID
                        FROM BorrowedBooks AS Y
                        WHERE Y.reader_ID = {readerId}
                    )
                )
                AND EXISTS
                (
                    SELECT *
                    FROM BorrowedBooks AS X0
                    WHERE X0.reader_ID = {readerId}
                    AND X0.book_ID NOT IN
                    (
                        SELECT Y0.book_ID
                        FROM BorrowedBooks AS Y0
                        WHERE Y0.reader_ID = R.reader_ID
                    )
                )"
                )
                .ToList();
            return View("QueryB4", readers);
        }
        [HttpPost]
        public IActionResult QueryB5(int readerId)
        {
            var readers = _context.Readers
                .FromSqlInterpolated($@"
                SELECT *
                FROM Readers AS R
                WHERE NOT EXISTS
                (
                    SELECT *
                    FROM BorrowedBooks AS X
                    WHERE X.reader_ID = {readerId}
                    AND X.book_ID NOT IN
                    (
                        SELECT Y.book_ID
                        FROM BorrowedBooks AS Y
                        WHERE Y.reader_ID = R.reader_ID
                    )
                )
                AND EXISTS
                (
                    SELECT *
                    FROM BorrowedBooks AS X0
                    WHERE X0.reader_ID = R.reader_ID
                    AND X0.book_ID NOT IN
                    (
                        SELECT Y0.book_ID
                        FROM BorrowedBooks AS Y0
                        WHERE Y0.reader_ID = {readerId}
                    )
                )"
                )
                .ToList();
            return View("QueryB5", readers);
        }
    }
}