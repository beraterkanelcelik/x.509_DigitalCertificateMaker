namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtest1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Certificates", "CertificateName");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Certificates", "CertificateName", unique: true, name: "CertificateName");
        }
    }
}
