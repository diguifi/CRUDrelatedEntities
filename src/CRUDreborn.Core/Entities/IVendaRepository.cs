using Abp.Domain.Repositories;

namespace CRUDreborn.Entities
{
    public interface IVendaRepository : IRepository<Venda, long>
    {
        void InsertAndAttach(Venda entity);
    }
}
