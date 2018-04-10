using Abp.Application.Services.Dto;

namespace CRUDreborn.Produto.Dtos
{
    public class GetAllProdutosItem : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long AssignedManufacturer_Id { get; set; }
        public CRUDreborn.Entities.Fabricante AssignedManufacturer { get; set; }
        public bool Consumable { get; set; }
    }
}
