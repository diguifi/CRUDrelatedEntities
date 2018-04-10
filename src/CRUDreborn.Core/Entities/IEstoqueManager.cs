using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IEstoqueManager
    {
        long Create(Estoque estoque, IEnumerable<Estoque> estoques);
        Task<Estoque> Update(Estoque estoque);
        Task<Estoque> UpdateQuantity(Estoque estoque);
        Task Delete(long id);
        Task<Estoque> GetById(long id);
        IEnumerable<Estoque> GetAll();
        IEnumerable<Estoque> GetAllFromProduto(long prod_id);
    }
}
