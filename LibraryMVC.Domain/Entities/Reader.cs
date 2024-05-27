using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Registered readers
/// </summary>
public partial class Reader : Entity
{
    [Required(ErrorMessage = "Поле має бути не порожнім")]
    [Display(Name = "Ім'я")]
    public string Name { get; set; } = null!;
    [Display(Name = "Контактні дані")]
    public string ContactInfo { get; set; } = null!;

    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
}
