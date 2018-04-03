namespace CRUDreborn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date_on_venda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendas", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendas", "Date");
        }
    }
}
