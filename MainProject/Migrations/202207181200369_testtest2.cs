namespace MainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtest2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Certificates", "SignatureBit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Certificates", "SignatureBit");
        }
    }
}
