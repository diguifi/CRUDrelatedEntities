using Abp.Application.Services;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto.Dtos;
using System.Threading.Tasks;

namespace CRUDreborn.Fabricante
{
    public interface IFabricanteAppService : IApplicationService
    {
        Task<CreateFabricanteOutput> CreateFabricante(CreateFabricanteInput input);
        Task<UpdateFabricanteOutput> UpdateFabricante(UpdateFabricanteInput input);
        Task DeleteFabricante(long id);
        Task<Dtos.GetFabricanteByIdOutput> GetById(long id);
        Task<GetAllFabricantesOutput> GetAllFabricantes();
        GetAllProdutosOutput GetAllAssignedProdutos(long fab_id);
    }
}
