using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto.Dtos;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Creates a 'Fabricante' (Manufacturer)
        /// </summary>
        /// <param name="input">Fabricante's input DTO</param>
        /// <returns>Creation Output DTO containing the Id</returns>
        public async Task<CreateFabricanteOutput> CreateFabricante(CreateFabricanteInput input)
        {
            var fabricante = input.MapTo<CRUDreborn.Entities.Fabricante>();
            var createdFabricanteId = await _fabricanteManager.Create(fabricante);
            return new CreateFabricanteOutput
            {
                Id = createdFabricanteId
            };
        }

        /// <summary>
        /// Deletes a 'Fabricante' (Manufacturer)
        /// </summary>
        /// <param name="id">Fabricante's Id</param>
        /// <returns></returns>
        public async Task DeleteFabricante(long id)
        {
            await _fabricanteManager.Delete(id);
        }

        /// <summary>
        /// Gets all stored 'Fabricantes' (Manufacturers)
        /// </summary>
        /// <returns>List DTO containing all stored 'Fabricante'</returns>
        public async Task<GetAllFabricantesOutput> GetAllFabricantes()
        {
            var fabricantes = await _fabricanteManager.GetAll();
            return new GetAllFabricantesOutput
            {
                Fabricantes = fabricantes.MapTo<List<GetAllFabricantesItem>>()
            };
        }

        /// <summary>
        /// Gets a stored 'Fabricante' (Manufacturer) by Id
        /// </summary>
        /// <param name="id">Fabricante's Id</param>
        /// <returns>Fabricante's DTO</returns>
        public async Task<Dtos.GetFabricanteByIdOutput> GetById(long id)
        {
            var fabricante = await _fabricanteManager.GetById(id);
            return fabricante.MapTo<Dtos.GetFabricanteByIdOutput>();
        }

        /// <summary>
        /// Updates 'Fabricante' (Manufacturer)
        /// </summary>
        /// <param name="input">Fabricante's input DTO</param>
        /// <returns>Fabricante's DTO</returns>
        public async Task<UpdateFabricanteOutput> UpdateFabricante(UpdateFabricanteInput input)
        {
            var fabricante = input.MapTo<CRUDreborn.Entities.Fabricante>();
            var fabricanteUpdated = await _fabricanteManager.Update(fabricante);
            return fabricanteUpdated.MapTo<UpdateFabricanteOutput>();
        }

        /// <summary>
        /// Get all 'Produtos' assigned to the specified 'Fabricante'
        /// </summary>
        /// <param name="fab_id">Fabricante's Id</param>
        /// <returns>List DTO containing all assigned 'Produtos'</returns>
        public GetAllProdutosOutput GetAllAssignedProdutos(long fab_id)
        {
            var produtos = _produtoManager.GetAllFromFabricante(fab_id).ToList();
            var output = Mapper.Map<List<GetAllProdutosItem>>(produtos);
            return new GetAllProdutosOutput { Produtos = output };
        }
     }
}
