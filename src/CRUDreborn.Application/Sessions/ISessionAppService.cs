using System.Threading.Tasks;
using Abp.Application.Services;
using CRUDreborn.Sessions.Dto;

namespace CRUDreborn.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
