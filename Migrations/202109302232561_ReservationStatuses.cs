namespace Bilijar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationStatuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BlockReservation = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Reservations", "ReservationStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "ReservationStatusId");
            AddForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus");
            DropIndex("dbo.Reservations", new[] { "ReservationStatusId" });
            DropColumn("dbo.Reservations", "ReservationStatusId");
            DropTable("dbo.ReservationStatus");
        }
    }
}
