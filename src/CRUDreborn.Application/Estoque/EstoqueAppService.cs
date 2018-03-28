using Abp.Application.Services;
using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Estoque.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void CreateEstoque(CreateEstoqueInput input)
        {
            var estoque = input.MapTo<CRUDreborn.Entities.Estoque>();
            _estoqueManager.Create(estoque);

        }

        public async Task DeleteEstoque(long id)
        {
            await _estoqueManager.Delete(id);
        }

        public GetAllEstoqueOutput GetAllEstoque()
        {
            var estoque = _estoqueManager.GetAll().ToList();
            var output = Mapper.Map<List<GetAllEstoqueItem>>(estoque);
            return new GetAllEstoqueOutput { Estoque = output };
        }

        public async Task<GetEstoqueByIdOutput> GetById(long id)
        {
            var estoque = await _estoqueManager.GetById(id);
            return estoque.MapTo<GetEstoqueByIdOutput>();
        }

        public async Task<UpdateEstoqueOutput> UpdateEstoque(UpdateEstoqueInput input)
        {
            var estoque = input.MapTo<CRUDreborn.Entities.Estoque>();
            var estoqueUpdated = await _estoqueManager.Update(estoque);
            return estoqueUpdated.MapTo<UpdateEstoqueOutput>();
        }
    }
}
