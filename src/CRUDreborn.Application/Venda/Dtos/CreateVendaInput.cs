﻿

namespace CRUDreborn.Venda.Dtos
{
    public class CreateVendaInput
    {
        public long AssignedProduct_Id { get; set; }
        public CRUDreborn.Entities.Produto AssignedProduct { get; set; }
        public long Quantity { get; set; }
        public float Total { get; set; }
    }
}
