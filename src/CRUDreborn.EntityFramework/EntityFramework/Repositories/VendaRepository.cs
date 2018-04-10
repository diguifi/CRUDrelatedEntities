using Abp.EntityFramework;
using CRUDreborn.Entities;

namespace CRUDreborn.EntityFramework.Repositories
{
    public class VendaRepository : CRUDrebornRepositoryBase<Venda, long>, IVendaRepository
    {
        public VendaRepository(IDbContextProvider<CRUDrebornDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public void InsertAndAttach(Venda entity)
        {
            Context.Produtos.Attach(entity.AssignedProduct);
            Context.Venda.Add(entity);
        }

    }
}
