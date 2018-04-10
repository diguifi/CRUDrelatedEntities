using System;
using System.Collections.Generic;
using Shouldly;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CRUDreborn.Estoque;
using CRUDreborn.Produto;
using CRUDreborn.Fabricante;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto.Dtos;
using CRUDreborn.Estoque.Dtos;

namespace CRUDreborn.Tests.Stock
{
    public class EstoqueAppService_Tests : CRUDrebornTestBase
    {
        private readonly IEstoqueAppService _estoqueAppService;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IFabricanteAppService _fabricanteAppService;

        public EstoqueAppService_Tests()
        {
            _estoqueAppService = Resolve<IEstoqueAppService>();
            _produtoAppService = Resolve<IProdutoAppService>();
            _fabricanteAppService = Resolve<IFabricanteAppService>();
        }

        [Fact]
        public async Task Should_Create_A_Estoque()
        {
            // Arrange
            var fabricanteOut = await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test",
                        Description = "Description_test"
                    });

            fabricanteOut.Id.ShouldBe(1);

            var fabricante = await _fabricanteAppService.GetById(1);

            var produtoOut = _produtoAppService.CreateProduto(
                    new CreateProdutoInput
                    {
                        Name = "Produto_test",
                        Description = "Description_test",
                        AssignedManufacturer_Id = fabricanteOut.Id,
                        AssignedManufacturer = new Entities.Fabricante
                        {
                            Id = fabricante.Id,
                            Name = fabricante.Name,
                            Description = fabricante.Description
                        },
                        Consumable = true
                    });

            var produto = await _produtoAppService.GetById(1);

            // Act
            var estoqueOut = _estoqueAppService.CreateEstoque(
                    new CreateEstoqueInput
                    {
                        Stock = 5,
                        Price = 4.9f,
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        }
                    });

            // Assert
            UsingDbContext(context =>
            {
                var estoque_teste = context.Estoque.FirstOrDefault();
                estoque_teste.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Update_A_Estoque()
        {
            // Arrange
            var fabricanteOut = await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test",
                        Description = "Description_test"
                    });

            fabricanteOut.Id.ShouldBe(1);

            var fabricante = await _fabricanteAppService.GetById(1);

            var produtoOut = _produtoAppService.CreateProduto(
                    new CreateProdutoInput
                    {
                        Name = "Produto_test",
                        Description = "Description_test",
                        AssignedManufacturer_Id = fabricanteOut.Id,
                        AssignedManufacturer = new Entities.Fabricante
                        {
                            Id = fabricante.Id,
                            Name = fabricante.Name,
                            Description = fabricante.Description
                        },
                        Consumable = true
                    });

            var produto = await _produtoAppService.GetById(produtoOut.Id);

            var estoqueOut = _estoqueAppService.CreateEstoque(
                    new CreateEstoqueInput
                    {
                        Stock = 5,
                        Price = 4.9f,
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        }
                    });

            // Act
            var updatedEstoque = await _estoqueAppService.UpdateEstoque(
                    new UpdateEstoqueInput
                    {
                        Id = estoqueOut.Id,
                        Stock = 10,
                        Price = 9.9f,
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        }
                    });

            // Assert
            updatedEstoque.Stock.ShouldBe(10);
            updatedEstoque.Price.ShouldBe(9.9f);
            UsingDbContext(context =>
            {
                var estoque_teste = context.Estoque.FirstOrDefault();
                estoque_teste.ShouldNotBeNull();
            });
        }
    }
}
