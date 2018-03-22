using Abp.AutoMapper;
using CRUDreborn.Sessions.Dto;

namespace CRUDreborn.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}