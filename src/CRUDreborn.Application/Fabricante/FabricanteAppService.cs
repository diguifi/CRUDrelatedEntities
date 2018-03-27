using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto.Dtos;
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
        private IProdutoManager _produtoManager;

        public FabricanteAppService(IFabricanteManager fabricanteManager, IProdutoManager produtoManager)
        {
            _fabricanteManager = fabricanteManager;
            _produtoManager = produtoManager;
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

        public async Task<Dtos.GetFabricanteByIdOutput> GetById(long id)
        {
            var fabricante = await _fabricanteManager.GetById(id);
            return fabricante.MapTo<Dtos.GetFabricanteByIdOutput>();
        }

        public async Task<UpdateFabricanteOutput> UpdateFabricante(UpdateFabricanteInput input)
        {
            var fabricante = input.MapTo<CRUDreborn.Entities.Fabricante>();
            var fabricanteUpdated = await _fabricanteManager.Update(fabricante);
            return fabricanteUpdated.MapTo<UpdateFabricanteOutput>();
        }

        public GetAllProdutosOutput GetAllAssignedProdutos(long fab_id)
        {
            var produtos = _produtoManager.GetAllFromFabricante(fab_id).ToList();
            var output = Mapper.Map<List<GetAllProdutosItem>>(produtos);
            return new GetAllProdutosOutput { Produtos = output };
        }
     }
}
