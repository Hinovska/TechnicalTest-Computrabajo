using Microsoft.IdentityModel.Tokens;

namespace Redarbor.WebApp.Authentication
{
    public interface IJwtAuthenticationManager
    {
        SecurityToken Authenticate(string APIUser, string APIPws);
    }
}
