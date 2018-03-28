using Abp.AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Venda.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Venda
{
    public class VendaAppService : IVendaAppService
    {
        private IVendaManager _vendaManager;

        public VendaAppService(IVendaManager vendaManager)
        {
            _vendaManager = vendaManager;
        }

        public async Task<CreateVendaOutput> CreateVenda(CreateVendaInput input)
        {
            var venda = input.MapTo<CRUDreborn.Entities.Venda>();
            var createdVendaId = await _vendaManager.Create(venda);
            return new CreateVendaOutput
            {
                //Id = createdVendaId
            };
        }

        public async Task DeleteVenda(long id)
        {
            await _vendaManager.Delete(id);
        }

        public async Task<GetAllVendasOutput> GetAllVendas()
        {
            var venda = await _vendaManager.GetAll();
            return new GetAllVendasOutput
            {
                Vendas = venda.MapTo<List<GetAllVendasItem>>()
            };
        }

        public async Task<GetVendaByIdOutput> GetById(long id)
        {
            var venda = await _vendaManager.GetById(id);
            return venda.MapTo<Dtos.GetVendaByIdOutput>();
        }

        public async Task<UpdateVendaOutput> UpdateVenda(UpdateVendaInput input)
        {
            var venda = input.MapTo<CRUDreborn.Entities.Venda>();
            var vendaUpdated = await _vendaManager.Update(venda);
            return vendaUpdated.MapTo<UpdateVendaOutput>();
        }
    }
}
