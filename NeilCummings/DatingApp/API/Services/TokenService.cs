using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API;
// Implement the ITokenService
public class TokenService : ITokenService
{
    // SymmetricSecurityKey will not be sent to client
    private readonly SymmetricSecurityKey _key;
    //constructor
    public TokenService(IConfiguration config)
    {
        // get the value from our local config for the key
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
    // Implement the interface
    public string CreateToken(AppUser user)
    {
        // claim - And our users going to have a token that claims their username is what it's set to inside the token,
        var claims = new List<Claim>
       {
        new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
       };
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
