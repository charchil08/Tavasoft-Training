using CommunityInvestment.Common;
using CommunityInvestment.Entities.Data;
using CommunityInvestment.Entities.DataModels;
using CommunityInvestment.Models;
using CommunityInvestment.Models.ViewModels;
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
        private readonly IConfiguration _configuration;

        public AuthController(CommunityInvestmentContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (user.Email == null || user.Password == null)
            {
                TempData["error"] = "Email or password incorrect";
            }
            var obj = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (obj == null)
            {
                TempData["error"] = "Email or password incorrect";
                return View();
            }
            if (!Crypto.VerifyHashedPassword(obj.Password, user.Password))
            {
                TempData["error"] = "Email or password incorrect";
                return View();
            }
            TempData["success"] = obj.Email + " is in...";
            return RedirectToAction("Index", "Story");
        }

        public IActionResult LostPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LostPassword(User user)
        {
            if (user.Email == null) {
                return View(); 
            }
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

                TempData["success"] = "Mail sent on " + user.Email.ElementAt(0) + user.Email.ElementAt(1) + "******.***";
                return View();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                TempData["error"] = msg;
                return View();
            }

        }


        public IActionResult ResetPassword([FromQuery]string token)
        {
            var email = _context.PasswordResets.FirstOrDefault(x => x.Token == token)?.Email;
            if (email == null)
            {
                TempData["error"] = "User not exist";
                return View("LostPassword");
            }
            User user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                TempData["error"] = "User not exist";
                return View("LostPassword");
            }

            ResetPasswordModel resetPassword = new ResetPasswordModel()
            {
                Token = token
            };

            return View(resetPassword);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel obj)
        {
            var email = _context.PasswordResets.FirstOrDefault(x => x.Token == obj.Token)?.Email;
            if (email == null)
            {
                TempData["error"] = "User not exist";
                return View("LostPassword");
            }

            User? user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                TempData["error"] = "User not exist";
                return View("LostPassword");
            }

            if (obj.Password == null || obj.Password.Trim() == "") {
                TempData["error"] = "Empty password not allowed";
                return RedirectToAction();
            }
            user.Password = Crypto.HashPassword(obj.Password);
            _context.Users.Update(user);
            _context.SaveChanges();

            TempData["success"] = "Password changed successfully";
            return View("Index");
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

            TempData["success"] = "Account created ...";
            return RedirectToAction("Index", "Story");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}