using Abp.Domain.Repositories;

namespace CRUDreborn.Entities
{
    public interface IProductRepository : IRepository<Produto, long>
    {
        void InsertAndAttach(Produto entity);
    }
}
