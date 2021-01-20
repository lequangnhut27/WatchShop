namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLengthColDongHoThongMinh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DongHoThongMinh", "TheoDoiSucKhoe", c => c.String());
            AlterColumn("dbo.DongHoThongMinh", "HienThiThongBao", c => c.String());
            AlterColumn("dbo.DongHoThongMinh", "TienIchKhac", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DongHoThongMinh", "TienIchKhac", c => c.String(maxLength: 100));
            AlterColumn("dbo.DongHoThongMinh", "HienThiThongBao", c => c.String(maxLength: 100));
            AlterColumn("dbo.DongHoThongMinh", "TheoDoiSucKhoe", c => c.String(maxLength: 100));
        }
    }
}
