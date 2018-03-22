using System.Linq;
using CRUDreborn.EntityFramework;
using CRUDreborn.MultiTenancy;

namespace CRUDreborn.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly CRUDrebornDbContext _context;

        public DefaultTenantCreator(CRUDrebornDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
