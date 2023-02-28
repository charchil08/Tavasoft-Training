using CommunityInvestment.Common;
using CommunityInvestment.Entities.Data;
using CommunityInvestment.Entities.DataModels;
using CommunityInvestment.Models;
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
                TempData["error"] = "User not exist";
                return NotFound("User not found");
            }
            string token = Functionality.GenerateSixCharToken();
            try
            {
                var mailMessage = new MailMessage
                {
                    Subject = "Reset your password",
                    Body = "Go to link -  https://localhost:7012/Auth/ResetPassword?token=" + token + "\n link Valid for 10 minutes only \n you are redirected to otp page directly ...",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(user.Email);

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("charchil.community@gmail.com", ""),
                };
                smtpClient.Send("charchil.community@gmail.com", user.Email, mailMessage.Subject, mailMessage.Body);

                //TODO: generate * string based on mail id


                PasswordReset passwordReset = new PasswordReset();
                passwordReset.Email = user.Email;
                passwordReset.Token = token;
                _context.PasswordResets.Add(passwordReset);
                _context.SaveChanges();

                TempData["success"] = "OTP sent on " + user.Email.ElementAt(0) + user.Email.ElementAt(1) + "******.***";
                return View("Index","Auth");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return NotFound(msg);
            }

        }


        public IActionResult ResetPassword([FromQuery]string token)
        {
            var email = _context.PasswordResets.FirstOrDefault(x => x.Token == token)?.Email;
            if (email == null) return NotFound("User not found");
            User user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null) return NotFound("user not found");
            return View("Index","Home");
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