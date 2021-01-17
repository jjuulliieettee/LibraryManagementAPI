using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementAPI.Core.Configs
{
    public class AuthConfigsManager
    {
        public SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
