using Abp.AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Fabricante.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Fabricante
{
    public class FabricanteAppService : IFabricanteAppService
    {
        private IFabricanteManager _fabricanteManager;

        public FabricanteAppService(IFabricanteManager fabricanteManager)
        {
            _fabricanteManager = fabricanteManager;
        }

        public async Task<CreateFabricanteOutput> CreateFabricante(CreateFabricanteInput input)
        {
            var fabricante = input.MapTo<CRUDreborn.Entities.Fabricante>();
            var createdFabricanteId = await _fabricanteManager.Create(fabricante);
            return new CreateFabricanteOutput
            {
                Id = createdFabricanteId
            };
        }

        public async Task DeleteFabricante(long id)
        {
            await _fabricanteManager.Delete(id);
        }

        public async Task<GetAllFabricantesOutput> GetAllFabricantes()
        {
            var fabricantes = await _fabricanteManager.GetAll();
            return new GetAllFabricantesOutput
            {
                Fabricantes = fabricantes.MapTo<List<GetAllFabricantesItem>>()
            };
        }

        public async Task<GetFabricanteByIdOutput> GetById(long id)
        {
            var fabricante = await _fabricanteManager.GetById(id);
            return fabricante.MapTo<GetFabricanteByIdOutput>();
        }

        public async Task<UpdateFabricanteOutput> UpdateFabricante(UpdateFabricanteInput input)
        {
            var fabricante = input.MapTo<CRUDreborn.Entities.Fabricante>();
            var fabricanteUpdated = await _fabricanteManager.Update(fabricante);
            return fabricanteUpdated.MapTo<UpdateFabricanteOutput>();
        }
    }
}
