namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullColumn_KhachHang : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.KhachHang", new[] { "TenDangNhap" });
            AlterColumn("dbo.KhachHang", "TenDangNhap", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.KhachHang", "MatKhau", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.KhachHang", "HoTen", c => c.String(maxLength: 100));
            AlterColumn("dbo.KhachHang", "GioiTinh", c => c.String(maxLength: 10));
            AlterColumn("dbo.KhachHang", "SoDT", c => c.String(maxLength: 10, unicode: false));
            CreateIndex("dbo.KhachHang", "TenDangNhap", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.KhachHang", new[] { "TenDangNhap" });
            AlterColumn("dbo.KhachHang", "SoDT", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.KhachHang", "GioiTinh", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.KhachHang", "HoTen", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.KhachHang", "MatKhau", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.KhachHang", "TenDangNhap", c => c.String(nullable: false, maxLength: 20, unicode: false));
            CreateIndex("dbo.KhachHang", "TenDangNhap", unique: true);
        }
    }
}
