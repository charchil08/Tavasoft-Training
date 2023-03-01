using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MvcJwt.WebApiJwt
{
    public class TokenManager
    {
        //use in appconfig.json
        private static string Secret = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789==";

        public static string GenerateToken(string UserName)
        {
            byte[] Key = Convert.FromBase64String(Secret);
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

        public static ClaimsPrincipal? GetPrincipal(string token)
        {
            try
            {

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken JwtToken = (JwtSecurityToken)handler.ReadToken(token);
                if (JwtToken == null) return null;
                byte[] Key = Convert.FromBase64String(TokenManager.Secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                };

                SecurityToken securityToken;
                ClaimsPrincipal principal = handler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static string? ValidateToken(string token)
        {
            string UserName;
            if(token == null) return null;
            ClaimsPrincipal claimsPrincipal = GetPrincipal(token);
            if(claimsPrincipal == null) return null;

            ClaimsIdentity identity = null;

            try
            {
                identity = (ClaimsIdentity)claimsPrincipal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }

            Claim UserNameClaim = identity.FindFirst(ClaimTypes.Name);
            UserName = UserNameClaim.Value;
            return UserName;
        }
    }

    
}
