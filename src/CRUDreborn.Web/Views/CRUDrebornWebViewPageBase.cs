using Abp.Web.Mvc.Views;

namespace CRUDreborn.Web.Views
{
    public abstract class CRUDrebornWebViewPageBase : CRUDrebornWebViewPageBase<dynamic>
    {

    }

    public abstract class CRUDrebornWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected CRUDrebornWebViewPageBase()
        {
            LocalizationSourceName = CRUDrebornConsts.LocalizationSourceName;
        }
    }
}