using Bilijar.Models;
using Bilijar.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Bilijar.Hubs
{
    public class ReservationHub : Hub
    {
        /// <summary>
        /// Update reservations list
        /// </summary>
        public static void LoadReservation()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ReservationHub>();

            // send to employee clients
            context.Clients.Group("Employee").InsertReservation();
        }

        /// <summary>
        /// Update reservation status only
        /// </summary>
        public static void UpdateReservation(int reservationId, string statusName)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ReservationHub>();

            // send to employee clients
            context.Clients.Group("Employee").UpdateReservation(reservationId, statusName);
        }

        public override Task OnConnected()
        {
            // Add employee user to Employee group
            // to avoid sending messages to all clients.
            bool isInRole = Context.User.IsInRole("Employee");
            if (isInRole)
            {
                Groups.Add(Context.ConnectionId, "Employee");
            }
            return base.OnConnected();
        }

    }
}