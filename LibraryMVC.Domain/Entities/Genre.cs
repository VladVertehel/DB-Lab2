using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Table of existing genres
/// </summary>
public partial class Genre : Entity
{
    [Required(ErrorMessage = "Поле має бути не порожнім")]
    [Display(Name = "Назва")]
    public string Name { get; set; } = null!;
    [Display(Name = "Опис")]
    public string? Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
