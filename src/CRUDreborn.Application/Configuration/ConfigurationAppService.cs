using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CRUDreborn.Configuration.Dto;

namespace CRUDreborn.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CRUDrebornAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
