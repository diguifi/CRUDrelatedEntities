using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Venda.Dtos
{
    public class GetAllVendasOutput : EntityDto<long>
    {
        public List<GetAllVendasItem> Vendas { get; set; }
    }
}
