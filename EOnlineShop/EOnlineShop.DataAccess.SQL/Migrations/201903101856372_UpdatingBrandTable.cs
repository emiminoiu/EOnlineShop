namespace EOnlineShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingBrandTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Brands", name: "Category_Id", newName: "ProductCategory_Id");
            RenameIndex(table: "dbo.Brands", name: "IX_Category_Id", newName: "IX_ProductCategory_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Brands", name: "IX_ProductCategory_Id", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Brands", name: "ProductCategory_Id", newName: "Category_Id");
        }
    }
}
