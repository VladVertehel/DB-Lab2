using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class Entity
    {
        [Required(ErrorMessage = "Поле має бути не порожнім")]
        public int ID { get; set; }
    }
}
