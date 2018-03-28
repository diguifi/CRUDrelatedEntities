using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Venda.Dtos
{
    public class CreateVendaInput
    {
        public long Product_Id { get; set; }
        public long Quantity { get; set; }
        public string ProductName { get; set; }
    }
}
