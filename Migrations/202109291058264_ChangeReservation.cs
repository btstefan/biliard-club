namespace Bilijar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservations", "Date");
            DropColumn("dbo.Reservations", "Hours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservations", "EndDate");
            DropColumn("dbo.Reservations", "StartDate");
        }
    }
}
