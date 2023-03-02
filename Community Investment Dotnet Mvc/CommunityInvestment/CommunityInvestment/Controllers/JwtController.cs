using CommunityInvestment.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommunityInvestment.Controllers
{
    public class JwtController : Controller
    {
        private readonly string tokenKey;
        private readonly CommunityInvestmentContext _context;

        public JwtController(string tokenKey, CommunityInvestmentContext context)
        {
            this.tokenKey = tokenKey;
            this._context = context;
        }

        //Auth model -> JWT token
        
        public string? Authenticate(string email)
        {
            if (!_context.Users.Any(u => u.Email == email))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            AddCookie(tokenString, 60, Response);

            return tokenString;
        }

        public static void AddCookie(string tokenString, int ExpireIn, HttpResponse Response)
        {
            string cookieKey = "authToken";

            CookieOptions cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(ExpireIn),
            };

            Response.Cookies.Append(cookieKey, tokenString, cookieOptions);

        }

        public static void RemoveCookie(string cookieKey, HttpResponse Response)
        {
            Response.Cookies.Delete(cookieKey);
        }

        public static string ReadCookie(string cookieKey, HttpRequest Request)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return Request.Cookies[cookieKey];
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
