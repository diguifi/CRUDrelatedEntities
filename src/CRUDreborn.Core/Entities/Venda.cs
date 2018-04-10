using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDreborn.Entities
{
    public class Venda : FullAuditedEntity<long>
    {
        [ForeignKey("AssignedProduct")]
        public long AssignedProduct_Id { get; set; }
        public Produto AssignedProduct { get; set; }
        public long Quantity { get; set; }
        public float Total { get; set; }
        public string Date { get; set; }

        public Venda()
        {
            CreationTime = DateTime.Now;
            Date = DateTime.Now.ToShortDateString();
        }

        public Venda(long assignedProduct_Id, Produto assignedProduct, long quantity, float total)
        {
            AssignedProduct_Id = assignedProduct_Id;
            AssignedProduct = assignedProduct;
            Quantity = quantity;
            Total = total;
            CreationTime = DateTime.Now;
            Date = DateTime.Now.ToShortDateString();
        }
    }
}
