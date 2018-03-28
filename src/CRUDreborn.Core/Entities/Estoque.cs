using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class Estoque : FullAuditedEntity<long>
    {
        public long Stock { get; set; }
        public float Price { get; set; }
        [ForeignKey("AssignedProduct")]
        public long AssignedProduct_Id { get; set; }
        public Produto AssignedProduct { get; set; }

        public Estoque()
        {
            CreationTime = DateTime.Now;
        }

        public Estoque(long stock, float price, long assignedProduct_Id, Produto assignedProduct)
        {
            Stock = stock;
            Price = price;
            AssignedProduct_Id = assignedProduct_Id;
            AssignedProduct = assignedProduct;
            CreationTime = DateTime.Now;
        }
    }
}
