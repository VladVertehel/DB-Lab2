//using System;
//using System.Collections.Generic;

//namespace Domain.Model;

///// <summary>
///// Table of all books
///// </summary>
//public partial class Book
//{
//    public int BookId { get; set; }

//    public string Title { get; set; } = null!;

//    public int? AuthorId { get; set; }

//    public string? Author { get; set; }

//    public int? PublisherId { get; set; }

//    public string? Publisher { get; set; }

//    public int? PublishingYear { get; set; }

//    public int? GenreId { get; set; }

//    public string? Genre { get; set; }

//    public virtual Author? AuthorNavigation { get; set; }

//    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

//    public virtual Genre? GenreNavigation { get; set; }

//    public virtual Publisher? PublisherNavigation { get; set; }
//}
