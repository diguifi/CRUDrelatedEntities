using CRUDreborn.EntityFramework;
using EntityFramework.DynamicFilters;

namespace CRUDreborn.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly CRUDrebornDbContext _context;

        public InitialHostDbBuilder(CRUDrebornDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
