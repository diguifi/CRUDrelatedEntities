using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IVendaManager
    {
        long Create(Venda venda);
        Task<Venda> Update(Venda venda);
        Task Delete(long id);
        Task<Venda> GetById(long id);
        IEnumerable<Venda> GetAll();
    }
}
