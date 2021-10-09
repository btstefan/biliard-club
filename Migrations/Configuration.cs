namespace Bilijar.Migrations
{
    using Bilijar.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // roles
            context.Roles.AddOrUpdate(m => m.Name,
                new IdentityRole { Name = "Employee" }
            );

            // employee user
            if (!context.Users.Any(u => u.UserName == "employee@test.rs"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "employee@test.rs", Email = "employee@test.rs" };

                manager.Create(user, "ChangeIt!");
                manager.AddToRole(user.Id, "Employee");
            }

            // reservation status
            context.ReservationStatuses.AddOrUpdate(m => m.Id,
                new ReservationStatus { Id = 1, Name = "Open", BlockReservation = false },
                new ReservationStatus { Id = 2, Name = "Accepted", BlockReservation = true },
                new ReservationStatus { Id = 3, Name = "Declined", BlockReservation = false }
            );

            // tables
            context.Tables.AddOrUpdate(m => m.Id,
                new Table { Id = 1, Name = "Table A", Price = 20, Description = "Desc1" },
                new Table { Id = 2, Name = "Table B", Price = 40, Description = "Desc2" },
                new Table { Id = 3, Name = "Table C", Price = 30, Description = "Desc3" },
                new Table { Id = 4, Name = "Table D", Price = 10, Description = "Desc4" },
                new Table { Id = 5, Name = "Table F", Price = 15, Description = "Desc5" }
            );
        }
    }
}
