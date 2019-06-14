using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LoginReg.Models;
using Microsoft.AspNetCore.Http;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
        private LogRegContext context;
        public HomeController(LogRegContext DBcontext) {
            context = DBcontext;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegUser user) {
            if (ModelState.IsValid) {
                if (context.Users.Any(u => u.Email == user.Email)) {
                    ModelState.AddModelError("Email", "Email already in use");
                }
                string[] keys = HttpContext.Session.Keys.ToArray();
                if (keys.Contains("count"))
                {
                    int? count = HttpContext.Session.GetInt32("count");
                    HttpContext.Session.SetInt32("count", (int)count + 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("count", 1);
                }
                ViewBag.Count = HttpContext.Session.GetInt32("count");

                PasswordHasher<RegUser> Hasher = new PasswordHasher<RegUser>();
                user.Password = Hasher.HashPassword(user, user.Password);
                context.Add(user);
                context.SaveChanges();
                return View("Success");
            }
            return View("Index");
        }

        [HttpGet("loginpage")]
        public IActionResult LoginPage() {
            return View("Login");
        }

        [HttpPost("login")]
        public IActionResult Login(LogUser user) {
            if (ModelState.IsValid) {
                var userInDb = context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (userInDb == null) {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
                string[] keys = HttpContext.Session.Keys.ToArray();
                if (keys.Contains("count"))
                {
                    int? count = HttpContext.Session.GetInt32("count");
                    HttpContext.Session.SetInt32("count", (int)count + 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("count", 1);
                }
                ViewBag.Count = HttpContext.Session.GetInt32("count");
                var hasher = new PasswordHasher<LogUser>();
                var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);
                Console.WriteLine(ViewBag.Count);
                if (result == 0) {
                    return View("Login");
                }
                return View("Success");
            }
            return View("Login");
        }

        [HttpGet("logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear();
            return View();
        }

    }
}
