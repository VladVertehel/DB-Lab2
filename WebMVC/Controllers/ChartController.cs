using Infrastracture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ChartController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonDataForGenres")]
        public JsonResult JsonDataForGenres()
        {
            var genres = _context.Genres.ToList();
            var books = _context.Books.ToList();
            int bookCount = 0;
            List<object> genreBook = new List<object>();
            genreBook.Add(new[] { "Жанр", "Кількість платівок" });
            foreach (var g in genres)
            {
                foreach (var b in books)
                {
                    if (b.Genre == g.Name)
                        bookCount++;
                }
                genreBook.Add(new object[] { g.Name, bookCount });
                bookCount = 0;
            }
            return new JsonResult(genreBook);
        }
        [HttpGet("JsonDataForAuthors")]
        public JsonResult JsonDataForAuthors()
        {
            var authors = _context.Authors.ToList();
            var books = _context.Books.ToList();
            int bookCount = 0;
            List<object> authorBook = new List<object>();
            authorBook.Add(new[] { "Автор", "Кількість платівок" });
            foreach (var a in authors)
            {
                foreach (var b in books)
                {
                    if (b.Author == a.Name)
                        bookCount++;
                }
                authorBook.Add(new object[] { a.Name, bookCount });
                bookCount = 0;
            }
            return new JsonResult(authorBook);
        }
        [HttpGet("JsonDataForPublishers")]
        public JsonResult JsonDataForPublishers()
        {
            var publishers = _context.Publishers.ToList();
            var books = _context.Books.ToList();
            int bookCount = 0;
            List<object> publisherBook = new List<object>();
            publisherBook.Add(new[] { "Лейбл", "Кількість платівок" });
            foreach (var p in publishers)
            {
                foreach (var b in books)
                {
                    if (b.Publisher == p.Name)
                        bookCount++;
                }
                publisherBook.Add(new object[] { p.Name, bookCount });
                bookCount = 0;
            }
            return new JsonResult(publisherBook);
        }
    }
}
