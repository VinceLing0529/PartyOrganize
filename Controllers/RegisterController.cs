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


namespace ActivityCenter.Controllers
{
    public class RegisterController :Controller
    {
        private ActivityCenterContext _context;
       

        public RegisterController(ActivityCenterContext context)
        {
            _context = context;
        }

        public IActionResult Regis()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Location()
        {
            return View();
        }

        public IActionResult NameNPw(Phone phone)
        {
            
            int PhoneId =_context.Phones.SingleOrDefault(n => n.Number == phone.Number).PhoneId;
            HttpContext.Session.SetInt32("phone", phone.PhoneId);
            return View("NameNPw");
        }
        

        [HttpPost("checkcellphone")]
        public IActionResult checkcellphone(Phone newphone)
        {
        
            if (ModelState.IsValid)
            {
                
                if (_context.Phones.Any(u => u.Number == newphone.Number))
                {
                    ModelState.AddModelError("Number", "is taken.");
                }

                
            }
            if (ModelState.IsValid == false)
            {
                return View("Regis");
            }
            
            _context.Phones.Add(newphone);
            _context.SaveChanges();
            
            return RedirectToAction("NameNPw",newphone);
        }

        [HttpPost("Registration")]
        public IActionResult Registration(User newUser)
        {   
            
            if (ModelState.IsValid)
            {
                //     if(newUser.Zip[0]=='0')
                // {
                //     newUser.Zip=newUser.Zip.Substring(1);
                // }
                // if(newUser.Zip[0]=='0')
                // {
                //     newUser.Zip=newUser.Zip.Substring(1);
                // }

                // if (_context.Users.Any(u => u.Name == newUser.Name))
                // {
                //     ModelState.AddModelError("Name", "is taken.");
                // }
                Location Userlocation=_context.Locations.SingleOrDefault(n => n.Zip==newUser.Zip);

                if(Userlocation==null)
                {
                    ModelState.AddModelError("Zip", "is not a valid zip.");
                }

            }
            if (ModelState.IsValid == false)
            {
                
                return View("NameNPw");
            }
            
            newUser.LocationId=_context.Locations.SingleOrDefault(n => n.Zip==newUser.Zip).LocationId;
            newUser.PhoneId= HttpContext.Session.GetInt32("phone")?? default(int);;
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("MainPage","Home");
        }


    }
}