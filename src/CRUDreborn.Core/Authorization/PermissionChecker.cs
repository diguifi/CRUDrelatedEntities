using Abp.Authorization;
using CRUDreborn.Authorization.Roles;
using CRUDreborn.Authorization.Users;

namespace CRUDreborn.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
