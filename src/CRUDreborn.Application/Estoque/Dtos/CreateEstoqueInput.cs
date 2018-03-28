using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Estoque.Dtos
{
    public class CreateEstoqueInput
    {
        public long Stock { get; set; }
        public float Price { get; set; }
        public long AssignedProduct_Id { get; set; }
        public CRUDreborn.Entities.Produto AssignedProduct { get; set; }
    }
}
