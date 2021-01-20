namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Set_Unique_ReadOnly_UsernameAccountManager_TenDangNhapKhachHang : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AccountManager", "Username", unique: true);
            CreateIndex("dbo.KhachHang", "TenDangNhap", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.KhachHang", new[] { "TenDangNhap" });
            DropIndex("dbo.AccountManager", new[] { "Username" });
        }
    }
}
