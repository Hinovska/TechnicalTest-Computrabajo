namespace Redarbor.WebApp.Authentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string APIUser, string APIPws);
    }
}
