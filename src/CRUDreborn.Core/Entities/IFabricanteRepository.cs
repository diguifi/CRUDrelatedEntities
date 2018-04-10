using Abp.Domain.Repositories;
using System.Collections.Generic;

namespace CRUDreborn.Entities
{
    public interface IFabricanteRepository : IRepository<Fabricante, long>
    {
        ICollection<Produto> GetAssignedProdutos(long id);
    }
}
