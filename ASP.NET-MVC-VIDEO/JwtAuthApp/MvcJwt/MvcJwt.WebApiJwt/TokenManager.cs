using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MvcJwt.WebApiJwt
{
    public class TokenManager
    {
        //use in appconfig.json
        private static readonly string secret = "officejwttoken";

        public static string GenerateToken(string UserName)
        {
            byte[] Key = Convert.FromBase64String(secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Key);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, UserName) }),
                Expires = DateTime.UtcNow.AddMinutes(160),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = jwtSecurityTokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
