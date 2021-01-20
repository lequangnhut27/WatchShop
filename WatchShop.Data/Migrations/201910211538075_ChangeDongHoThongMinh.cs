namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDongHoThongMinh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DongHoThongMinh", "DuongKinhMat", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DongHoThongMinh", "DuongKinhMat", c => c.String(maxLength: 100));
        }
    }
}
