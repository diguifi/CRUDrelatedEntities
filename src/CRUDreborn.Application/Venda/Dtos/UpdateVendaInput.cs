using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Venda.Dtos
{
    public class UpdateVendaInput : EntityDto<long>
    {
        public long Product_Id { get; set; }
        public long Quantity { get; set; }
        public string ProductName { get; set; }
    }
}
