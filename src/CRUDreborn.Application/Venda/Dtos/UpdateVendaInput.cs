using Abp.Application.Services.Dto;

namespace CRUDreborn.Venda.Dtos
{
    public class UpdateVendaInput : EntityDto<long>
    {
        public long AssignedProduct_Id { get; set; }
        public CRUDreborn.Entities.Produto AssignedProduct { get; set; }
        public long Quantity { get; set; }
        public float Total { get; set; }
    }
}
