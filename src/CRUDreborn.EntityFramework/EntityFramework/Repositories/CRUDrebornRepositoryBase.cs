using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace CRUDreborn.EntityFramework.Repositories
{
    public abstract class CRUDrebornRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<CRUDrebornDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected CRUDrebornRepositoryBase(IDbContextProvider<CRUDrebornDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class CRUDrebornRepositoryBase<TEntity> : CRUDrebornRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected CRUDrebornRepositoryBase(IDbContextProvider<CRUDrebornDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
