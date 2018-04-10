using Abp.Application.Services;
using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Estoque.Dtos;
using CRUDreborn.Produto.Dtos;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Creates a 'Produto' (Product)
        /// </summary>
        /// <param name="input">Produto's input DTO</param>
        /// <returns>Creation Output DTO containing the Id</returns>
        public CreateProdutoOutput CreateProduto(CreateProdutoInput input)
        {
            var produto = input.MapTo<CRUDreborn.Entities.Produto>();
            var createdProdutoId = _produtoManager.Create(produto);
            return new CreateProdutoOutput
            {
                Id = createdProdutoId
            };
        }

        /// <summary>
        /// Deletes a 'Produto' (Product)
        /// </summary>
        /// <param name="id">Produto's Id</param>
        /// <returns></returns>
        public async Task DeleteProduto(long id)
        {
            await _produtoManager.Delete(id);
        }

        /// <summary>
        /// Gets all stored 'Produtos' (Products)
        /// </summary>
        /// <returns>List DTO containing all stored 'Produto'</returns>
        public GetAllProdutosOutput GetAllProdutos()
        {
            var produtos = _produtoManager.GetAll().ToList();
            var output = Mapper.Map<List<GetAllProdutosItem>>(produtos);
            return new GetAllProdutosOutput { Produtos = output };
        }

        /// <summary>
        /// Gets all 'Produtos' (Products) checking if are on stock
        /// </summary>
        /// <returns>Returns only Produtos not yet in stock</returns>
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

        /// <summary>
        /// Gets the Estoque assigned to the Produto
        /// </summary>
        /// <param name="prod_id">Id of the Produto</param>
        /// <returns>Estoque assigned to it</returns>
        public GetAllEstoqueOutput GetAllAssignedEstoque(long prod_id)
        {
            var estoques = _estoqueManager.GetAllFromProduto(prod_id).ToList();
            var output = Mapper.Map<List<GetAllEstoqueItem>>(estoques);
            return new GetAllEstoqueOutput { Estoque = output };
        }

        /// <summary>
        /// Gets a stored 'Produto' (Product) by Id
        /// </summary>
        /// <param name="id">Produto's Id</param>
        /// <returns>Produto's DTO</returns>
        public async Task<GetProdutoByIdOutput> GetById(long id)
        {
            var produto = await _produtoManager.GetById(id);
            return produto.MapTo<GetProdutoByIdOutput>();
        }

        /// <summary>
        /// Updates a 'Produto' (Product)
        /// </summary>
        /// <param name="input">Produto's input DTO</param>
        /// <returns>Produto's DTO</returns>
        public async Task<UpdateProdutoOutput> UpdateProduto(UpdateProdutoInput input)
        {
            var produto = input.MapTo<CRUDreborn.Entities.Produto>();
            var produtoUpdated = await _produtoManager.Update(produto);
            return produtoUpdated.MapTo<UpdateProdutoOutput>();
        }
    }
}
