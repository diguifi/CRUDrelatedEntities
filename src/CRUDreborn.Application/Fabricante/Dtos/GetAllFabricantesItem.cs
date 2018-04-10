using Abp.Application.Services.Dto;

namespace CRUDreborn.Fabricante.Dtos
{
    public class GetAllFabricantesItem : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
