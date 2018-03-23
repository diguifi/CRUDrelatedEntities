using Abp.Application.Services;
using Abp.AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Produto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Produto
{
    public class ProdutoAppService : ApplicationService, IProdutoAppService
    {
        private IProdutoManager _produtoManager;

        public ProdutoAppService(IProdutoManager produtoManager)
        {
            _produtoManager = produtoManager;
        }

        public async Task<CreateProdutoOutput> CreateProduto(CreateProdutoInput input)
        {
            var produto = input.MapTo<CRUDreborn.Entities.Produto>();
            var createdProdutoId = await _produtoManager.Create(produto);
            return new CreateProdutoOutput
            {
                Id = createdProdutoId
            };
        }

        public async Task DeleteProduto(long id)
        {
            await _produtoManager.Delete(id);
        }

        public async Task<GetAllProdutosOutput> GetAllProdutos()
        {
            var produtos = await _produtoManager.GetAll();
            return new GetAllProdutosOutput
            {
                Produtos = produtos.MapTo<List<GetAllProdutosItem>>()
            };
        }

        public async Task<GetProdutoByIdOutput> GetById(long id)
        {
            var produto = await _produtoManager.GetById(id);
            return produto.MapTo<GetProdutoByIdOutput>();
        }

        public async Task<UpdateProdutoOutput> UpdateProduto(UpdateProdutoInput input)
        {
            var produto = input.MapTo<CRUDreborn.Entities.Produto>();
            var produtoUpdated = await _produtoManager.Update(produto);
            return produtoUpdated.MapTo<UpdateProdutoOutput>();
        }
    }
}
