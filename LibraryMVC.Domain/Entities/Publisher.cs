using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Table of publishers
/// </summary>
public partial class Publisher : Entity
{
    [Required(ErrorMessage = "Поле має бути не порожнім")]
    [Display(Name = "Назва")]
    public string Name { get; set; } = null!;
    [Display(Name = "Контактні дані")]
    public string ContactInfo { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
