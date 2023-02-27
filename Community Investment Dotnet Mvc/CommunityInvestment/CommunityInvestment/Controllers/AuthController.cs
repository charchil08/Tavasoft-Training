using CommunityInvestment.Data;
using CommunityInvestment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web.Helpers;

namespace CommunityInvestment.Controllers
{
    public class AuthController : Controller
    {
        private readonly CommunityInvestmentContext _context;

        public AuthController(CommunityInvestmentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (user == null)
            {
                return NotFound("User not found");
            }
            var obj = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (obj == null)
            {
                return NotFound();
            }
            if (!Crypto.VerifyHashedPassword(obj.Password, user.Password))
            {
                return NotFound("User not found");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LostPassword()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (user == null) return View("Error");
            user.CityId = 11;
            user.CountryId = 1;

            user.Password = Crypto.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}