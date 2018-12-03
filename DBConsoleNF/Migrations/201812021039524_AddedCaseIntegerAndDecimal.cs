namespace DBConsoleNF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCaseIntegerAndDecimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentInfoes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DocumentInfoes", "Pages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentInfoes", "Pages");
            DropColumn("dbo.DocumentInfoes", "Price");
        }
    }
}
