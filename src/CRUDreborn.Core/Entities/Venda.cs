using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class Venda : FullAuditedEntity<long>
    {
        [ForeignKey("AssignedProduct")]
        public long AssignedProduct_Id { get; set; }
        public Produto AssignedProduct { get; set; }
        public long Quantity { get; set; }

        public Venda()
        {
            CreationTime = DateTime.Now;
        }

        public Venda(long assignedProduct_Id, Produto assignedProduct, long quantity)
        {
            AssignedProduct_Id = assignedProduct_Id;
            AssignedProduct = assignedProduct;
            Quantity = quantity;
            CreationTime = DateTime.Now;
        }
    }
}
