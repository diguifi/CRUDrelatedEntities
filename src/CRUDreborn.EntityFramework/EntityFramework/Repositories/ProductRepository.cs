using Abp.EntityFramework;
using CRUDreborn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Context.SaveChanges();
        }

    }
}
