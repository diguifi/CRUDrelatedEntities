using CRUDreborn.Fabricante;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto;
using CRUDreborn.Produto.Dtos;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CRUDreborn.Tests.Products
{
    public class ProdutoAppService_Tests : CRUDrebornTestBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IFabricanteAppService _fabricanteAppService;

        public ProdutoAppService_Tests()
        {
            _produtoAppService = Resolve<IProdutoAppService>();
            _fabricanteAppService = Resolve<IFabricanteAppService>();
        }

        [Fact]
        public async Task Should_Create_A_Produto()
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

            // Act
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

            // Assert
            UsingDbContext(context =>
            {
                var produto_teste = context.Produtos.FirstOrDefault();
                produto_teste.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Update_A_Produto()
        {
            // Arrange (Creation)
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

            // Act
            var output = await _produtoAppService.UpdateProduto(
                    new UpdateProdutoInput
                    {
                        Id = produtoOut.Id,
                        Name = "Produto_test_updated",
                        Description = "Description_test_updated",
                        AssignedManufacturer_Id = fabricanteOut.Id,
                        AssignedManufacturer = new Entities.Fabricante
                        {
                            Id = fabricante.Id,
                            Name = fabricante.Name,
                            Description = fabricante.Description
                        },
                        Consumable = false
                    });

            output.Name.ShouldBe("Produto_test_updated");
            output.Consumable.ShouldBeFalse();

            // Assert
            UsingDbContext(context =>
            {
                var produto_teste = context.Produtos.FirstOrDefault();
                produto_teste.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Delete_A_Produto()
        {
            // Arrange (Creation)
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

            // Act
            await _produtoAppService.DeleteProduto(produtoOut.Id);

            // Assert
            UsingDbContext(context =>
            {
                var produto_teste = context.Produtos.FirstOrDefault(u => u.Id == produtoOut.Id);
                produto_teste.IsDeleted.ShouldBeTrue();
            });

        }

        [Fact]
        public async Task Should_Get_A_ProdutoById()
        {
            // Arrange (Creation)
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

            // Act
            var produto = await _produtoAppService.GetById(produtoOut.Id);

            // Assert
            produto.Name.ShouldBe("Produto_test");
            produto.Id.ShouldBe(produtoOut.Id);
        }

        [Fact]
        public async Task Should_Get_All_Produtos()
        {
            // Arrange (Creation)
            var fabricanteOut = await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test",
                        Description = "Description_test"
                    });

            fabricanteOut.Id.ShouldBe(1);

            var fabricante = await _fabricanteAppService.GetById(1);

            var produto1Out = _produtoAppService.CreateProduto(
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

            var produto2Out = _produtoAppService.CreateProduto(
                    new CreateProdutoInput
                    {
                        Name = "Produto_test2",
                        Description = "Description_test2",
                        AssignedManufacturer_Id = fabricanteOut.Id,
                        AssignedManufacturer = new Entities.Fabricante
                        {
                            Id = fabricante.Id,
                            Name = fabricante.Name,
                            Description = fabricante.Description
                        },
                        Consumable = false
                    });

            // Act
            var produtos = _produtoAppService.GetAllProdutos();

            // Assert
            produtos.Produtos.Count.ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}
