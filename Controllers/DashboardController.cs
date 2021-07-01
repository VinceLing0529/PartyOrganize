using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ActivityCenter.Models;
namespace ActivityCenter.Controllers
{
    public class DashboardController : Controller
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

        public DashboardController(ActivityCenterContext context)
        {
            _context = context;
        }

        public IActionResult Main()
        {
            ViewBag.user = _context.Users
            .Include(l =>l.phone)
            .Include(User => User.UserLocation)
            .SingleOrDefault(l => l.UserId == uid);

            ViewBag.myact=_context.Activits
            .Where(l => l.UserId==uid)
            .ToList();

            List<Activit> final = new List<Activit>();

            foreach(var i in ViewBag.myact)
            {
                
                if(i.Date >DateTime.Now)
                {
                    
                final.Add(i);
                           
              }
            }

            ViewBag.myact= final;

            
            return View();
        }

        public IActionResult History()
        {
            ViewBag.user = _context.Users
            .Include(l =>l.phone)
            .Include(User => User.UserLocation)
            .SingleOrDefault(l => l.UserId == uid);

            ViewBag.myact=_context.Activits
            .Where(l => l.UserId==uid)
            .ToList();
            List<Activit> final = new List<Activit>();

            foreach(var i in ViewBag.myact)
            {
                
                if(i.Date <DateTime.Now)
                {
                    
                final.Add(i);
                           
              }
            }

            ViewBag.myact= final;
            
            return View();
        }

            public IActionResult Profile()
        {
            
            ViewBag.user = _context.Users
            .Include(l =>l.phone)
            .Include(User => User.UserLocation)
            .SingleOrDefault(l => l.UserId == uid);
            return View();
        }
        [HttpPost("Update")]
        public IActionResult Update(UpdateUser newUser)
        {
            int id = uid ?? default(int);
           User thisuser = _context.Users.FirstOrDefault(d => d.UserId == id);
           if(ModelState.IsValid) 
           {

           
 Location Userlocation=_context.Locations.SingleOrDefault(n => n.Zip==newUser.Zip);
                if(Userlocation==null)
                {
                    ModelState.AddModelError("Zip", "is not a valid zip.");
                }

                
                Console.WriteLine("Vaid");
           }
           if (ModelState.IsValid == false)
            {
                 ViewBag.user = _context.Users
            .Include(l =>l.phone)
            .Include(User => User.UserLocation)
            .SingleOrDefault(l => l.UserId == uid);
                
                return View("Profile");
            }

           thisuser.Name = newUser.Name;
           thisuser.Gender = newUser.Gender;
           thisuser.Email = newUser.Email;
           thisuser.Zip=newUser.Zip;
           thisuser.Birthday = newUser.Birthday;
           thisuser.LocationId=_context.Locations.SingleOrDefault(n => n.Zip==newUser.Zip).LocationId;
         //  thisuser.Age = newUser.Age;
          // thisuser.School = dishs.School;
          // thisuser.interest = newUser.interest;
           thisuser.UpdatedAt = DateTime.Now;
           _context.SaveChanges(); 
           return RedirectToAction("Profile");
        }



    }}
