namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Gia_ChiTietHoaDon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChiTietHoaDon", "Gia", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChiTietHoaDon", "Gia");
        }
    }
}
