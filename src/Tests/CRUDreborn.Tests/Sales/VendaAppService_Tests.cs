using CRUDreborn.Estoque;
using CRUDreborn.Estoque.Dtos;
using CRUDreborn.Fabricante;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto;
using CRUDreborn.Produto.Dtos;
using CRUDreborn.Venda;
using CRUDreborn.Venda.Dtos;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CRUDreborn.Tests.Sales
{
    public class VendaAppService_Tests : CRUDrebornTestBase
    {
        private readonly IVendaAppService _vendaAppService;
        private readonly IEstoqueAppService _estoqueAppService;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IFabricanteAppService _fabricanteAppService;

        public VendaAppService_Tests()
        {
            _vendaAppService = Resolve<IVendaAppService>();
            _estoqueAppService = Resolve<IEstoqueAppService>();
            _produtoAppService = Resolve<IProdutoAppService>();
            _fabricanteAppService = Resolve<IFabricanteAppService>();
        }

        [Fact]
        public async Task Should_Create_A_Sale()
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

            //Act
            var vendaID = _vendaAppService.CreateVenda(
                    new CreateVendaInput
                    {
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        },
                        Quantity = 1,
                        Total = 4.9f
                    });

            // Assert
            UsingDbContext(context =>
            {
                var venda_teste = context.Venda.FirstOrDefault();
                venda_teste.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Delete_A_Sale()
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

            var vendaID = _vendaAppService.CreateVenda(
                    new CreateVendaInput
                    {
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        },
                        Quantity = 1,
                        Total = 4.9f
                    });

            // Act
            await _vendaAppService.DeleteVenda(vendaID);

            // Assert
            UsingDbContext(context =>
            {
                var venda_teste = context.Venda.FirstOrDefault(u => u.Id == vendaID);
                venda_teste.IsDeleted.ShouldBeTrue();
            });

        }

        [Fact]
        public async Task Should_Get_A_SaleById()
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

            var vendaID = _vendaAppService.CreateVenda(
                    new CreateVendaInput
                    {
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        },
                        Quantity = 1,
                        Total = 4.9f
                    });

            // Act
            var venda = await _vendaAppService.GetById(vendaID);

            // Assert
            venda.Quantity.ShouldBe(1);
            venda.Total.ShouldBe(4.9f);
        }

        [Fact]
        public async Task Should_Get_All_Sales()
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

            var venda1ID = _vendaAppService.CreateVenda(
                    new CreateVendaInput
                    {
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        },
                        Quantity = 1,
                        Total = 4.9f
                    });

            var venda2ID = _vendaAppService.CreateVenda(
                    new CreateVendaInput
                    {
                        AssignedProduct_Id = produtoOut.Id,
                        AssignedProduct = new Entities.Produto
                        {
                            Id = produtoOut.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            AssignedManufacturer_Id = produto.AssignedManufacturer_Id,
                            AssignedManufacturer = produto.AssignedManufacturer,
                            Consumable = produto.Consumable
                        },
                        Quantity = 3,
                        Total = 4.9f
                    });

            // Act
            var venda = _vendaAppService.GetAllVendas();

            // Assert
            venda.Vendas.Count.ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}
