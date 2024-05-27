using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Table of authors and thier boigraphies
/// </summary>
public partial class Author : Entity
{
    [Required(ErrorMessage = "Поле має бути не порожнім")]
    [Display(Name = "Ім'я")]
    public string Name { get; set; } = null!;
    [Display(Name = "Біографія")]
    public string? Biography { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
