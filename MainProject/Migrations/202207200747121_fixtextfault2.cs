namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixtextfault2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Certificates", "HashAlgorithm", c => c.String(nullable: false));
            DropColumn("dbo.Certificates", "HashAlgoritm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Certificates", "HashAlgoritm", c => c.String(nullable: false));
            DropColumn("dbo.Certificates", "HashAlgorithm");
        }
    }
}
