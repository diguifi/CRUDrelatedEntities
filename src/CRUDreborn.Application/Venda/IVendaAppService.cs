﻿using Abp.Application.Services;
using CRUDreborn.Venda.Dtos;
using System.Threading.Tasks;

namespace CRUDreborn.Venda
{
    public interface IVendaAppService : IApplicationService
    {
        long CreateVenda(CreateVendaInput input);
        Task<UpdateVendaOutput> UpdateVenda(UpdateVendaInput input);
        Task DeleteVenda(long id);
        Task<GetVendaByIdOutput> GetById(long id);
        GetAllVendasOutput GetAllVendas();
        float GetTotalVendas();
        string GetMostSold();
        string GetDaysSales();
    }
}
