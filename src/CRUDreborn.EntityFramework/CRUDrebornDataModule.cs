using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using CRUDreborn.EntityFramework;

namespace CRUDreborn
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(CRUDrebornCoreModule))]
    public class CRUDrebornDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CRUDrebornDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
