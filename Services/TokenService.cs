using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using koinfast.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace koinfast.Services
{
  public class TokenService : ITokenService
  {
    public readonly SymmetricSecurityKey _key;
    public TokenService()
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["TokenKey"]));
    }

    public string CreateToken(string Username, string RoleName)
    {
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.NameId, Username),
        new Claim("role", RoleName)
      };

      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddMinutes(30),
        SigningCredentials = creds
      };

      var tokHandler = new JwtSecurityTokenHandler();
      var tocken = tokHandler.CreateToken(tokenDescriptor);
      return tokHandler.WriteToken(tocken);

    }
  }
}
