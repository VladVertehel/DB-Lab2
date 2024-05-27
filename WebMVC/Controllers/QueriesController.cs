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
        //[HttpPost]
        //public IActionResult QueryB1(int userId)
        //{
        //    var users = _context.Users
        //        .FromSqlInterpolated($@"
        //        SELECT *
        //        FROM [User] AS U
        //        WHERE NOT EXISTS
        //        (
        //            SELECT *
        //            FROM Article AS X
        //            WHERE X.AuthorId = U.UserId
        //            AND X.CategoryId NOT IN
        //            (
        //                SELECT Y.CategoryId
        //                FROM Article AS Y
        //                WHERE Y.AuthorId = {userId}
        //            )
        //        )").ToList();
        //    return View("QueryB1", users);
        //}
        //[HttpPost]
        //public IActionResult QueryB2(int categoryId)
        //{
        //    var categories = _context.Categories
        //        .FromSqlInterpolated($@"
        //        SELECT *
        //        FROM Category AS C
        //        WHERE NOT EXISTS
        //        (
        //            SELECT *
        //            FROM Article AS X
        //            WHERE X.CategoryId = {categoryId}
        //            AND X.AuthorId NOT IN
        //            (
        //                SELECT Y.AuthorId
        //                FROM Article AS Y
        //                WHERE Y.CategoryId = C.CategoryId
        //            )
        //        )").ToList();
        //    return View("QueryB2", categories);
        //}
        //[HttpPost]
        //public IActionResult QueryB3(int adminId)
        //{
        //    var admins = _context.Administrator
        //        .FromSqlInterpolated($@"
        //        SELECT *
        //        FROM Administrator AS A
        //        WHERE NOT EXISTS
        //        (
        //            SELECT *
        //            FROM RequestCheck AS X
        //            WHERE X.AdminId = {adminId}
        //            AND X.ArticleId NOT IN
        //            (
        //                SELECT Y.ArticleId
        //                FROM RequestCheck AS Y
        //                WHERE Y.AdminId = A.AdminId
        //            )
        //        )
        //        AND EXISTS
        //        (
        //            SELECT *
        //            FROM RequestCheck AS X0
        //            WHERE X0.AdminId = A.AdminId
        //            AND X0.ArticleId NOT IN
        //            (
        //                SELECT Y0.ArticleId
        //                FROM RequestCheck AS Y0
        //                WHERE Y0.AdminId = {adminId}
        //            )
        //        )"
        //        )
        //        .ToList();
        //    return View("QueryB3", admins);
        //}
        //public IActionResult QueryB4(int userId)
        //{
        //    var users = _context.Users
        //        .FromSqlInterpolated($@"
        //        SELECT *
        //        FROM [User] AS A
        //        WHERE NOT EXISTS
        //        (
        //            SELECT *
        //            FROM Comment AS X
        //            WHERE X.AuthorId = A.UserId
        //            AND X.ArticleId NOT IN
        //            (
        //                SELECT Y.ArticleId
        //                FROM Comment AS Y
        //                WHERE Y.AuthorId = {userId}
        //            )
        //        )
        //        AND EXISTS
        //        (
        //            SELECT *
        //            FROM Comment AS X0
        //            WHERE X0.AuthorId = {userId}
        //            AND X0.ArticleId NOT IN
        //            (
        //                SELECT Y0.ArticleId
        //                FROM Comment AS Y0
        //                WHERE Y0.AuthorId = A.UserId
        //            )
        //        )"
        //        )
        //        .ToList();
        //    return View("QueryB4", users);
        //}
    }
}