using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IProdutoManager
    {
        long Create(Produto produto);
        Task<Produto> Update(Produto produto);
        Task Delete(long id);
        Task<Produto> GetById(long id);
        IEnumerable<Produto> GetAll();
        IEnumerable<Produto> GetAllFromFabricante(long fab_id);
    }
}
