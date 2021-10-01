using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bilijar.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}