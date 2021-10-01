namespace Bilijar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "StartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Reservations", "EndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Reservations", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
