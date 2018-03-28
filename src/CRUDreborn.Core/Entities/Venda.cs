using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class Venda : FullAuditedEntity<long>
    {
        public long Product_Id { get; set; }
        public long Quantity { get; set; }
        public string ProductName { get; set; }

        public Venda()
        {
            CreationTime = DateTime.Now;
        }

        public Venda(long product_Id, long quantity, string productName)
        {
            Product_Id = product_Id;
            Quantity = quantity;
            ProductName = productName;
            CreationTime = DateTime.Now;
        }
    }
}
