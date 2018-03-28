using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Estoque.Dtos
{
    public class UpdateEstoqueOutput : EntityDto<long>
    {
        public long Stock { get; set; }
        public float Price { get; set; }
        public long AssignedProduct_Id { get; set; }
        public CRUDreborn.Entities.Produto AssignedProduct { get; set; }
    }
}
