using Abp.Application.Services;
using CRUDreborn.Estoque.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Estoque
{
    public interface IEstoqueAppService : IApplicationService
    {
        void CreateEstoque(CreateEstoqueInput input);
        Task<UpdateEstoqueOutput> UpdateEstoque(UpdateEstoqueInput input);
        Task DeleteEstoque(long id);
        Task<GetEstoqueByIdOutput> GetById(long id);
        GetAllEstoqueOutput GetAllEstoque();
        long GetAllEmptyEstoque();
    }
}
