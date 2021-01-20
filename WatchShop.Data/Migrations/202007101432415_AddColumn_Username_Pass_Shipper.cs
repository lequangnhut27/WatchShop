namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Username_Pass_Shipper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shippers", "Username", c => c.String(nullable: true, maxLength: 20));
            AddColumn("dbo.Shippers", "Pass", c => c.String(nullable: true, maxLength: 20));
        }
        
        public override void Down()
        {
            DropIndex("dbo.Shippers", new[] { "Username" });
            DropColumn("dbo.Shippers", "Pass");
            DropColumn("dbo.Shippers", "Username");
        }
    }
}
