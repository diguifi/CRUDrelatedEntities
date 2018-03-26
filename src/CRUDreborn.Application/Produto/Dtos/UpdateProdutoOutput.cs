using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Produto.Dtos
{
    public class UpdateProdutoOutput : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long AssignedManufacturer_Id { get; set; }
        public CRUDreborn.Entities.Fabricante AssignedManufacturer { get; set; }
        public bool Consumable { get; set; }
    }
}
