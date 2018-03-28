namespace CRUDreborn.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class stockentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estoques",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Stock = c.Long(nullable: false),
                        Price = c.Single(nullable: false),
                        AssignedProduct_Id = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Estoque_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtoes", t => t.AssignedProduct_Id, cascadeDelete: true)
                .Index(t => t.AssignedProduct_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estoques", "AssignedProduct_Id", "dbo.Produtoes");
            DropIndex("dbo.Estoques", new[] { "AssignedProduct_Id" });
            DropTable("dbo.Estoques",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Estoque_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
