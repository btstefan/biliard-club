namespace Bilijar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "TotalPrice");
        }
    }
}
