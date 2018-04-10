using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IFabricanteManager
    {
        Task<long> Create(Fabricante fabricante);
        Task<Fabricante> Update(Fabricante fabricante);
        Task Delete(long id);
        Task<Fabricante> GetById(long id);
        Task<List<Fabricante>> GetAll();
    }
}
