using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CRUDreborn.Roles.Dto;
using CRUDreborn.Users.Dto;

namespace CRUDreborn.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}