﻿using CRUDreborn.Fabricante;
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
        public async Task Should_Create_A_Fabricante()
        {
            // Act
            var fabricanteId = await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test",
                        Description = "Description_test"
                    });

            // Assert
            UsingDbContext(context =>
            {
                var fabricante_teste = context.Fabricantes.FirstOrDefault();
                fabricante_teste.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Get_A_Fabricante()
        {
            // Arrange
            var fabricanteId = await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test",
                        Description = "Description_test"
                    });

            UsingDbContext(context =>
            {
                var fabricante_teste = context.Fabricantes.FirstOrDefault();
                fabricante_teste.ShouldNotBeNull();
            });

            // Assert
            var fabricante = await _fabricanteAppService.GetById(1);

            fabricante.Name.ShouldBe("Fabricante_test");
            fabricante.Id.ShouldBe(1);
        }

        [Fact]
        public async Task Should_Update_A_Fabricante()
        {
            // Arrange
            var fabricanteOut = await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test_update",
                        Description = "Description_test_update"
                    });
            
            UsingDbContext(context =>
            {
                var fabricante_teste = context.Fabricantes.FirstOrDefault();
                fabricante_teste.ShouldNotBeNull();
            });

            // Act
            var output = await _fabricanteAppService.UpdateFabricante(
                    new UpdateFabricanteInput
                    {
                        Id = fabricanteOut.Id,
                        Name = "Fabricante_test_updateee",
                        Description = "Desc_test_updateee"
                    });

            output.Name.ShouldBe("Fabricante_test_updateee");

            // Assert
            UsingDbContext(context =>
            {
                var fabricante_teste = context.Fabricantes.FirstOrDefault();
                fabricante_teste.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Delete_A_Fabricante()
        {
            // Arrange
            var fabricanteOutput = await _fabricanteAppService.CreateFabricante(
                    new CreateFabricanteInput
                    {
                        Name = "Fabricante_test_ForDeletion",
                        Description = "Description_test_ForDeletion"
                    });

            UsingDbContext(context =>
            {
                var fabricante_teste = context.Fabricantes.FirstOrDefault(u => u.Name == "Fabricante_test_ForDeletion");
                fabricante_teste.ShouldNotBeNull();
            });

            // Act
            await _fabricanteAppService.DeleteFabricante(fabricanteOutput.Id);

            // Assert
            UsingDbContext(context =>
            {
                var fabricante_teste = context.Fabricantes.FirstOrDefault(u => u.Id == 1);
                fabricante_teste.IsDeleted.ShouldBeTrue();
            });
        }
    }
}
