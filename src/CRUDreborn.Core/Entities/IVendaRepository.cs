using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IVendaRepository : IRepository<Venda, long>
    {
        void InsertAndAttach(Venda entity);
    }
}
