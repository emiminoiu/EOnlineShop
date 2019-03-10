namespace EOnlineShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUpdateOnBrands : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "CategoryName");
        }
    }
}
