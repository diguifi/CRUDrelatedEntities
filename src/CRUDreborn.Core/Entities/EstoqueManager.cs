using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class EstoqueManager : IDomainService, IEstoqueManager
    {
        private IEstoqueRepository _estoqueRepository;

        public EstoqueManager(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public void Create(Estoque estoque)
        {
            _estoqueRepository.InsertAndAttach(estoque);
        }

        public async Task<Estoque> Update(Estoque estoque)
        {
            return await _estoqueRepository.UpdateAsync(estoque);
        }

        public async Task Delete(long id)
        {
            await _estoqueRepository.DeleteAsync(id);
        }

        public async Task<Estoque> GetById(long id)
        {
            return await _estoqueRepository.GetAsync(id);
        }

        public IEnumerable<Estoque> GetAll()
        {
            return _estoqueRepository.GetAllIncluding(x => x.AssignedProduct);
        }

        public IEnumerable<Estoque> GetAllFromProduto(long prod_id)
        {
            IEnumerable<Estoque> estoque = GetAll();
            IEnumerable<Estoque> filteringQuery = estoque.Where(e => e.AssignedProduct_Id == prod_id);

            return filteringQuery;
        }
    }
}
