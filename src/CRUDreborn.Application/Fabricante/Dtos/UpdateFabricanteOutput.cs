using Abp.Application.Services.Dto;

namespace CRUDreborn.Fabricante.Dtos
{
    public class UpdateFabricanteOutput : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
