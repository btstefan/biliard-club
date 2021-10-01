using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bilijar.ViewModels
{
    public class ReservationViewModel
    {
        [Required]
        public int TableId { get; set; }

        [Required]
        [DisplayName("From")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("To")]
        public DateTime EndDate { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}