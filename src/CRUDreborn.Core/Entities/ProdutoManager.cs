using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class ProdutoManager : IDomainService, IProdutoManager
    {
        private IProductRepository _produtoRepository;

        public ProdutoManager(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void Create(Produto produto)
        {
            _produtoRepository.InsertAndAttach(produto);
        }

        public async Task<Produto> Update(Produto produto)
        {
            return await _produtoRepository.UpdateAsync(produto);
        }

        public async Task Delete(long id)
        {
            await _produtoRepository.DeleteAsync(id);
        }

        public async Task<Produto> GetById(long id)
        {
            return await _produtoRepository.GetAsync(id);
        }

        public IEnumerable<Produto> GetAll()
        {
            return _produtoRepository.GetAllIncluding(x => x.AssignedManufacturer);
        }

        public IEnumerable<Produto> GetAllFromFabricante(long fab_id)
        {
            IEnumerable<Produto> produtos = GetAll();

            IEnumerable<Produto> filteringQuery = produtos.Where(p => p.AssignedManufacturer_Id == fab_id);

            //IEnumerable<Produto> filteringQuery =
            //    from prod in produtos
            //    where prod.AssignedManufacturer_Id == fab_id
            //    select prod;

            return filteringQuery;
        }

    }
}
