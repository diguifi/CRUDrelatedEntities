namespace CRUDreborn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vendacorrection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendas", "AssignedProduct_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Vendas", "AssignedProduct_Id");
            AddForeignKey("dbo.Vendas", "AssignedProduct_Id", "dbo.Produtoes", "Id", cascadeDelete: true);
            DropColumn("dbo.Vendas", "Product_Id");
            DropColumn("dbo.Vendas", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vendas", "ProductName", c => c.String());
            AddColumn("dbo.Vendas", "Product_Id", c => c.Long(nullable: false));
            DropForeignKey("dbo.Vendas", "AssignedProduct_Id", "dbo.Produtoes");
            DropIndex("dbo.Vendas", new[] { "AssignedProduct_Id" });
            DropColumn("dbo.Vendas", "AssignedProduct_Id");
        }
    }
}
