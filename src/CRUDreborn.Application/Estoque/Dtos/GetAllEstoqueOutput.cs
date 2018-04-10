using System.Collections.Generic;

namespace CRUDreborn.Estoque.Dtos
{
    public class GetAllEstoqueOutput
    {
        public List<GetAllEstoqueItem> Estoque { get; set; }
    }
}
