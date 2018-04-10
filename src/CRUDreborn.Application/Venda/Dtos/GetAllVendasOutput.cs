using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace CRUDreborn.Venda.Dtos
{
    public class GetAllVendasOutput : EntityDto<long>
    {
        public List<GetAllVendasItem> Vendas { get; set; }
    }
}
