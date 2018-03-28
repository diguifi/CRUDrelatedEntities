using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Estoque.Dtos
{
    public class GetAllEstoqueOutput
    {
        public List<GetAllEstoqueItem> Estoque { get; set; }
    }
}
