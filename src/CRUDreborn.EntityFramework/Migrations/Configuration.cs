using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using CRUDreborn.Entities;
using CRUDreborn.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace CRUDreborn.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<CRUDreborn.EntityFramework.CRUDrebornDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CRUDreborn";
        }

        protected override void Seed(CRUDreborn.EntityFramework.CRUDrebornDbContext context)
        {

            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
