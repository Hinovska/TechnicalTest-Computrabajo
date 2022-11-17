using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Redarbor.WebApp.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly List<APIAuthUser> API_Users = new List<APIAuthUser>();
        private readonly string key;

        public JwtAuthenticationManager(string key, List<APIAuthUser> API_Users)
        {
            this.key = key;
            this.API_Users = API_Users;
        }

        public string Authenticate(string APIUser, string APIPws)
        {
            if (!API_Users.Any(u => u.userName == APIUser && u.password == APIPws))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                 new Claim( ClaimTypes.Name, APIUser)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
