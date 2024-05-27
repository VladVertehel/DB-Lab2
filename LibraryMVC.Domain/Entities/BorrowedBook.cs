using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Books that are currently borrowed
/// </summary>
public partial class BorrowedBook : Entity
{
    [Display(Name = "Вінілова пластинка")]
    public int BookId { get; set; }
    [Display(Name = "Вінілова пластинка")]
    public string? BookTitle { get; set; }
    [Display(Name = "Користувач")]
    public int ReaderId { get; set; }
    [Display(Name = "Користувач")]
    public string? ReaderName { get; set; }
    public int? BorrowStart { get; set; }
    public int? BorrowTime { get; set; }

    public virtual Book? Book { get; set; } = null!;

    public virtual Reader? Reader { get; set; } = null!;
}
