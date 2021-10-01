using Bilijar.Hubs;
using Bilijar.Models;
using Bilijar.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bilijar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            if (TempData["ValidationError"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ValidationError"];
            }

            List<Table> tables = _db.Tables.ToList();
            ViewBag.Tables = tables;

            return View();
        }


        // Add new reservation to db.
        [HttpPost]
        public ActionResult MakeReservation(ReservationViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ValidationError"] = ViewData;
                    return RedirectToAction("Index");
                }

                // server side validation example
                if (vm.TableId == 0)
                {
                    TempData["error"] = "Table not selected";
                    TempData["ValidationError"] = ViewData; // save user input
                    return RedirectToAction("Index");
                }

                // check if dates are valid
                if (!IsReservationValid(vm))
                {
                    TempData["error"] = "Table is not available in selected period.";
                    TempData["ValidationError"] = ViewData;
                    return RedirectToAction("Index");
                }

                string userId = User.Identity.GetUserId();

                Reservation reservation = new Reservation
                {
                    TableId = vm.TableId,
                    UserId = userId,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    Comment = vm.Comment,
                    ReservationStatusId = 1,
                    TotalPrice = GetPrice(vm),
                    Created = DateTime.Now
                };

                _db.Reservations.Add(reservation);
                _db.SaveChanges();

                // after insert, update employee page
                ReservationHub.LoadReservation();

                TempData["success"] = "Order added successfuly.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Unexpected error occurred. Try again later.";
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// Function for Date validations
        /// </summary>
        /// <returns>true if valid</returns>
        public bool IsReservationValid(ReservationViewModel vm)
        {
            // if end date 
            if (DateTime.Compare(vm.StartDate, vm.EndDate) > 0)
                return false;

            // get all reservations for a table where start date is in the future and status is blocking reservation
            var tableReservations = _db.Reservations
                .Where(r => r.TableId == vm.TableId
                    && r.StartDate >= DateTime.Now
                    && r.ReservationStatus.BlockReservation == true)
                .ToList();


            // date is valid when vm.EndDate is before old reservations start date
            // or vm.StartDate is after end date
            bool isValid = true;
            foreach (Reservation reservation in tableReservations)
            {
                //if (vm.EndDate > reservation.StartDate || vm.StartDate < reservation.EndDate)
                if (DateTime.Compare(vm.EndDate, reservation.StartDate) > 0 || DateTime.Compare(vm.StartDate, reservation.EndDate) < 0)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Calculate total price for reservation
        /// </summary>
        public double GetPrice(ReservationViewModel vm)
        {
            TimeSpan ts = vm.EndDate - vm.StartDate;

            double hours = (double)ts.TotalMinutes/60;

            var table = _db.Tables.Find(vm.TableId);
            double totalPrice = hours * table.Price;

            return totalPrice;
        }
    }
}