using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Service
{
    public class TokenService : iTokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            //
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); //get มาจากappsetting
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName) //key
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature); //genSignature

            var tokenDescription = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescription) ;

        return tokenHandler.WriteToken(token);
        }
    }
}