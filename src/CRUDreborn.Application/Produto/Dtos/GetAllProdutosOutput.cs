﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Produto.Dtos
{
    public class GetAllProdutosOutput
    {
        public List<GetAllProdutosItem> Produtos { get; set; }
    }
}