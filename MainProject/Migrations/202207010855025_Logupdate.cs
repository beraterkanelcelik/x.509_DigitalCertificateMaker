namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Type");
        }
    }
}
