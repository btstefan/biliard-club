using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bilijar.Models
{
    public class ReservationStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // block new reservation when old reservation has status?
        public bool BlockReservation { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}