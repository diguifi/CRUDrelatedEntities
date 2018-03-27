﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public interface IProdutoManager
    {
        void Create(Produto produto);
        Task<Produto> Update(Produto produto);
        Task Delete(long id);
        Task<Produto> GetById(long id);
        IEnumerable<Produto> GetAll();
        IEnumerable<Produto> GetAllFromFabricante(long fab_id);
    }
}
