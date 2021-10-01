namespace Bilijar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noNullUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropIndex("dbo.Reservations", new[] { "UserId" });
            AlterColumn("dbo.Reservations", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reservations", "UserId");
            AddForeignKey("dbo.Reservations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropIndex("dbo.Reservations", new[] { "UserId" });
            AlterColumn("dbo.Reservations", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "UserId");
            AddForeignKey("dbo.Reservations", "UserId", "dbo.Users", "Id");
        }
    }
}
