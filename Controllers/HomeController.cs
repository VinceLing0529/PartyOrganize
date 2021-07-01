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
public IActionResult _login()
        {
            return View();
        }
       
        [HttpPost("Login")]
        public IActionResult Login(LoginUser loginUser)
        {
           if (ModelState.IsValid == false)
            {
                return View("_login");
            }


            Phone dbphone = _context.Phones.FirstOrDefault(user => user.Number == loginUser.LoginName);
             if (dbphone == null)
            {
                ModelState.AddModelError("LoginName", "Phone not found.");
                                return View("_login");

            }

            User dbUser = _context.Users.FirstOrDefault(user => user.PhoneId == dbphone.PhoneId);
            if (dbUser == null)
            {
                ModelState.AddModelError("LoginName", "Phone not found.");
                                return View("_login");

            }
        

           
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);
            if (pwCompareResult == 0)
            {
                ModelState.AddModelError("LoginName", "incorrect credentials.");
                return View("_login"); 
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
            
            string usercity = ViewBag.user.UserLocation.city;
            string userstate = ViewBag.user.UserLocation.state_name;
            
            ViewBag.act= _context.Activits.Include(l => l.Creator.UserLocation)
            .Where(l=>l.Creator.UserLocation.city==usercity && l.Creator.UserLocation.state_name==userstate);
            

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
