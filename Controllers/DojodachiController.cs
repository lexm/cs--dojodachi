using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace dojodachi.Controllers
{
    public class DojodachiController : Controller
    {
        Random randy = new Random();
        [HttpGet]
        [Route("")]
        public IActionResult Dojodachi()
        {
            if(HttpContext.Session.GetInt32("happiness") == null)
            {
                HttpContext.Session.SetInt32("fullness", 20);
                HttpContext.Session.SetInt32("happiness", 20);
                HttpContext.Session.SetInt32("meals", 3);
                HttpContext.Session.SetInt32("energy", 50);
            }
                ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
                ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
                ViewBag.meals = HttpContext.Session.GetInt32("meals");
                ViewBag.energy = HttpContext.Session.GetInt32("energy");
            // if(TempData["happiness"] == null)
            // {
            //     TempData["fullness"] = 20;
            //     TempData["happiness"] = 20;
            //     TempData["meals"] = 3;
            //     TempData["energy"] = 50;
            // }
            //     ViewBag.fullness = TempData["fullness"];
            //     ViewBag.happiness = TempData["happiness"];
            //     ViewBag.meals = TempData["meals"];
            //     ViewBag.energy = TempData["energy"];
            return View("dojodachi", ViewBag);
        }

        [HttpPost]
        [Route("move")]
        public IActionResult Move(string result)
        {
            int? fullInt = HttpContext.Session.GetInt32("fullness");
            int? happInt = HttpContext.Session.GetInt32("happiness");
            int? mealInt = HttpContext.Session.GetInt32("meals");
            int? enerInt = HttpContext.Session.GetInt32("energy");
            System.Console.WriteLine("{0} {1} {2} {3}", fullInt, happInt, mealInt, enerInt);
            if(result == "Feed")
            {
                if(mealInt > 0)
                {
                    bool like = randy.Next(0, 4) != 0;
                    if(like)
                    {
                        fullInt += randy.Next(5,11);
                    }
                    mealInt--;
                }
            }
            else if(result == "Play")
            {
                if(enerInt > 4)
                {
                    bool like = randy.Next(0, 4) != 0;
                    if(like)
                    {
                        happInt += randy.Next(5, 11);
                    }
                    enerInt -= 5;
                }
            }
            else if(result == "Work")
            {
                if(enerInt > 4)
                {
                    mealInt += randy.Next(1, 4);
                    enerInt -= 5;
                }
            }
            else if(result == "Sleep") 
            {
                enerInt += 15;
                fullInt -= 5;
                happInt -= 5;
            }
            HttpContext.Session.SetInt32("fullness", (int)fullInt);
            HttpContext.Session.SetInt32("happiness", (int)happInt);
            HttpContext.Session.SetInt32("meals", (int)mealInt);
            HttpContext.Session.SetInt32("energy", (int)enerInt);
            // TempData["fullness"] = Convert.ToString(fullInt);
            // TempData["happiness"] = Convert.ToString(happInt);
            // TempData["meals"] = Convert.ToString(mealInt);
            // TempData["energy"] = Convert.ToString(enerInt);
            return RedirectToAction("Dojodachi");
        }
    }
}