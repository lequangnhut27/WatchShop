namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_SoDT_HoTen_HoaDon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoaDon", "HoTen", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.HoaDon", "SoDT", c => c.String(nullable: false, maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HoaDon", "SoDT");
            DropColumn("dbo.HoaDon", "HoTen");
        }
    }
}
