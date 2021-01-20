namespace WatchShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColunm_MoTa_DongHo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "MoTa", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DongHo", "MoTa");
        }
    }
}
