using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CRUDreborn.MultiTenancy.Dto;

namespace CRUDreborn.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
