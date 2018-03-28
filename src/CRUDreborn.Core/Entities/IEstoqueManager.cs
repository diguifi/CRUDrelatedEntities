using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IEstoqueManager
    {
        void Create(Estoque estoque);
        Task<Estoque> Update(Estoque estoque);
        Task Delete(long id);
        Task<Estoque> GetById(long id);
        IEnumerable<Estoque> GetAll();
        IEnumerable<Estoque> GetAllFromProduto(long prod_id);
    }
}
