namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_Shipper : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shippers",
                c => new
                    {
                        MaShipper = c.Int(nullable: false, identity: true),
                        HoTen = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaShipper);
            
            AddColumn("dbo.HoaDon", "MaShipper", c => c.Int());
            CreateIndex("dbo.HoaDon", "MaShipper");
            AddForeignKey("dbo.HoaDon", "MaShipper", "dbo.Shippers", "MaShipper");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HoaDon", "MaShipper", "dbo.Shippers");
            DropIndex("dbo.HoaDon", new[] { "MaShipper" });
            DropColumn("dbo.HoaDon", "MaShipper");
            DropTable("dbo.Shippers");
        }
    }
}
