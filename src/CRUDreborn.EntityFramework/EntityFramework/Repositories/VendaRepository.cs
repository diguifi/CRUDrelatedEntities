using Abp.EntityFramework;
using CRUDreborn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
