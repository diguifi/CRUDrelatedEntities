using System.Threading.Tasks;
using Abp.Application.Services;
using CRUDreborn.Configuration.Dto;

namespace CRUDreborn.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}