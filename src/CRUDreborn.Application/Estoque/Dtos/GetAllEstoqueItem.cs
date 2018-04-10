using Abp.Application.Services.Dto;

namespace CRUDreborn.Estoque.Dtos
{
    public class GetAllEstoqueItem : EntityDto<long>
    {
        public long Stock { get; set; }
        public float Price { get; set; }
        public long AssignedProduct_Id { get; set; }
        public CRUDreborn.Entities.Produto AssignedProduct { get; set; }
    }
}
