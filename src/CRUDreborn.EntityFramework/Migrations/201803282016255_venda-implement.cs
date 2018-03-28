namespace CRUDreborn.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class vendaimplement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Product_Id = c.Long(nullable: false),
                        Quantity = c.Long(nullable: false),
                        ProductName = c.String(),
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
                    { "DynamicFilter_Venda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vendas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Venda_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
