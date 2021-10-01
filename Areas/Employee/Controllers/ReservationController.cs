using Bilijar.Hubs;
using Bilijar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bilijar.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReservationController()
        {
            this._db = new ApplicationDbContext();
        }


        // GET: Employee/Home
        public ActionResult Index()
        {
            List<Reservation> reservations;
            reservations = _db.Reservations.OrderByDescending(m => m.Created).ToList();

            return View(reservations);
        }

        // change reservation status
        [HttpPost]
        public EmptyResult ChangeStatus(int reservationId, int statusId)
        {
            Reservation reservation = _db.Reservations.Find(reservationId);
            reservation.ReservationStatusId = statusId;
            _db.SaveChanges();

            string statusName = _db.ReservationStatuses.Find(statusId).Name;

            ReservationHub.UpdateReservation(reservation.Id, statusName);

            return new EmptyResult();
        }
    }
}