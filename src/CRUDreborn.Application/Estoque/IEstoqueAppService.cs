﻿using Abp.Application.Services;
using CRUDreborn.Estoque.Dtos;
using System.Threading.Tasks;

namespace CRUDreborn.Estoque
{
    public interface IEstoqueAppService : IApplicationService
    {
        CreateEstoqueOutput CreateEstoque(CreateEstoqueInput input);
        Task<UpdateEstoqueOutput> UpdateEstoque(UpdateEstoqueInput input);
        Task<UpdateEstoqueOutput> UpdateEstoqueQuantity(UpdateEstoqueInput input);
        Task DeleteEstoque(long id);
        Task<GetEstoqueByIdOutput> GetById(long id);
        GetAllEstoqueOutput GetAllEstoque();
        long GetAllEmptyEstoque();
    }
}
