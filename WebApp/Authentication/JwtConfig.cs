using System.Collections.Generic;

namespace Redarbor.WebApp.Authentication
{
    public class JwtConfig
    {
        public string key { get; set; }
        public List<APIAuthUser> authUsers { get; set; }
    }

    public class APIAuthUser
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
