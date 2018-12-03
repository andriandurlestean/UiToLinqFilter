namespace DBConsoleNF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEnum_RemoveViewmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DocumentInfoes", "Status", c => c.String());
            DropTable("dbo.DocumentInfoViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DocumentInfoViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DocumentStatus = c.String(),
                        UserName = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.DocumentInfoes", "Status", c => c.Int(nullable: false));
        }
    }
}
