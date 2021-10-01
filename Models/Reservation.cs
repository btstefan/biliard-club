using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilijar.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TableId { get; set; }
        
        [Required]
        public int ReservationStatusId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public double TotalPrice { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Table Table { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }
    }
}