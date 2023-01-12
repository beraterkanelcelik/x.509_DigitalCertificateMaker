namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "UserMail");
            AlterColumn("dbo.Users", "UserMail", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            CreateIndex("dbo.Users", "UserMail", unique: true, name: "UserMail");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "UserMail");
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "UserMail", c => c.String(maxLength: 80));
            CreateIndex("dbo.Users", "UserMail", unique: true, name: "UserMail");
        }
    }
}
