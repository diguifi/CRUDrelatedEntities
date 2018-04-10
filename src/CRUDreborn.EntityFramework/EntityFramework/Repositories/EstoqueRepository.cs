using Abp.EntityFramework;
using CRUDreborn.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void UpdateFix(Estoque entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

    }
}
