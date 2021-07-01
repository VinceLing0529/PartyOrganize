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
    public class ActivitController : Controller
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
        public ActivitController(ActivityCenterContext context)
        {
            _context = context;
        }
        

        public IActionResult New()
        {
            


            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(Activit n)
        {
             if (ModelState.IsValid == false)
            {
                Console.WriteLine("Not valid");
                return View("New");
            }
            if(n.Duration ==0)
            {
                n.DurationUnit=null;
            }
           
            User CurrentUser= _context.Users
            .FirstOrDefault(l => l.UserId==uid);
            n.UserId = CurrentUser.UserId;
            
            _context.Activits.Add(n);
            _context.SaveChanges();
            return RedirectToAction("MainPage","Home");

        }
        

        [HttpPost("Reserve")]
        public IActionResult Reserve(Join n)
        {
            
            _context.Joins.Add(n);
            _context.SaveChanges();
            return RedirectToAction("Home");

        }
        [HttpPost("UnReserve")]
        public IActionResult UnReserve(Join n)
        {
            Join one =_context.Joins
            .SingleOrDefault(d => d.ActivitId == n.ActivitId && d.UserId == n.UserId); 
            _context.Joins.Remove(one);
            _context.SaveChanges();
            return RedirectToAction("Home");

        }

        [HttpGet("{ActivitId}/delete")]
        public IActionResult Delete(int ActivitId)
        {
            Activit one = _context.Activits
            .SingleOrDefault(d => d.ActivitId == ActivitId); 
           _context.Activits.Remove(one);
           _context.SaveChanges();
           return RedirectToAction("Home");
        }

        [HttpGet("{Activits}")]
        public IActionResult Detail(int Activits)
        {

            ViewBag.Ac= _context.Activits
            .Include(l => l.Creator)
            .Include(l => l.Guest)
            .ThenInclude(l => l.User)
            .FirstOrDefault(d => d.ActivitId == Activits)
            ;
            
            ViewBag.AllGuest = _context.Users
            .Include(l=>l.Attend)
            .ThenInclude(l => l.Activit)
            .Where(l=>l.Attend.Any(l=>l.Activit.ActivitId==Activits))
            .ToList();

             ViewBag.Exceptions = _context.Activits
            .Include(l => l.Guest)
            .ThenInclude(l =>l.User)
            .Where(l => l.Guest.Any(l => l.User.UserId == uid ))
            .ToList();

            ViewBag.Id = uid;


           return View("Detail");
        }

    }
}