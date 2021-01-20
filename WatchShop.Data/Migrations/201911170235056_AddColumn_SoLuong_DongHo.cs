namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_SoLuong_DongHo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "SoLuong", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DongHo", "SoLuong");
        }
    }
}
