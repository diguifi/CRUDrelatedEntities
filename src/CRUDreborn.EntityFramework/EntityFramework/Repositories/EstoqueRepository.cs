using Abp.EntityFramework;
using CRUDreborn.Entities;
using System.Data.Entity;

namespace CRUDreborn.EntityFramework.Repositories
{
    public class EstoqueRepository : CRUDrebornRepositoryBase<Estoque, long>, IEstoqueRepository
    {
        public EstoqueRepository(IDbContextProvider<CRUDrebornDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public void InsertAndAttach(Estoque entity)
        {
            Context.Produtos.Attach(entity.AssignedProduct);
            Context.Estoque.Add(entity);
        }
    }
}
