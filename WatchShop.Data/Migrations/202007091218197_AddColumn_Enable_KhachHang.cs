namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Enable_KhachHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHang", "Enable", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KhachHang", "Enable");
        }
    }
}
