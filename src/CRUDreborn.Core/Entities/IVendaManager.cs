using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IVendaManager
    {
        void Create(Venda venda);
        Task<Venda> Update(Venda venda);
        Task Delete(long id);
        Task<Venda> GetById(long id);
        IEnumerable<Venda> GetAll();
    }
}
