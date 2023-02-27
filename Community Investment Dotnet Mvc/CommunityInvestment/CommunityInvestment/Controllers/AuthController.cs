using CommunityInvestment.Data;
using CommunityInvestment.Models;
using CommunityInvestment.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
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

        [HttpPost]
        public IActionResult LostPassword(User user)
        {
            if (user.Email == null) { return NotFound(); }
            var obj = _context.Users.FirstOrDefault(y => y.Email == user.Email);
            if (obj == null)
            {
                return NotFound("User not found");
            }

            var mailMessage = new MailMessage
            {
                Subject = "Reset your password",
                Body = "<h1>Reset your password with given link ...</h1>",
                IsBodyHtml = false,
            };
            mailMessage.To.Add(user.Email);

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("collectordesk123@gmail.com", "slqrgnuxeuwylhil"),
                EnableSsl = true
            };
            smtpClient.Send("collectordesk123@gmail.com", user.Email, mailMessage.Subject, mailMessage.Body);

            return RedirectToAction("Index", "Home");
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