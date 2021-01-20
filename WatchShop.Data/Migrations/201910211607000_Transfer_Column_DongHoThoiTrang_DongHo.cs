namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transfer_Column_DongHoThoiTrang_DongHo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "AnhChinhURL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHo", "Anh1URL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHo", "Anh2URL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHo", "Anh3URL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHo", "Anh4URL", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.DongHoThoiTrang", "AnhChinhURL");
            DropColumn("dbo.DongHoThoiTrang", "Anh1URL");
            DropColumn("dbo.DongHoThoiTrang", "Anh2URL");
            DropColumn("dbo.DongHoThoiTrang", "Anh3URL");
            DropColumn("dbo.DongHoThoiTrang", "Anh4URL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DongHoThoiTrang", "Anh4URL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHoThoiTrang", "Anh3URL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHoThoiTrang", "Anh2URL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHoThoiTrang", "Anh1URL", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.DongHoThoiTrang", "AnhChinhURL", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.DongHo", "Anh4URL");
            DropColumn("dbo.DongHo", "Anh3URL");
            DropColumn("dbo.DongHo", "Anh2URL");
            DropColumn("dbo.DongHo", "Anh1URL");
            DropColumn("dbo.DongHo", "AnhChinhURL");
        }
    }
}
