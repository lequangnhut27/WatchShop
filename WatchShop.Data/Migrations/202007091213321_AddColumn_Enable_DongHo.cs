namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Enable_DongHo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "Enable", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DongHo", "Enable");
        }
    }
}
