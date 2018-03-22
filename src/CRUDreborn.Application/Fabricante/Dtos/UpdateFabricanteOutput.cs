using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Fabricante.Dtos
{
    public class UpdateFabricanteOutput : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
