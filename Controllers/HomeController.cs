using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ActivityCenter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ActivityCenter.Controllers
{
    public class HomeController : Controller
    {
        private ActivityCenterContext _context;
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }
        
        private bool isLoggedIn
        {
            get
            {
                return uid != null;
            }
        }

        public HomeController(ActivityCenterContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        [HttpPost("Login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }
            Phone dbphone = _context.Phones.FirstOrDefault(user => user.Number == loginUser.LoginName);
            User dbUser = _context.Users.FirstOrDefault(user => user.PhoneId == dbphone.PhoneId);
            if (dbUser == null)
            {
                ModelState.AddModelError("LoginName", "Phone not found.");
                return View("Index"); 
            }

            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);
            if (pwCompareResult == 0)
            {
                ModelState.AddModelError("LoginName", "incorrect credentials.");
                return View("Index"); 
            }
            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            return RedirectToAction("MainPage");
        }

        public IActionResult MainPage()
        {
            if(!isLoggedIn)
            {
                return RedirectToAction("Index");
            }
            ViewBag.user = _context.Users
            .Include(User => User.UserLocation)
            .SingleOrDefault(l => l.UserId == uid);
            return View();
        }

       
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
