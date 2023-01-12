namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usermailsize : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "UserMail");
            AlterColumn("dbo.Users", "UserMail", c => c.String(maxLength: 80));
            CreateIndex("dbo.Users", "UserMail", unique: true, name: "UserMail");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "UserMail");
            AlterColumn("dbo.Users", "UserMail", c => c.String(maxLength: 20));
            CreateIndex("dbo.Users", "UserMail", unique: true, name: "UserMail");
        }
    }
}
