using Abp.Application.Services;
using CRUDreborn.Fabricante.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Fabricante
{
    public interface IFabricanteAppService : IApplicationService
    {
        Task<CreateFabricanteOutput> CreateFabricante(CreateFabricanteInput input);
        Task<UpdateFabricanteOutput> UpdateFabricante(UpdateFabricanteInput input);
        Task DeleteFabricante(long id);
        Task<GetFabricanteByIdOutput> GetById(long id);
        Task<GetAllFabricantesOutput> GetAllFabricantes();
    }
}
