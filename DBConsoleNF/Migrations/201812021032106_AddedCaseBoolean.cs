namespace DBConsoleNF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCaseBoolean : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentInfoes", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentInfoes", "IsDeleted");
        }
    }
}
