namespace CRUDreborn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vendatotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendas", "Total", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendas", "Total");
        }
    }
}
