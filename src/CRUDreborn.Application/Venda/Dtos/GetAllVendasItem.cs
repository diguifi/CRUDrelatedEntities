using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Venda.Dtos
{
    public class GetAllVendasItem : EntityDto<long>
    {
        public long AssignedProduct_Id { get; set; }
        public CRUDreborn.Entities.Produto AssignedProduct { get; set; }
        public long Quantity { get; set; }
        public float Total { get; set; }
    }
}
