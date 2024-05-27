using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Table of all books
/// </summary>
public partial class Book : Entity
{
    [Required(ErrorMessage = "Поле має бути не порожнім")]
    [Display(Name = "Назва")]
    public string Title { get; set; } = null!;
    [Display(Name = "Автор")]
    public int? AuthorId { get; set; }
    [Display(Name = "Автор")]
    public string? Author { get; set; }
    [Display(Name = "Лейбл")]
    public int PublisherId { get; set; }
    [Display(Name = "Лейбл")]
    public string? Publisher { get; set; }
    [Display(Name = "Рік створення")]
    public int PublishingYear { get; set; }
    [Display(Name = "Жанр")]
    public int? GenreId { get; set; }
    [Display(Name = "Жанр")]
    public string? Genre { get; set; }

    public virtual Author? AuthorNavigation { get; set; }

    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

    public virtual Genre? GenreNavigation { get; set; }

    public virtual Publisher? PublisherNavigation { get; set; }
}
