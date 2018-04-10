using Abp.EntityFramework;
using CRUDreborn.Entities;

namespace CRUDreborn.EntityFramework.Repositories
{
    public class ProductRepository : CRUDrebornRepositoryBase<Produto, long>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<CRUDrebornDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public void InsertAndAttach(Produto entity)
        {
            Context.Fabricantes.Attach(entity.AssignedManufacturer);
            Context.Produtos.Add(entity);
        }

    }
}
