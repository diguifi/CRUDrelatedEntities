using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Abp.Zero.EntityFramework;
using CRUDreborn.Authorization.Roles;
using CRUDreborn.Authorization.Users;
using CRUDreborn.Entities;
using CRUDreborn.MultiTenancy;

namespace CRUDreborn.EntityFramework
{
    public class CRUDrebornDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        public virtual IDbSet<Produto> Produtos { get; set; }
        public virtual IDbSet<Fabricante> Fabricantes { get; set; }
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */

        public CRUDrebornDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in CRUDrebornDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of CRUDrebornDbContext since ABP automatically handles it.
         */
        public CRUDrebornDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public CRUDrebornDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public CRUDrebornDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
