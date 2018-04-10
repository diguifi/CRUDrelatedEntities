using Abp.Application.Services.Dto;

namespace CRUDreborn.Fabricante.Dtos
{
    public class UpdateFabricanteInput : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
