using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using CRUDreborn.EntityFramework;

namespace CRUDreborn.Migrator
{
    [DependsOn(typeof(CRUDrebornDataModule))]
    public class CRUDrebornMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<CRUDrebornDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}