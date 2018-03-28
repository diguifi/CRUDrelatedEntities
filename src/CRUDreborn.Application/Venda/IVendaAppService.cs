using Abp.Application.Services;
using CRUDreborn.Venda.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Venda
{
    public interface IVendaAppService : IApplicationService
    {
        Task<CreateVendaOutput> CreateVenda(CreateVendaInput input);
        Task<UpdateVendaOutput> UpdateVenda(UpdateVendaInput input);
        Task DeleteVenda(long id);
        Task<GetVendaByIdOutput> GetById(long id);
        Task<GetAllVendasOutput> GetAllVendas();
    }
}
