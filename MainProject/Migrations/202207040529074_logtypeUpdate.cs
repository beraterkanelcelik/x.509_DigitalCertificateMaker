namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logtypeUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Logs", "LogMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "LogMessage", c => c.String());
        }
    }
}
