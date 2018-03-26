using Abp.Application.Services;
using Abp.AutoMapper;
using AutoMapper;
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

        public void CreateProduto(CreateProdutoInput input)
        {
            var produto = input.MapTo<CRUDreborn.Entities.Produto>();
            _produtoManager.Create(produto);
            
        }

        public async Task DeleteProduto(long id)
        {
            await _produtoManager.Delete(id);
        }

        public GetAllProdutosOutput GetAllProdutos()
        {
            var produtos = _produtoManager.GetAll().ToList();
            var output = Mapper.Map<List<GetAllProdutosItem>>(produtos);
            return new GetAllProdutosOutput { Produtos = output };
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
