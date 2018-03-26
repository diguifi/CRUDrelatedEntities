namespace CRUDreborn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_foreign_key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produtoes", "AssignedManufacturer_Id", "dbo.Fabricantes");
            DropIndex("dbo.Produtoes", new[] { "AssignedManufacturer_Id" });
            AlterColumn("dbo.Produtoes", "AssignedManufacturer_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Produtoes", "AssignedManufacturer_Id");
            AddForeignKey("dbo.Produtoes", "AssignedManufacturer_Id", "dbo.Fabricantes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtoes", "AssignedManufacturer_Id", "dbo.Fabricantes");
            DropIndex("dbo.Produtoes", new[] { "AssignedManufacturer_Id" });
            AlterColumn("dbo.Produtoes", "AssignedManufacturer_Id", c => c.Long());
            CreateIndex("dbo.Produtoes", "AssignedManufacturer_Id");
            AddForeignKey("dbo.Produtoes", "AssignedManufacturer_Id", "dbo.Fabricantes", "Id");
        }
    }
}
