using Abp.Domain.Repositories;

namespace CRUDreborn.Entities
{
    public interface IEstoqueRepository : IRepository<Estoque, long>
    {
        void InsertAndAttach(Estoque entity);
    }
}
