namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueattributeAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Certificates", "CertificateName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "UserMail", c => c.String(maxLength: 20));
            CreateIndex("dbo.Certificates", "CertificateName", unique: true, name: "CertificateName");
            CreateIndex("dbo.Users", "UserMail", unique: true, name: "UserMail");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "UserMail");
            DropIndex("dbo.Certificates", "CertificateName");
            AlterColumn("dbo.Users", "UserMail", c => c.String());
            AlterColumn("dbo.Certificates", "CertificateName", c => c.String(nullable: false));
        }
    }
}
