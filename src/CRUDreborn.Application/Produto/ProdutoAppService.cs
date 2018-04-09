using Abp.Application.Services;
using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Estoque.Dtos;
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
        private IEstoqueManager _estoqueManager;

        public ProdutoAppService(IProdutoManager produtoManager, IEstoqueManager estoqueManager)
        {
            _produtoManager = produtoManager;
            _estoqueManager = estoqueManager;
        }

        public CreateProdutoOutput CreateProduto(CreateProdutoInput input)
        {
            var produto = input.MapTo<CRUDreborn.Entities.Produto>();
            var createdProdutoId = _produtoManager.Create(produto);
            return new CreateProdutoOutput
            {
                Id = createdProdutoId
            };
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

        public GetAllProdutosOutput GetAllProdutosCheckingEstoque()
        {
            var produtosRight = new List<Entities.Produto>();
            var produtos = _produtoManager.GetAll().ToList();
            foreach (var produto in produtos)
            {
                var estoques = _estoqueManager.GetAllFromProduto(produto.Id).ToList();
                if (estoques.Count == 0)
                    produtosRight.Add(produto);
            }

            var output = Mapper.Map<List<GetAllProdutosItem>>(produtosRight);
            return new GetAllProdutosOutput { Produtos = output };
        }

        public GetAllEstoqueOutput GetAllAssignedEstoque(long prod_id)
        {
            var estoques = _estoqueManager.GetAllFromProduto(prod_id).ToList();
            var output = Mapper.Map<List<GetAllEstoqueItem>>(estoques);
            return new GetAllEstoqueOutput { Estoque = output };
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
