﻿using Abp.AutoMapper;
using AutoMapper;
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

        public void CreateVenda(CreateVendaInput input)
        {
            var venda = input.MapTo<CRUDreborn.Entities.Venda>();
            _vendaManager.Create(venda);
        }

        public async Task DeleteVenda(long id)
        {
            await _vendaManager.Delete(id);
        }

        public GetAllVendasOutput GetAllVendas()
        {
            var venda = _vendaManager.GetAll().ToList();
            var output = Mapper.Map<List<GetAllVendasItem>>(venda);
            return new GetAllVendasOutput { Vendas = output };
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