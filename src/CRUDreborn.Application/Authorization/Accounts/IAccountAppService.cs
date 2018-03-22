using System.Threading.Tasks;
using Abp.Application.Services;
using CRUDreborn.Authorization.Accounts.Dto;

namespace CRUDreborn.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
