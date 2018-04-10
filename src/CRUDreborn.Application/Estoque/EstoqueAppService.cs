using Abp.Application.Services;
using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Estoque.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDreborn.Estoque
{
    public class EstoqueAppService : ApplicationService, IEstoqueAppService
    {
        private IEstoqueManager _estoqueManager;

        public EstoqueAppService(IEstoqueManager estoqueManager)
        {
            _estoqueManager = estoqueManager;
        }

        /// <summary>
        /// Creates a 'Estoque' (Stock)
        /// </summary>
        /// <param name="input">Estoque's input DTO</param>
        /// <returns>Creation Output DTO containing the Id</returns>
        public CreateEstoqueOutput CreateEstoque(CreateEstoqueInput input)
        {
            var estoque = input.MapTo<CRUDreborn.Entities.Estoque>();
            var estoques = _estoqueManager.GetAll().ToList();
            var createEstoqueId = _estoqueManager.Create(estoque, estoques);
            return new CreateEstoqueOutput
            {
                Id = createEstoqueId
            };

        }

        /// <summary>
        /// Deletes a 'Estoque' (Stock)
        /// </summary>
        /// <param name="id">Estoque's Id</param>
        /// <returns></returns>
        public async Task DeleteEstoque(long id)
        {
            await _estoqueManager.Delete(id);
        }

        /// <summary>
        /// Gets all stored 'Estoque' (Stock)
        /// </summary>
        /// <returns>List DTO containing all stored 'Estoque'</returns>
        public GetAllEstoqueOutput GetAllEstoque()
        {
            var estoque = _estoqueManager.GetAll().ToList();
            var output = Mapper.Map<List<GetAllEstoqueItem>>(estoque);
            return new GetAllEstoqueOutput { Estoque = output };
        }

        /// <summary>
        /// Gets number of all empty 'Estoques' (Stocks)
        /// </summary>
        /// <returns>Number of all 'Estoques' (Stocks) which Quantity attribute is equal to 0</returns>
        public long GetAllEmptyEstoque()
        {
            var estoque = _estoqueManager.GetAll().ToList();
            long allEmpty = 0;
            foreach (var est in estoque)
            {
                if (est.Stock == 0)
                    allEmpty++;
            }
            return allEmpty;
        }

        /// <summary>
        /// Gets a stored 'Estoque' (Stock) by Id
        /// </summary>
        /// <param name="id">Estoque's Id</param>
        /// <returns>Estoque's DTO</returns>
        public async Task<GetEstoqueByIdOutput> GetById(long id)
        {
            var estoque = await _estoqueManager.GetById(id);
            return estoque.MapTo<GetEstoqueByIdOutput>();
        }

        /// <summary>
        /// Updates a 'Estoque' (Stock)
        /// </summary>
        /// <param name="input">Estoque's input DTO</param>
        /// <returns>Estoque's DTO</returns>
        public async Task<UpdateEstoqueOutput> UpdateEstoque(UpdateEstoqueInput input)
        {
            var estoque = input.MapTo<CRUDreborn.Entities.Estoque>();
            var estoqueUpdated = await _estoqueManager.Update(estoque);
            return estoqueUpdated.MapTo<UpdateEstoqueOutput>();
        }

        /// <summary>
        /// Updates a 'Estoque' (Stock)
        /// </summary>
        /// <param name="input">Estoque's input DTO</param>
        /// <returns>Estoque's DTO</returns>
        public async Task<UpdateEstoqueOutput> UpdateEstoqueQuantity(UpdateEstoqueInput input)
        {
            var estoque = input.MapTo<CRUDreborn.Entities.Estoque>();
            var estoqueUpdated = await _estoqueManager.UpdateQuantity(estoque);
            return estoqueUpdated.MapTo<UpdateEstoqueOutput>();
        }
    }
}
