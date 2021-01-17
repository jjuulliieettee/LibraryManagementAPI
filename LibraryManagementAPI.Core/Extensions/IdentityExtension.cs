using System.Linq;
using System.Security.Claims;

namespace LibraryManagementAPI.Core.Extensions
{
    public static class IdentityExtension
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.First(i => i.Type == "UserId").Value);
        }
    }
}
