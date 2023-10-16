using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SFirstApplicationApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SFirstApplicationApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string CreateToken(UserDTO userDTO)   //using that method which is told in Itokenservice
        {
           // claim in list
            var claims = new List<Claim>
          {
              new Claim(JwtRegisteredClaimNames.NameId,userDTO.UserId)
          };
          // signing credential
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
           //security token descriptor
               // get identity
               //expire date
               //signing credential
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = cred
            };
          //security token handler
          //tokenHandler.createToken(tokenDesc)
          //tokenHandler.WriteToken(token)
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }

     
    }
}