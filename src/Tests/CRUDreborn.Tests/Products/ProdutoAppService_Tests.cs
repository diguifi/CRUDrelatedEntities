using CRUDreborn.Fabricante;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto;
using CRUDreborn.Produto.Dtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //Assert
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
    }
}
