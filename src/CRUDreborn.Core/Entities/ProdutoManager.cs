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
            //_produtoRepository.InsertAndGetIdAsync(produto);
            //return await _produtoRepository.InsertAndGetIdAsync(produto);
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

        public async Task<List<Produto>> GetAll()
        {
            return await _produtoRepository.GetAllListAsync();
        }

    }
}
