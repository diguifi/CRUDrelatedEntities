using CRUDreborn.Fabricante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using CRUDreborn.Fabricante.Dtos;

namespace CRUDreborn.Tests.Manufacturers
{
    public class FabricanteAppService_Tests : CRUDrebornTestBase
    {
        private readonly IFabricanteAppService _fabricanteAppService;

        public FabricanteAppService_Tests()
        {
            _fabricanteAppService = Resolve<IFabricanteAppService>();
        }

        [Fact]
        //[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
        public async Task Should_Create_A_Fabricante()
        {
            // Act
            await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test",
                        Description = "Description_test"
                    });

            // Assert
            UsingDbContext(context =>
            {
                var fabricante_teste = context.Fabricantes.FirstOrDefault(u => u.Name == "Fabricante_test");
                fabricante_teste.ShouldNotBeNull();
            });
        }
    }
}
